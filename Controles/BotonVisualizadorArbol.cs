using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SMA_ModelEditor
{
    public partial class BotonVisualizadorArbol : UserControl
    {
        public int parentid;
        public int nodoid { get; private set; }
        public bool desplegado=false;
        public List<int> children = new List<int>();
        public int orden;
        public delegate void ClickBoton(BotonVisualizadorArbol boton);
        public ClickBoton clickBoton;
        public string Cadena
        {
            get
            {
                return this.Texto.Text;
            }
            set { Texto.Text = value; }
        }


        public BotonVisualizadorArbol()
        {
            InitializeComponent();
            Cadena = "";
        }

        public BotonVisualizadorArbol(int _nodoid)
        {
            InitializeComponent();
            Cadena = "";
            nodoid = _nodoid;
        }

        public void AddChildren(BotonVisualizadorArbol hijo)
        {
            if (!children.Contains(hijo.nodoid))
            {     
                children.Add(hijo.nodoid);
                hijo.parentid = this.nodoid;
                hijo.orden = this.orden + 1;
            }
        }
        public void AddParent(BotonVisualizadorArbol parent)
        {
            this.parentid = parent.nodoid;
            this.orden = parent.orden + 1;
        }

            public void Posicionar(int orden=0)
        {
            System.Drawing.Size tamañopanel = this.Parent.Size;
            int elementos=this.Parent.Controls.Count;
            int mitad = (tamañopanel.Height - (tamañopanel.Height % 2))/2;

            int mitadelementos = (elementos % 2 == 1) ? 
                ((elementos - 1) / 2) :
                ((elementos) / 2);

            int distanciaentrelementos = 35;
            int distanciadelcentro = (distanciaentrelementos*mitadelementos);
            this.Location = new System.Drawing.Point(0, (mitad-distanciadelcentro)+(distanciaentrelementos*orden) );

        }

        private void CambiarEstadoClick(object sender, EventArgs e)
        {
            clickBoton(this);
            CambiarEstado();
            //clickBoton(this);

            //this.desplegado = !desplegado;
            ////this.BotonDesplegar.Text = "-:" + nodoid.ToString();
            //if (desplegado)
            //{
            //    this.BotonDesplegar.Text = "-:";
            //    this.BotonDesplegar.BackColor = Color.LightSteelBlue;
            //    this.PanelMarcador.BackColor = Color.LightSteelBlue;
            //}
            //else 
            //{
            //    this.BotonDesplegar.Text = "+:";
            //    this.BotonDesplegar.BackColor = Color.LightSkyBlue;
            //    this.PanelMarcador.BackColor = Color.Transparent;
            //}
        }

        public void CambiarEstado()
        {
            this.desplegado = !desplegado;
            //this.BotonDesplegar.Text = "-:" + nodoid.ToString();
            if (desplegado)
            {
                this.BotonDesplegar.Text = "-:";
                this.BotonDesplegar.BackColor = Color.LightSteelBlue;
                this.PanelMarcador.BackColor = Color.LightSteelBlue;
            }
            else
            {
                this.BotonDesplegar.Text = "+:";
                this.BotonDesplegar.BackColor = Color.LightSkyBlue;
                this.PanelMarcador.BackColor = Color.Transparent;
            }
        }
    }
}
