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
    public partial class Edit_Tree_Blocks : Form
    {
        private Form1 formularioprincipal;
        Coordenada coordenadapadre1=null;
        Coordenada coordenadapadre2=null;
        Arbol ArbolDeNodos = new Arbol();
        Arbol Arbol2DeNodos = new Arbol();
        List <int> ListaNodosActivados=new List<int>();

        #region rutinas internas

        public Edit_Tree_Blocks(Form1 _formulario)
        {
            InitializeComponent();
            formularioprincipal = _formulario;
        }
        private void Edit_Tree_Blocks_Load(object sender, EventArgs e)
        {
            
            //Creo una copia para evitar modificar el arbol original y poder descartar los cambios.
            ArbolDeNodos = (Arbol)formularioprincipal.ArbolDeNodos.Clone();
            Arbol2DeNodos = (Arbol)formularioprincipal.Arbol2DeNodos.Clone();

            
            if (ArbolDeNodos.Count > 0) coordenadapadre1 = ArbolDeNodos.Find(ArbolDeNodos.comienzo);
            if (Arbol2DeNodos.Count > 0) coordenadapadre2 = Arbol2DeNodos.Find(Arbol2DeNodos.comienzo);
            btnCopiarListas.Visible = true;
            panelIzdo.AutoSize = true;
            try
            {
                ActualizarInformacion();
            }
            catch (Exception ex) { MessageBox.Show("Error: Data not found"); };
        }

        private void ActualizarInformacion()
        {
            label4.Text = string.Format("INFORMATION OF SELECTED ROOT NODE, ID:{0} ", coordenadapadre1.id);
            lblDataString1.Text = coordenadapadre1.ToString();
            string cadena = "";
            if (coordenadapadre1.haschildren)
            {
                cadena = string.Format("{0} childrens:", coordenadapadre1.children.Count);
                foreach (int hijo in coordenadapadre1.children) cadena += hijo.ToString() + ",";
                lblChildersList1.Text = cadena.Substring(0, cadena.Length - 1);
            }
            else lblChildersList1.Text = "No childrens";
            lblSize1.Text = string.Format("There are {0} total nodes", ArbolDeNodos.Size(coordenadapadre1.id)); ;
        }

        private void ActualizarInformacion2()
        {
            label14.Text = string.Format("INFORMATION OF SELECTED ROOT NODE, ID:{0} ", coordenadapadre2.id);
            lblDataString2.Text = coordenadapadre2.ToString();
            string cadena = "";
            if (coordenadapadre2.haschildren)
            {
                cadena = string.Format("{0} childrens:", coordenadapadre2.children.Count);
                foreach (int hijo in coordenadapadre2.children) cadena += hijo.ToString() + ",";
                lblChildersList2.Text = cadena.Substring(0, cadena.Length - 1);
            }
            else lblChildersList2.Text = "No childrens";
            lblSize2.Text = string.Format("There are {0} total nodes", Arbol2DeNodos.Size(coordenadapadre2.id)); ;
        }


        #endregion

        #region Botones acciones genericas
        private void btnSaveAndExit_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure? This action cannot be reversed without reloading", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                //Salvar los datos actuales;
                formularioprincipal.ArbolDeNodos = ArbolDeNodos;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLoadBodyData_Click(object sender, EventArgs e)
        {
            int PhysicalBody = 15;
            if (ArbolDeNodos.Count > 0) coordenadapadre1 = ArbolDeNodos.Find(PhysicalBody);
            if (Arbol2DeNodos.Count > 0) coordenadapadre2 = Arbol2DeNodos.Find(PhysicalBody);

            try
            {
                ActualizarInformacion();
            }
            catch (Exception ex) { MessageBox.Show("Error: Data not found"); };
        }

        private void btnLoadPersonality_Click(object sender, EventArgs e)
        {
            int Personality = 2062;
            if (ArbolDeNodos.Count > 0) coordenadapadre1 = ArbolDeNodos.Find(Personality);
            if (Arbol2DeNodos.Count > 0) coordenadapadre2 = Arbol2DeNodos.Find(Personality);

            try
            {
                ActualizarInformacion();
            }
            catch (Exception ex) { MessageBox.Show("Error: Data not found"); };

        }

        #endregion

        #region Botones de accion entre arboles
        private void BtnCompareTrees_Click(object sender, EventArgs e)
        {
            bool resultado = ArbolDeNodos.Compare(coordenadapadre1.id, Arbol2DeNodos);

            if (resultado) lblComparison.Text = "Source node could fit into destination with no adaptations";
            else lblComparison.Text = "Source node will not fit into destination without adaptations";
        }
        private void lblChildrenDiferences1_Click(object sender, EventArgs e)
        {
            string cadena = "Nodes not found in SOURCE FILE: ";
            if (coordenadapadre1.haschildren)
                foreach (int hijo in coordenadapadre1.children)
                    if (!coordenadapadre2.children.Contains(hijo)) cadena += hijo.ToString() + ",";
            lblChildreDiferencies1.Text = cadena.Substring(0, cadena.Length - 1);


            cadena = "Nodes not found in EDITED FILE: ";
            if (coordenadapadre2.haschildren)
                foreach (int hijo in coordenadapadre2.children)
                    if (!coordenadapadre1.children.Contains(hijo)) cadena += hijo.ToString() + ",";
            lblChildreDiferencies2.Text = cadena.Substring(0, cadena.Length - 1);
        }


        #endregion

        #region Botones fichero destino
        private void button3_Click(object sender, EventArgs e) //Boton para editar el nodo de destino y ajustar 
        {
            using (var form = new Editar(coordenadapadre1, coordenadapadre2))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> ListaDatos = form.ListaDatos;
                    coordenadapadre1.ListaDatos = ListaDatos;
                }
                //else MessageBox.Show("No salvado:");
            }
        }

        private void btnLoad1_Click(object sender, EventArgs e)
        {
            try
            {
                coordenadapadre1 = ArbolDeNodos.Find(int.Parse(txtNodoArbol1.Text));
                ActualizarInformacion();
                lblComparison.Text = "Root node changed, please check again";
            }
            catch (Exception ex) { MessageBox.Show("Error: Data not found/wrong node ID"); };
        }



        #endregion

        #region Botones fichero Origen
        private void btnLoad2_Click(object sender, EventArgs e)
        {
            try
            {
                coordenadapadre2 = Arbol2DeNodos.Find(int.Parse(txtNodoArbol2.Text));
                ActualizarInformacion2();
            }
            catch (Exception ex) { MessageBox.Show("Error: Data not found/wrong node ID"); };
        }






        #endregion

        private void btnCopySourceIntoDestination_Click(object sender, EventArgs e)
        {
            bool resultado = ArbolDeNodos.Compare(coordenadapadre1.id, Arbol2DeNodos);

            if (!resultado) {MessageBox.Show("Source node tree does not fit into destination with no adaptations, and is not currently implemented"); return; }
            else 
            //Atencion la sustitucion es la mas simple, podria ser diferente solo si no son iguales, crear otro inicializador del metodo con un segundo int?
            ArbolDeNodos.Replace(coordenadapadre1.id,Arbol2DeNodos);
            //Arbol2DeNodos.Replace(coordenadapadre1.id, ArbolDeNodos); //lo doy la vuelta para comprobarlo

            //MessageBox.Show("Esperar");
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

            string texto = "";

            using (var form = new Form2("Text to search in node tree",1))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    texto = form.ReturnValue;
                }
                else return;
            }
            PanelListaBusqueda.SuspendLayout();
            while (PanelListaBusqueda.Controls.Count > 0)
                foreach (Control control in PanelListaBusqueda.Controls) control.Dispose();
            PanelListaBusqueda.ResumeLayout();
            this.panelIzdo.Size = new System.Drawing.Size((this.Size.Width - (this.Size.Width % 3) / 3), 536);
            btnCopiarListas.Visible = true;
            panelIzdo.AutoSize = true;


            var lista = ArbolDeNodos.FindText(texto);

            if (lista != null)
            {
                panelIzdo.Width = 250;

                //Mostrar en un panel?
                PanelListaBusqueda.SuspendLayout();
                //DialogResult pregunta=MessageBox.Show((string.Format("Found:{0} nodes containing:{1}, Want to show them?", lista.Count, texto)), "Atention", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                DialogResult pregunta = MessageBox.Show((string.Format("Found:{0} nodes containing:{1}, Want to show them?", lista.Count, texto)), "Atention", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                // MessageBox.Show("Are you sure? This action cannot be reversed without reloading", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (pregunta != DialogResult.OK) return;

                foreach (Coordenada _coordenada in lista)
                {
                    Boton _boton = new Boton(_coordenada.id);
                    _boton.Text = "Node:" +_coordenada.id.ToString() + ":" + _coordenada.ToString();
                    PanelListaBusqueda.Controls.Add(_boton);
                    _boton.Click += (s, a) => { ControladorBotonLista(s,a,_boton); };
                }
                PanelListaBusqueda.ResumeLayout();
            }
            else MessageBox.Show("No nodes found");

        }

        private void ControladorBotonLista(object s, EventArgs a, Boton boton)
        {
            if (boton.BackColor != Color.LimeGreen)
            {
                boton.BackColor = Color.LimeGreen;
                ListaNodosActivados.Add(boton.nodoid);
            }
            else
            {
                boton.BackColor = Color.Transparent;
                ListaNodosActivados.Remove(boton.nodoid);
            }
            
        }

        private void btnCopiarListas_Click(object sender, EventArgs e)
        {
            foreach (int botonactivadoindex in ListaNodosActivados)
            {
                bool resultado = ArbolDeNodos.Compare(botonactivadoindex, Arbol2DeNodos);

                if (!resultado) { MessageBox.Show("Source node tree does not fit into destination with no adaptations, and is not currently implemented,doing nothing"); }
                else
                    ArbolDeNodos.Replace(botonactivadoindex, Arbol2DeNodos);
            }
            MessageBox.Show("FINISHED");
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
