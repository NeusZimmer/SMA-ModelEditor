using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlPanelVisual;

namespace SMA_ModelEditor
{
    public partial class PanelVisualizadorArbol : UserControl
    {

        List<int> ListaNiveles = new List<int>();
        //List<BotonVisualizadorArbol> listabotones = new List<BotonVisualizadorArbol>();
        public Arbol ArbolDeNodos;
        public PanelVisualizadorArbol()
        {
            InitializeComponent();
            panelKeyValue.Visible = false;
        }
        public void ProcesarArbol()
        {
            BotonVisualizadorArbol boton = CrearBotonDeCoordenada(ArbolDeNodos.Find(ArbolDeNodos.comienzo));
            AddTreeNode(boton, 1);
        }

        private BotonVisualizadorArbol CrearBotonDeCoordenada(Coordenada coordenada)
        {
            BotonVisualizadorArbol boton = new BotonVisualizadorArbol(coordenada.id);
            boton.parentid = coordenada.parent;
            if (boton.parentid == -1) boton.orden = 0;
            boton.children = coordenada.children;
            boton.clickBoton += LanzadorClickBoton;
            //boton.Cadena = "{"+ boton.nodoid + "}:"+coordenada.ToString();
            //boton.Cadena = coordenada.ToString();
            //boton.Cadena = "FinalizaVacia:" + coordenada.FinalizaCadenaVacia.ToString() + ":" + coordenada.ToString();
            //boton.Cadena = "m_id:" + coordenada.m_id.ToString() + "Keyvalue:"+coordenada.keyvalue.ToString() + coordenada.ToString();
            boton.Cadena = coordenada.ToString(true);
            //boton.AutoSize = true;
            //boton.Dock=DockStyle.Fill;
            return boton;

        }

        private void AddTreeNode(BotonVisualizadorArbol boton, int posicion)
        {
            PanelNivelVisualDatos panelnivel = SeleccionarPanel(boton.orden);

            panelnivel.Visible = true;
            panelnivel.Maximize();
            panelnivel.SetSize(1);
            panelnivel.Add(boton, posicion);
        }

        private PanelNivelVisualDatos CrearPanelNivel(int orden)
        {
            PanelNivelVisualDatos panel = new PanelNivelVisualDatos();
            panel.Dock = System.Windows.Forms.DockStyle.Left;
            panel.Location = new System.Drawing.Point(0, 0);
            panel.Name = string.Format("P{0}", orden);
            panel.AutoSize = true;

            return panel;
        }
        //private Panel CrearPanelNivel(int orden)
        //{
        //    Panel panel = new Panel();
        //    panel.Dock = System.Windows.Forms.DockStyle.Left;
        //    panel.Location = new System.Drawing.Point(0, 0);
        //    panel.Name = string.Format("P{0}",orden);
        //    panel.AutoSize = true;
        //    panel.AutoScroll = false;
        //    panel.HorizontalScroll.Enabled = false;
        //    panel.HorizontalScroll.Visible = false;
        //    panel.HorizontalScroll.Maximum = 1;

        //    panel.VerticalScroll.Enabled = true;
        //    panel.VerticalScroll.Visible = true;
        //    panel.AutoScroll = true;
        //    return panel;
        //}

        private void LanzadorClickBoton(BotonVisualizadorArbol boton)
        {
            if (boton.desplegado)
            {
                //CambiarEstadoRestoBotonesDelPanel(boton);//puede ser innecesario
                ReplegarPaneles(boton.orden + 1);

            }
            else
            {
                CambiarEstadoRestoBotonesDelPanel(boton);
                ReplegarPaneles(boton.orden + 1);
                BotonDesplegarHijos(boton);
            }

        }
        private void ReplegarPaneles(int nivelprofundidadaborrar)
        {
            panelKeyValue.Visible = false;
            foreach (int indice in ListaNiveles)
            {
                if (indice >= nivelprofundidadaborrar)
                {
                    PanelNivelVisualDatos panelnivel = (PanelNivelVisualDatos)(this.Controls.Find(string.Format("P{0}", indice), true).First());
                    panelnivel.Visible = false;
                    //panelnivel.SuspendLayout();
                    //panelnivel.ControlsClear();
                    //panelnivel.ResumeLayout();
                    //panelnivel.Maximize();
                }
            }

        }
        //private void ReplegarPaneles(int nivelprofundidadaborrar)
        //{
        //    panelKeyValue.Visible = false;
        //    foreach (int indice in ListaNiveles)
        //    {
        //        if (indice >= nivelprofundidadaborrar)
        //        {
        //            Panel panelnivel = (Panel)(this.Controls.Find(string.Format("P{0}", indice), true).First());
        //            panelnivel.Visible = false;
        //            panelnivel.SuspendLayout();
        //            panelnivel.Controls.Clear();
        //            panelnivel.ResumeLayout();
        //            MaximizarPanel(panelnivel);
        //        }
        //    }

