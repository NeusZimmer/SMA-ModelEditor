using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMA_ModelEditor
{
    class BotonDesplegar: Button
    {
        public bool desplegado;
        public int nodoid;
        public int? parentid=null;
        public List<int> children=new List<int>();
        public int nivelprofundidad=1;
        public int orden = 1;
        public string nombre;
        private Label campodetexto = new Label();
        public BotonDesplegar(int _nodoid)
        {
            nodoid = _nodoid;
            
            this.Text = "+";
            this.Click += (s,a)=> CambiarEstadoClick(s,a,this);
            this.Name = nodoid.ToString();
            this.Width = 50;
            this.Height = 25;
            //this.panel1.Location = new System.Drawing.Point(0, 0);
            this.AutoSize = false;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackColor = Color.LightSkyBlue;
            crearlabel();
        }
        public new void Show()
        {
            campodetexto.Show();  
            base.Show();
        }
        public new void Hide()
        {
            campodetexto.Hide();
            base.Hide();
        }

        private void crearlabel()
        {
            this.Controls.Add(campodetexto);
            //campodetexto.AutoSize = true;
            //campodetexto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            campodetexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //campodetexto.Margin = new System.Windows.Forms.Padding(0);
            campodetexto.Name = "label1"+nodoid.ToString();
            campodetexto.Size = new System.Drawing.Size(250, 200);
            campodetexto.Text = "EESTOTOOODODODODODODODODO";
            campodetexto.BackColor = Color.Black;
            campodetexto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        }

        private void PosicionarLabel()
        {
            Point sitioparent = this.Location;
            campodetexto.Location = new System.Drawing.Point(sitioparent.X+50, sitioparent.Y+80);
            campodetexto.Location = new System.Drawing.Point(150, 180);
            campodetexto.Show();
        }
        private void CambiarEstadoClick(object s, EventArgs a, BotonDesplegar botonDesplegar)
        {
            if (!(this.parentid == null))
            {
                var listabotones = this.Parent.Controls.OfType<BotonDesplegar>();
                var listaHermanos = from BotonDesplegar _botonhermano in listabotones where ((_botonhermano.parentid) == this.parentid) && (_botonhermano.nodoid != this.nodoid) select _botonhermano;
                foreach (Control hermano in listaHermanos)
                    ((BotonDesplegar)hermano).Replegar();
            }
            CambiarEstado();
        }
        public bool CambiarEstado()
        {
            this.desplegado = !desplegado;

            if (desplegado)
            {
                this.Posicionar();
                this.Text = "-:"+nodoid.ToString();
                this.BackColor = Color.LightSteelBlue;
                foreach(int hijoindex in this.children)
                {
                    Control hijo = (from Control _botonhijo in this.Parent.Controls where _botonhijo.Name == hijoindex.ToString() select _botonhijo).First();
                    ((BotonDesplegar)hijo).Desplegar();
                }
            }
            else
            {
                this.Text = "+:" + nodoid.ToString();
                this.BackColor = this.BackColor = Color.LightSkyBlue;
                foreach (int hijoindex in this.children)
                {
                    Control hijo = (from Control _botonhijo in this.Parent.Controls where _botonhijo.Name == hijoindex.ToString() select _botonhijo).First();
                    ((BotonDesplegar)hijo).Replegar();
                    ((BotonDesplegar)hijo).Hide();
                }
            }
            return desplegado;
        }

        public void Replegar()
        {
            if (desplegado)
                CambiarEstado();
        }

        private void Desplegar()
        {
            desplegado = true;
            this.Show();
        }

        private void CalcularPosicion()
        {
            int AltoPantalla = this.Parent.Height;
            int AnchoPantalla = this.Parent.Width;
            if (parentid == null) //posicion inicial
                this.Location = new System.Drawing.Point(50, 50);
            else
            {
                var ListaBotonesDesplegados= (this.Parent.Controls.Find(parentid.ToString(),true).ToList());
                BotonDesplegar botonpadre = (from BotonDesplegar boton in ListaBotonesDesplegados where boton.nodoid == parentid select boton).First();
                this.Location = new System.Drawing.Point(botonpadre.Location.X+100, botonpadre.Location.Y+((this.orden-1) * 50));
            }

        }
        public void Posicionar()
        {
            CalcularPosicion();
            PosicionarLabel();
        }
        public void AddChildren(BotonDesplegar botonhijo)
        {
            if (!this.children.Contains(botonhijo.nodoid))
            { 
                this.children.Add(botonhijo.nodoid);
                botonhijo.parentid = this.nodoid;
                botonhijo.nivelprofundidad = this.nivelprofundidad + 1;
                botonhijo.orden = this.children.Count;
                botonhijo.Posicionar();
            } //AQUI PONER SI EL HIJO YA EXISTE.
        }

    }
}
