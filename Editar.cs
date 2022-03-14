using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMA_ModelEditor
{
    public partial class Editar : Form
    {
        //public string NodoTextValue { get; set; }
        public List<string> ListaDatos;
        private List<string> ListaDatosTemp =new List<string>();
        private Coordenada coordenada,coordenadaNodo2;
        public List<Coordenada> ListaCoordenadas1 = null; 
        private List<Coordenada> ListaCoordenadas2 = null;
        int controladordenodo;
        public Editar(Coordenada _Coordenada, Coordenada _CoordenadaNodo2)//=null)
        {
            InitializeComponent();
            coordenada= _Coordenada;
            coordenadaNodo2 = _CoordenadaNodo2;
            panelListaNodos.Visible = false;

            if (coordenadaNodo2 == null)
            {
                panelNodo2.Visible = false;
                txtArbol2.Visible = false;
                btnCopyToOne.Visible = false;
            }
        }

        public Editar(List<Coordenada> _ListaCoordenadas1,  List<Coordenada> _ListaCoordenadas2)
        {
            InitializeComponent();
            panelListaNodos.Visible = true;
            ListaCoordenadas1 = _ListaCoordenadas1;
            ListaCoordenadas2 = _ListaCoordenadas2;
            //coordenada = _ListaCoordenadas1.First();
            //coordenadaNodo2 = _ListaCoordenadas2.First();
            MessageBox.Show("Opcion por implementar");
            BtnSaveCoord.Text = "Save node info";

            int contador = 0;
            foreach (Coordenada coord in ListaCoordenadas1)
            {
                Boton boton = CrearBoton3(contador+":"+coord.ToString(), contador++);
                boton.Dock = DockStyle.Top;
                panelListaNodos.Controls.Add(boton);
            }

        }
        private void Editar_Load(object sender, EventArgs e)
        {
            if (ListaCoordenadas1 == null)
            {
                CargarInformacionNodosUnitarios();
                btnClose.Parent = panel3; ;
                BtnSaveCoord.Parent = panel3;
                panelListaNodos.Visible = false;
                BtnModifyChildren.Visible = false;
            }

        }

        private void CargarInformacionNodosUnitarios()
        {
            int contador = 0;
            
            if (coordenada.vacia) { txtCadena.Text = "Empty";return; }

            foreach (string linea in coordenada.ListaDatos)
            {
                ListaDatosTemp.Add(linea);
                Boton boton = CrearBoton(linea, contador);
                contador++;
                PanelDeBotons.Controls.Add(boton);
            }
            if (coordenada.haschildren)
                foreach (int hijo in coordenada.children)
                    listView1.Items.Add(hijo.ToString());

            if (coordenadaNodo2 != null)
            {
                if (coordenadaNodo2.vacia) { txtArbol2.Text = "Empty"; }
                else
                { 
                    contador = 0;
                    foreach (string linea in coordenadaNodo2.ListaDatos)
                    {
                        //ListaDatosTemp2.Add(linea);
                        Boton boton = CrearBoton2(linea, 1000000 + contador); //El nombre que se le asigna es: boton.Name = "btn" + nodoid;
                        contador++;
                        PanelBotones2.Controls.Add(boton);
                    }
                }
            }

        }

        private Boton CrearBoton3(string cadena, int contador)
        {
            Boton boton = new Boton(contador);
            boton.IsInfo = false;
            boton.Text = cadena;
            boton.Click += (s, args) => { ControladorBoton3(s, args, boton); };
            return boton;
        }

        private Boton CrearBoton(string cadena,int contador)
        {
            Boton boton = new Boton(contador);
            boton.IsInfo = false;
            boton.Text = cadena;
            boton.Click += (s, args) => { ControladorBoton(s, args, boton); };
            return boton;
        }
        private Boton CrearBoton2(string cadena, int contador)
        {
            Boton boton = new Boton(contador);
            boton.IsInfo = false;
            boton.Text = cadena;
            boton.Click += (s, args) => { ControladorBoton2(s, args, boton); };
            return boton;
        }


        private void ControladorBoton3(object sender, EventArgs args, Boton boton)
        {
            //Vaciar todos los elementos de los paneles;
            while (PanelDeBotons.Controls.Count > 0)
                foreach (Control control in PanelDeBotons.Controls) control.Dispose();
            while (PanelBotones2.Controls.Count > 0)
                foreach (Control control in PanelBotones2.Controls) control.Dispose();

            listView1.Items.Clear();

            //Asignar las variables correspondientes 
            ListaDatos = new List<string>();
            ListaDatosTemp = new List<string>();
            controladordenodo = 0;

            coordenada = ListaCoordenadas1[boton.nodoid];
            coordenadaNodo2 = ListaCoordenadas2[boton.nodoid];
            CargarInformacionNodosUnitarios();
            //llamar al inicializador de comienzo del fomulario en modo unitario.

            boton.BackColor = Color.LimeGreen;
        }
        private void ControladorBoton2(object sender, EventArgs args, Boton boton)
        {
            QuitarColorBotones2();
            txtArbol2.Text = boton.Text;
            boton.BackColor = Color.LimeGreen;
        }
        private void ControladorBoton(object s, EventArgs args, Boton boton)
        {
            QuitarColorBotones();
            controladordenodo = boton.nodoid;
            txtCadena.Text = ListaDatosTemp[controladordenodo];
            boton.BackColor = Color.LimeGreen;
            if (coordenadaNodo2 != null)
            {
                Control boton1=null;
                try
                { 
                    boton1 = PanelBotones2.Controls.Find("btn" + (1000000 + boton.nodoid).ToString(), true).First();
                    ControladorBoton2(this, args, (Boton)boton1);
                }
                catch (Exception ex) { }
                
                
                
            }
        }

        private void QuitarColorBotones()
        {
            foreach (Control control in PanelDeBotons.Controls) control.BackColor = Color.White;
        }
        private void QuitarColorBotones2()
        {
             foreach (Control control in PanelBotones2.Controls) control.BackColor = Color.White;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDiscardData_Click_1(object sender, EventArgs e)
        {
            txtCadena.Text = ListaDatosTemp[controladordenodo];
        }

        private void btnCopyToOne_Click(object sender, EventArgs e)
        {
            if (txtCadena.Text != null & txtCadena.Text != "")
            {
                Boton boton =null;
                foreach(Control control in PanelBotones2.Controls)
                {
                    if (control.BackColor == Color.LimeGreen) { boton = (Boton)control;}
                }
                if (boton == null) { MessageBox.Show("Operation not allowed"); return; }
                if (controladordenodo == (boton.nodoid - 1000000))
                {
                    txtCadena.Text = txtArbol2.Text;
                    btnSaveData_Click_1(sender, e);
                }
                else MessageBox.Show("Not the same string to be overwrited");

            }
            else MessageBox.Show("You did not selected an option to be overwrited");
        }

        private void btnSaveData_Click_1(object sender, EventArgs e)
        {
            ListaDatosTemp[controladordenodo] = txtCadena.Text;
        }

        private void BtnSaveCoord_Click_1(object sender, EventArgs e)
        {
            if (ListaCoordenadas1 == null)
            {
                DialogResult res = MessageBox.Show("Are you sure? This action cannot be reversed without reloading", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    ListaDatos = ListaDatosTemp;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Comprobar que se salvan bien");
                //Borrar y añadir o solo modificar la lista de datos????
                ListaDatos = ListaDatosTemp;
                coordenada.ListaDatos = ListaDatos;
            } 
        }

    }
}