        //}
        private void BotonDesplegarHijos(BotonVisualizadorArbol boton)
        {
            try
            {
                if (boton.children.Count == 0) return;
            }
            catch { return; }

            Coordenada coordenadapadre = ArbolDeNodos.Find(boton.nodoid);
            if (coordenadapadre.keyvalue)
            {
                MostrarPanelKeyValues(coordenadapadre);

            }

            else if (boton.children.Count > 200)
            {
                MostrarListadosGrandes(boton, coordenadapadre);
            }
            else
            {
                PanelNivelVisualDatos panelnivel = SeleccionarPanel(boton.orden + 1);
                panelnivel.ControlsClear();
                panelnivel.Visible = true; //tiene que ser visible para que el tamaño se ajuste bien.
                panelnivel.Maximize();
                panelnivel.Visible = false;
                panelnivel.SetSize(boton.children.Count);
                int contador = 1;
                List<Control> controles = new List<Control>();
                foreach (int indicehijo in boton.children)
                {
                    Coordenada coordenadahijo = ArbolDeNodos.Find(indicehijo);
                    if (!coordenadahijo.FinalizaCadenaVacia)
                    {
                        while (coordenadahijo.passtrough)
                            coordenadahijo = ArbolDeNodos.Find(coordenadahijo.children[0]);

                        BotonVisualizadorArbol botonjijo = CrearBotonDeCoordenada(coordenadahijo);
                        botonjijo.AddParent(boton);
                        //panelnivel.Add(botonjijo, contador);
                        controles.Add(botonjijo);
                    }
                    contador++;
                }
                panelnivel.Add(controles);
                panelnivel.Visible = true;
            }
        }
        //private void BotonDesplegarHijos(BotonVisualizadorArbol boton)
        //{
        //    try
        //    {
        //        if (boton.children.Count == 0) return;
        //    }
        //    catch { return; }

        //    Coordenada coordenadapadre = ArbolDeNodos.Find(boton.nodoid);
        //    if (coordenadapadre.keyvalue)
        //    {
        //        MostrarPanelKeyValues(coordenadapadre);

        //    }
        //    else if(boton.children.Count>24)
        //        {
        //        MessageBox.Show("Son muchos");
        //        flowListaGrande.BringToFront();
        //        flowListaGrande.Visible = true;
        //        }
        //        else
        //        {
        //        //Panel panelnivel = SeleccionarPanel(boton.orden + 1);
        //            PanelNivelVisualDatos panelnivel = SeleccionarPanel(boton.orden + 1);
        //           // panelnivel.SuspendLayout();
        //            //panelnivel.Controls.Clear();
        //            panelnivel.ControlsClear();
        //        //PonerBotonAlPanel(panelnivel);
        //        panelnivel.Maximize();
        //        panelnivel.SetSize(boton.children.Count);
        //        int contador = 1;
        //            foreach (int indicehijo in boton.children)
        //            {
        //                Coordenada coordenadahijo = ArbolDeNodos.Find(indicehijo);
        //                if (!coordenadahijo.FinalizaCadenaVacia)
        //                {
        //                    while (coordenadahijo.passtrough)
        //                        coordenadahijo = ArbolDeNodos.Find(coordenadahijo.children[0]);


        //                    BotonVisualizadorArbol botonjijo = CrearBotonDeCoordenada(coordenadahijo);
        //                    botonjijo.AddParent(boton);
        //                    //panelnivel.Controls.Add(botonjijo);
        //                    panelnivel.Add(botonjijo,contador);
        //            }
        //            contador++;
        //            }
        //            //contador = 0;
        //            //foreach (BotonVisualizadorArbol botonposicionar in panelnivel.Controls.OfType<BotonVisualizadorArbol>()) botonposicionar.Posicionar(contador++);

        //           // panelnivel.ResumeLayout();
        //            panelnivel.Show();
        //        }
        //}


        private void MostrarListadosGrandes(BotonVisualizadorArbol boton, Coordenada coordenadapadre)
        {
            bool buscar = true;
            //bool apariencia = false, personalidad = false;
            Coordenada temp = coordenadapadre;
            List<string> listaseparada=null;
            while (buscar)
            {
                if (temp.ToString().ToLower().Contains("apariencia"))
                {
                    //apariencia = true;
                    buscar = false;
                    listaseparada = new List<string>() { "seno", "vag", "coloreador", "scaler", "desplazador", "textureador", "face", "morpher" };
                }
                if (temp.ToString().ToLower().Contains("personalidad"))
                {
                    //personalidad = true;
                    buscar = false;
                    listaseparada = new List<string>() { 
                         "Rasgo", "traithumano", "consent", "Privacidad_visual",
                        "Privacidad", "erogenidad", "sensibilidad", "max_emocion",
                        "EstGen", "ConcentRequerido", "Exp_InterPorGrupo", "Inc_InterPorGrupo",
                        "DatosDeUmbral_Placer", "DatosDeUmbral_Dolor", "DatosDeUmbral_Rage" };


                }
                else temp = ArbolDeNodos.Find(temp.parent);
                if (temp.parent == -1) break;
            }

            //if (apariencia) 

            //if (personalidad) MessageBox.Show("N/A");

            MostrarListadosSeparadosEnLista(boton, coordenadapadre, listaseparada);

        }
        private void MostrarListadosSeparadosEnLista(BotonVisualizadorArbol boton, Coordenada coordenadapadre, List<string> listaseparada)
        {
            List<Coordenada> listahijos = new List<Coordenada>();
            foreach (int children in boton.children)
                listahijos.Add(ArbolDeNodos.Find(children));

            //MessageBox.Show("Son muchos");
            
            List<List<int>> ListaBotonesReducida = new List<List<int>>();
            foreach (string separador in listaseparada)
            {
                List<int> listatemp = new List<int>();
                var lista = (from coordenada in listahijos where coordenada.ToString().ToLower().Contains(separador.ToLower()) orderby coordenada.ToString() ascending select  coordenada );
                foreach (Coordenada coordenada in lista.ToList())
                {
                    listatemp.Add(coordenada.id);
                    listahijos.Remove(coordenada);
                }
                ListaBotonesReducida.Add(listatemp);
            }
            List<int> listatemp2 = new List<int>();
            foreach (Coordenada coordenada in listahijos)
            {
                listatemp2.Add(coordenada.id);
            }
            ListaBotonesReducida.Add(listatemp2);
            PanelNivelVisualDatos panelnivel = SeleccionarPanel(boton.orden + 1);
            List<Control> listabotones = new List<Control>();
            listaseparada.Add("Rest");
            int contador = 0;
            foreach (List<int> listahijos2 in ListaBotonesReducida)
            {
                Coordenada coordenadahijo2 = new Coordenada(coordenadapadre.comienzo, coordenadapadre.final, coordenadapadre.id);
                coordenadahijo2.children = listahijos2;
                coordenadahijo2.desplegado = true;
                coordenadahijo2.parent = coordenadapadre.parent;
                coordenadahijo2.ListaDatos = new List<string>(); ;
                coordenadahijo2.ListaDatos.Add(listaseparada[contador++]);
                BotonVisualizadorArbol botonjijo = CrearBotonDeCoordenada(coordenadahijo2);
                botonjijo.AddParent(boton);
                listabotones.Add(botonjijo);
            }
            panelnivel.Add(listabotones);
            panelnivel.Visible = true;
            //flowListaGrandeApariencia.BringToFront();
            //flowListaGrandeApariencia.Visible = true;

        }


        private void MostrarPanelKeyValues(Coordenada coordenadapadre)
        {
            panelKeyValue.Visible = true;
            panelKeyValue.BringToFront();
            KeyValue keyvaluecontrol = (KeyValue)panelKeyValue.Controls.Find("keyValuecontrol", true).First();
            keyvaluecontrol.Clear();
            Coordenada coordenadahijokeys,coordenadahijovalues;
            coordenadahijokeys = ArbolDeNodos.Find(coordenadapadre.children[1]);
            coordenadahijovalues= ArbolDeNodos.Find(coordenadapadre.children[0]);

            while (coordenadahijokeys.haschildren)
                coordenadahijokeys = ArbolDeNodos.Find(coordenadahijokeys.children[0]);
            while (coordenadahijovalues.haschildren)
                coordenadahijovalues = ArbolDeNodos.Find(coordenadahijovalues.children[0]);

            keyvaluecontrol.AddKeys(coordenadahijokeys);
            keyvaluecontrol.AddValues(coordenadahijovalues);

            keyvaluecontrol.Check(); //pensar si poner o no
        }




        private void PonerBotonAlPanel(Panel panelnivel) //innecesaria
        {
            Button contraerpanel = new Button();
            contraerpanel.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            contraerpanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            contraerpanel.FlatAppearance.BorderSize = 0;
            contraerpanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            contraerpanel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            contraerpanel.UseVisualStyleBackColor = false;

            contraerpanel.Text = "";// "BotonContraer:" + panelnivel.Name;
            contraerpanel.Dock = System.Windows.Forms.DockStyle.Top;
            contraerpanel.Visible = true;
            contraerpanel.Enabled = true;
            contraerpanel.Click += (s, args) => ContraerExtenderPanel(s, args, panelnivel);
            panelnivel.Controls.Add(contraerpanel);
        }

        private void ContraerExtenderPanel(object s, EventArgs args, Panel panelnivel)
        {
            panelnivel.AutoSize = true;
            panelnivel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Button boton = (Button)s;
            if (panelnivel.Size.Width > 25) 
            {
                MinimizarPanel(panelnivel,boton);
            }
            else 
            {
                MaximizarPanel(panelnivel, boton);
            }
        }
        private void MinimizarPanel(Panel panelnivel,Button boton=null)
        {
            if (boton == null) boton = panelnivel.Controls.OfType<Button>().First();

            panelnivel.MaximumSize = new Size(25, panelnivel.MaximumSize.Height);
            //boton.MaximumSize= new Size(25, 25);
            boton.TextAlign = ContentAlignment.MiddleLeft;
            boton.Text = ""; //">";
            boton.Dock = DockStyle.Fill;

            boton.BringToFront();

        }
        private void MaximizarPanel(Panel panelnivel, Button boton= null)
        {
            try
            {
                if (boton == null) boton = panelnivel.Controls.OfType<Button>().First();
            }
            catch { return; }
            panelnivel.MaximumSize = new Size(999, panelnivel.MaximumSize.Height);
            boton.TextAlign = ContentAlignment.MiddleRight;
            //boton.MaximumSize = new Size(999, 25);
            boton.Text = "";//"<";
            boton.Dock = DockStyle.Top;
            boton.SendToBack();
        }

        private PanelNivelVisualDatos SeleccionarPanel(int NivelDeProfundidad)
        {
            PanelNivelVisualDatos panelnivel;
            if (!ListaNiveles.Contains(NivelDeProfundidad))
            {
                panelnivel = CrearPanelNivel(NivelDeProfundidad);
                //panelnivel.AutoSize = true;
                //panelnivel.Name = "P" + NivelDeProfundidad;
                //PonerBotonAlPanel(panelnivel);
                ListaNiveles.Add(NivelDeProfundidad);
                panelnivel.Maximize();
                this.Controls.Add(panelnivel);
                panelnivel.BringToFront();
            }
            else
            {
                panelnivel = (PanelNivelVisualDatos)(this.Controls.Find(string.Format("P{0}", NivelDeProfundidad), true).First());
            }

            return panelnivel;
        }

        //private Panel SeleccionarPanel(int NivelDeProfundidad)
        //{
        //    Panel panelnivel;
        //    if (!ListaNiveles.Contains(NivelDeProfundidad))
        //    {
        //        panelnivel = CrearPanelNivel(NivelDeProfundidad);
        //        PonerBotonAlPanel(panelnivel);
        //        ListaNiveles.Add(NivelDeProfundidad);
        //        this.Controls.Add(panelnivel);
        //        panelnivel.BringToFront();
        //    }
        //    else
        //    {
        //        panelnivel = (Panel)(this.Controls.Find(string.Format("P{0}", NivelDeProfundidad), true).First());
        //    }

        //    return panelnivel;
        //}

        private void CambiarEstadoRestoBotonesDelPanel(BotonVisualizadorArbol boton)
        {
            foreach (BotonVisualizadorArbol botoninactivo in boton.Parent.Controls.OfType<BotonVisualizadorArbol>())
            {
                if ((botoninactivo.desplegado) & (botoninactivo!=boton)) botoninactivo.CambiarEstado();
            }
        }


    }
}
