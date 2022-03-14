using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMA_ModelEditor
{

    public partial class Form1 : Form
    {
        public Arbol ArbolDeNodos = new Arbol();
        public Arbol Arbol2DeNodos = new Arbol();

        string nombrefichero2="File Name 2";
        string label2titulo = "HEADER";

        string filename = "";
        Stream imagenpngfichero1 = new MemoryStream();
        Stream imagenpngfichero2 = new MemoryStream();
        List<string> CadenasNoDatos = new List<string>();
        private Encoding utf8 = new UTF8Encoding(true);
        //public delegate void EditarNodoDelegate(int nodoid);
        public Form1()
        {
            InitializeComponent();
            PanelDatosFlowOriginal3.Visible = false;
            
        }




        private void ButtonLoadFile1(object sender, EventArgs e)
        {
            string text = "";
            int posicion = 0;

            DialogResult pregunta = openFileDialog1.ShowDialog();
            if (pregunta == DialogResult.OK)
            {
                lblNombreFichero1.Text = openFileDialog1.FileName;
                //Leer imagen png

                byte[] ficherobinario = File.ReadAllBytes(openFileDialog1.FileName);
                byte[] DataStartSequence = utf8.GetBytes("TValle.Character.Data");

                int start = BuscarEnBinario(ficherobinario, DataStartSequence);
                if (start == -1)
                {
                    MessageBox.Show("Cannot  find TValle header");
                    return;
                }
                ArbolDeNodos = null;
                GC.Collect();
                ArbolDeNodos = new Arbol();


                //Leer parte de texto

                StreamReader fichero = new StreamReader(openFileDialog1.FileName);

                text = fichero.ReadToEnd();
                posicion = text.IndexOf("TValle.Character.Data");
                text = text.Substring(posicion);
                fichero.Close();
                CadenasNoDatos.Add("TValle.Character.Data:");



                //parte de carga de imagen desactivada temporalmente
                byte[] imagenpng1 = new byte[start];
                
                //imagenpngfichero1.Position = 0;
                Array.Copy(ficherobinario, 0, imagenpng1, 0, start);
                imagenpngfichero1.Write(imagenpng1, 0, imagenpng1.Length);
                Image imagen2 = Image.FromStream(imagenpngfichero1);
                pictureBox1.Image = imagen2;
                this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                //pictureBox1.Width = imagen2.Width;
                //pictureBox1.Height = imagen2.Height;



                AnalizarDatosArbolDeNodos(text);
                Coordenada raiz = ArbolDeNodos.Find(0);
                CadenasNoDatos.Add(text.Substring(raiz.final + 1));
                int id = CrearPanelBotones(0, 0);

                btnLoadFile1.Enabled = false;
            }
        }

        private int BuscarEnBinario(byte[] array, byte[] sequence)
        {
            if (sequence.Length == 0)
                return -1;

            int found = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == sequence[found])
                {
                    if (++found == sequence.Length)
                    {
                        return i - found + 1;
                    }
                }
                else
                {
                    found = 0;
                }
            }

            return -1;
        }

        private void FuncionMarcar(int nodoid)
        {
            MessageBox.Show("This option is currently not implemented");
        }

        private void FuncionEditar(int nodoid)
        {
            Coordenada coordenada=null, coordenada2 = null;
            try
            {
                coordenada = ArbolDeNodos.Find(nodoid);
                coordenada2 = Arbol2DeNodos.Find(nodoid);
            }
            catch (Exception ex) { }
            using (var form = new Editar(coordenada,coordenada2))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> ListaDatos = form.ListaDatos;
                    coordenada.ListaDatos = ListaDatos;
                    MessageBox.Show("Saved");
                }
                else MessageBox.Show("Not saved");
            }
        }
        private int CrearPanelBotones(int _PanelParentID, int _NodoId)
        {
            int PanelID = _PanelParentID + 1;
            PanelBotones panelBotones = new PanelBotones(_NodoId, _PanelParentID);
            panelBotones.BotonEditarDelegateCallback += new PanelBotones.BotonEditarDelegate(FuncionEditar);
            panelBotones.BotonMarcarDelegateCallback += new PanelBotones.BotonMarcarDelegate(FuncionMarcar);
            List<Boton> ListaBotones = CrearBotones(_NodoId);
            if (ListaBotones.Count > 10)
            {
                MessageBox.Show(string.Format("Too many Childrens:{0}, they will open in the below panel", ListaBotones.Count));
                PanelDatosFlowOriginal2.SuspendLayout();
                foreach (Boton _boton in ListaBotones)
                    PanelDatosFlowOriginal2.Controls.Add(_boton);
                //Añadir un boton de borrar el panel inferior.
                PanelDatosFlowOriginal2.ResumeLayout();
            }
            else
            {
                foreach (Boton _boton in ListaBotones)
                    panelBotones.Controles.Add(_boton);
                //Añadir un boton de enviar al panel inferior
            }
            PanelDatosFlowOriginal1.Controls.Add(panelBotones);
            return PanelID;
        }
        private Boton CrearBoton(int idnodo)
        {
            Boton boton = new Boton(idnodo);
            Coordenada nodoactual = ArbolDeNodos.Find(idnodo);
            boton.Text = nodoactual.ToString();
            boton.nodoid = idnodo;
            if (nodoactual.children != null)
            {
                boton.IsInfo = false;
                boton.Click += (s, args) => { ControladorBoton(s, args, boton); };
            }

            return boton;
        }
        private List<Boton> CrearBotones(int idnodo)
        {
            List<Boton> ListaBotones = new List<Boton>();
            Boton boton1 = new Boton(idnodo);
            Coordenada nodoactual = ArbolDeNodos.Find(idnodo);
            string retorno = nodoactual.ToString();

            if (nodoactual.ListaDatos != null)
            {
                foreach (string cadena in nodoactual.ListaDatos)
                {
                    Boton desactivado = new Boton(-1, nodoactual.id); desactivado.IsInfo = true;
                    desactivado.Text = cadena;
                    ListaBotones.Add(desactivado);
                }
                foreach (Boton _boton in ListaBotones)
                {
                    if (nodoactual.children != null)
                    {
                        foreach (int nodoid in nodoactual.children)
                        {
                            string marcador = "{" + nodoid + "}";
                            if ((_boton.Text).IndexOf(marcador) != -1)
                            {
                                _boton.IsInfo = false;
                                _boton.nodoid = nodoid;
                                _boton.Click += (s, args) => { ControladorBoton(s, args, _boton); };
                            }
                        }
                    }
                }

            }

            return ListaBotones;
        }
        private void ControladorBoton(object sender, EventArgs e, Boton _boton)
        {
            _boton.BackColor = Color.LightSeaGreen;
            PanelDatosFlowOriginal1.SuspendLayout();
            int NodoId = _boton.nodoid;
            int _PanelParentID = 0;
            if (_boton.Parent.Name == "PanelDatosFlowOriginal2")
            {
                if (PanelDatosFlowOriginal1.Controls.Count > 0)
                {
                    PanelBotones PanelSuperiorAnterior = (PanelBotones)PanelDatosFlowOriginal1.Controls[PanelDatosFlowOriginal1.Controls.Count - 1];
                    _PanelParentID = PanelSuperiorAnterior.parent + 1;
                    PanelSuperiorAnterior.children = CrearPanelBotones(_PanelParentID, NodoId);
                }
                else { CrearPanelBotones(0, NodoId); }

            }
            else {
                _PanelParentID = ((PanelBotones)(_boton.Parent).Parent).parent + 1;
                ((PanelBotones)(_boton.Parent).Parent).children = CrearPanelBotones(_PanelParentID, NodoId);
            }


            PanelDatosFlowOriginal1.ResumeLayout();

        }

        private void AnalizarDatosArbolDeNodos(string text)
        {
            Arbol nodoslista = new Arbol();
            Arbol nodosarray = new Arbol();

            //Crea la lista de elementos de {}            
            List<int> _aperturas = new List<int>();
            List<int> _cierres = new List<int>();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '{')
                {
                    _aperturas.Add(i);
                }
                else if (text[i] == '}')
                {
                    _cierres.Add(i);
                }
            }
            if (_aperturas.Count != _cierres.Count) throw new Exception("Error en el fichero de origen");
            nodoslista = CrearListaDeCoordenadas(_aperturas, _cierres);

            //Crea la lista de elementos de []
            List<int> aperturas = new List<int>();
            List<int> cierres = new List<int>();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '[')
                {
                    aperturas.Add(i);
                }
                else if (text[i] == ']')
                {
                    cierres.Add(i);
                }
            }
            if (aperturas.Count != cierres.Count) throw new Exception("Error en el fichero de origen");
            nodosarray = CrearListaDeCoordenadas(aperturas, cierres);
            foreach (Coordenada _coordenada in nodosarray) { _coordenada.IsArray = true; }

            ArbolDeNodos = nodoslista.Sumar(nodosarray);

            //Vacia la memoria
            _aperturas = null;
            aperturas = null;
            _cierres = null;
            cierres = null;
            nodoslista = null;
            nodosarray = null;
            GC.Collect();

            ArbolDeNodos.Arbolar();
            int vacias = ArbolDeNodos.EncontrarMarcarVacias();

            ArbolDeNodos.ExtraerDatos(0, text);//Empezar desde el nodo raiz=0;
            Coordenada raiz = ArbolDeNodos.Find(0);
            foreach (Coordenada nodo in ArbolDeNodos)
                nodo.desplegado = true;

        }

        private Arbol CrearListaDeCoordenadas(List<int> _aperturas, List<int> _cierres)
        {
            Arbol nodos = new Arbol();
            List<int> listacierres = _cierres;
            List<int> aperturas = _aperturas;
            aperturas.Reverse();
            listacierres.Reverse();
            //List<int> listacierres = cierres;
            foreach (int elemento in aperturas)
            {
                int elementoanterior = 0;
                foreach (int cierre in listacierres)
                {
                    if (cierre > elemento)
                    {
                        elementoanterior = cierre;
                    }
                    else break;
                }
                listacierres.Remove(elementoanterior);
                int indice = aperturas.Count - nodos.Count - 1;
                nodos.Add(new Coordenada(elemento, elementoanterior, indice));
            }
            return nodos;
        }


        private void LimpiarPanelInferior()
        {
            PanelDatosFlowOriginal2.SuspendLayout();
            while (PanelDatosFlowOriginal2.Controls.Count != 0)
                foreach (Boton boton in PanelDatosFlowOriginal2.Controls)
                    boton.Dispose();
            PanelDatosFlowOriginal2.ResumeLayout();
        }
        private void LimpiarPanelSuperior()
        {
            PanelDatosFlowOriginal2.SuspendLayout();
            while (PanelDatosFlowOriginal1.Controls.Count != 0)
                foreach (Control control in PanelDatosFlowOriginal1.Controls)
                    control.Dispose();
            PanelDatosFlowOriginal2.ResumeLayout();
        }
        private void btnBorrarNodo_Click(object sender, EventArgs e)
        {
            
            string texto = "";
            using (var form = new Form2("Node number to be deleted(including its childrens)"))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    texto = form.ReturnValue;
                    try
                    {
                        int nodoid = int.Parse(texto);
                        Coordenada coordenada = ArbolDeNodos.Find(nodoid);
                        EliminarDatosDeReferenciaEnElParent(coordenada);
                        ArbolDeNodos.BorrarNodoConHijos(nodoid);
                    }
                    catch (Exception exception) { MessageBox.Show("Wrong node ID"); }
                }
            }
        }

        private void EliminarDatosDeReferenciaEnElParent(Coordenada coordenadahijo)
        {
            Coordenada coordenadaparent = ArbolDeNodos.Find(coordenadahijo.parent);
            int? contadorindice=null;
            string marcador = "{" + coordenadahijo.id + "}";
            if (!coordenadaparent.haschildren) throw new Exception("Deleting a unreferenced node");
            int? referenciaaborrar=null;
            foreach (int children in coordenadaparent.children)
                if (children == coordenadahijo.id) referenciaaborrar = children;

            if (referenciaaborrar != null)
            { coordenadaparent.children.Remove((int)referenciaaborrar);

                int contador = 0; //string cadenacambiada;
                foreach (string cadena in coordenadaparent.ListaDatos)
                {
                    int index=cadena.IndexOf(marcador);
                    if (index != -1)
                    {
                        //string cadena2=cadena.Replace(marcador, "[DELETEDNODE]");
                        contadorindice = contador;
                    }
                    contador++;
                }
                    
            }
            else throw new Exception("Error: not in children list");
            if (contadorindice!=null)
                    coordenadaparent.ListaDatos[(int)contadorindice] = coordenadaparent.ListaDatos[(int)contadorindice].Replace(marcador, "[DELETEDNODE]");
            if (coordenadaparent.children.Count == 0) coordenadaparent.haschildren = false;
        }

        #region Botones
        private void btn_LoadRoot_Click(object sender, EventArgs e)
        {
            int id = CrearPanelBotones(0, ArbolDeNodos.comienzo);
        }

        private void btnSaveFile1_Click(object sender, EventArgs e)
        {
            string text = CadenasNoDatos[0] + ArbolDeNodos.ToString() + CadenasNoDatos[1];//+"}"
            DialogResult pregunta = saveFileDialog1.ShowDialog();
            if (pregunta == DialogResult.OK)
            {
                Stream fichero = saveFileDialog1.OpenFile();

                imagenpngfichero1.Position = 0;
                imagenpngfichero1.CopyTo(fichero);


                byte[] buffer2 = utf8.GetBytes(text);   //utf8.GetBytes(text, 0, text.Length, buffer2, 0);
                //MemoryStream texto = new MemoryStream();
                //texto.Write(buffer2, 0, text.Length);
                //texto.CopyTo(fichero);
                fichero.Write(buffer2, 0, buffer2.Length);
                //fichero.Write(buffer2, (int)fichero.Position, buffer2.Length);
                fichero.Close();

            }
        }

        private void btnPersonality_Click(object sender, EventArgs e)
        {
            int Personality = 2062;
            int id = CrearPanelBotones(0, Personality);
        }

        private void btnBody_Click(object sender, EventArgs e)
        {
            int PhysicalBody = 15;
            int id = CrearPanelBotones(0, PhysicalBody);
        }

        private void btnFindText_Click(object sender, EventArgs e)
        {
            label2titulo = "Results View";
            if (PanelDatosFlowOriginal3.Visible) IntercambiarVistaInferior();
            string texto = "";
            using (var form = new Form2("Text to search in node tree"))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    texto = form.ReturnValue;
                }
            }


            var lista = ArbolDeNodos.FindText(texto);

            if (lista != null)
            {
                LimpiarPanelInferior();
                PanelDatosFlowOriginal2.SuspendLayout();

                MessageBox.Show(string.Format("Found:{0} nodes containing:{1}", lista.Count, texto));
                foreach (Coordenada _coordenada in lista)
                {
                    Boton _boton = CrearBoton(_coordenada.id);
                    PanelDatosFlowOriginal2.Controls.Add(_boton);
                }
                PanelDatosFlowOriginal2.ResumeLayout();
            }
            else MessageBox.Show("No nodes found");
        }

        private void btnClearPanel_Click(object sender, EventArgs e)
        {
            LimpiarPanelInferior();
        }

        #endregion

        private void btnExtraerArbol_Click(object sender, EventArgs e)
        {
            Arbol temporal=ArbolDeNodos.ExtraerArbolDeNodo(15);
            temporal.ReEnumerar(temporal.comienzo, 100);
            temporal.comienzo = 100;
            ArbolDeNodos = temporal;



        }

        private void btnLoadFile2_Click(object sender, EventArgs e)
        {

            string text = "";
            int posicion = 0;

            DialogResult pregunta = openFileDialog1.ShowDialog();
            if (pregunta == DialogResult.OK)
            {
                nombrefichero2  = openFileDialog1.FileName;
                //Leer imagen png

                byte[] ficherobinario = File.ReadAllBytes(openFileDialog1.FileName);
                byte[] DataStartSequence = utf8.GetBytes("TValle.Character.Data");

                int start = BuscarEnBinario(ficherobinario, DataStartSequence);
                if (start == -1)
                {
                    MessageBox.Show("Cannot find TValle header");
                    return;
                }
                Arbol2DeNodos= null;
                GC.Collect();
                Arbol2DeNodos =  new Arbol();

                //Leer parte de texto

                StreamReader fichero = new StreamReader(openFileDialog1.FileName);

                text = fichero.ReadToEnd();
                posicion = text.IndexOf("TValle.Character.Data");
                text = text.Substring(posicion);
                fichero.Close();




                //parte de carga de imagen desactivada temporalmente
                byte[] imagenpng1 = new byte[start];
                //imagenpngfichero2.Position = 0;
                Array.Copy(ficherobinario, 0, imagenpng1, 0, start);
                imagenpngfichero2.Write(imagenpng1, 0, imagenpng1.Length);
                Image imagen2 = Image.FromStream(imagenpngfichero2);
                pictureBox2.Image = imagen2;
                this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                //pictureBox1.Width = imagen2.Width;
                //pictureBox1.Height = imagen2.Height;



                AnalizarDatosArbol2DeNodos(text);

                if (PanelDatosFlowOriginal2.Visible) IntercambiarVistaInferior();
                //Coordenada raiz = Arbol2DeNodos.Find(0);
                //int id = CrearPanelBotones(0, 0);
                btnLoadFile2.Enabled = false;
            }
        }

        private void AnalizarDatosArbol2DeNodos(string text)
        {
            Arbol nodoslista = new Arbol();
            Arbol nodosarray = new Arbol();

            //Crea la lista de elementos de {}            
            List<int> _aperturas = new List<int>();
            List<int> _cierres = new List<int>();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '{')
                {
                    _aperturas.Add(i);
                }
                else if (text[i] == '}')
                {
                    _cierres.Add(i);
                }
            }
            if (_aperturas.Count != _cierres.Count) throw new Exception("Error en el fichero de origen");
            nodoslista = CrearListaDeCoordenadas(_aperturas, _cierres);

            //Crea la lista de elementos de []
            List<int> aperturas = new List<int>();
            List<int> cierres = new List<int>();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '[')
                {
                    aperturas.Add(i);
                }
                else if (text[i] == ']')
                {
                    cierres.Add(i);
                }
            }
            if (aperturas.Count != cierres.Count) throw new Exception("Error en el fichero de origen");
            nodosarray = CrearListaDeCoordenadas(aperturas, cierres);
            foreach (Coordenada _coordenada in nodosarray) { _coordenada.IsArray = true; }

            Arbol2DeNodos = nodoslista.Sumar(nodosarray);

            //Vacia la memoria
            _aperturas = null;
            aperturas = null;
            _cierres = null;
            cierres = null;
            nodoslista = null;
            nodosarray = null;
            GC.Collect();

            Arbol2DeNodos.Arbolar();
            int vacias = Arbol2DeNodos.EncontrarMarcarVacias();

            Arbol2DeNodos.ExtraerDatos(0, text);//Empezar desde el nodo raiz=0;
            Coordenada raiz = Arbol2DeNodos.Find(0);
            foreach (Coordenada nodo in Arbol2DeNodos)
                nodo.desplegado = true;

        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            LimpiarPanelSuperior();
        }

        private void PanelDatosFlowOriginal3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnChangeView_Click(object sender, EventArgs e)
        {
            IntercambiarVistaInferior();
        }

        private void IntercambiarVistaInferior()
        {
            btnClearPanel.Enabled = !btnClearPanel.Enabled;
            PanelDatosFlowOriginal2.Visible = !PanelDatosFlowOriginal2.Visible;
            PanelDatosFlowOriginal3.Visible = !PanelDatosFlowOriginal3.Visible;

            if (PanelDatosFlowOriginal2.Visible)
            {
                lblNombreFichero2.Text = label2titulo;
            }
            else lblNombreFichero2.Text = nombrefichero2;
        }

        private void PanelDatosFlowOriginal2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTestCompare_Click(object sender, EventArgs e)
        {
            if (ArbolDeNodos.Find(0).Compare(Arbol2DeNodos.Find(0)))
            {
                MessageBox.Show("FuncionaIguales");
            }else MessageBox.Show("NoIguales"); ;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            List<Coordenada> coordenadas1 = new List<Coordenada>();
            List<Coordenada> coordenadas2 = new List<Coordenada>();

            for (int i = 0; i < 10; i++)
            {
                Coordenada coordenada = ArbolDeNodos.Find(i); coordenadas1.Add(coordenada);
                 Coordenada coordenada2 = Arbol2DeNodos.Find(i); coordenadas2.Add(coordenada2);
            }
        //}catch (Exception ex) { }
        
            using (var form = new Editar(coordenadas1, coordenadas2))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    List<string> ListaDatos = form.ListaDatos;
                    //coordenada.ListaDatos = ListaDatos;
                    //MessageBox.Show("String saved:" + coordenada.ToString());
                }
                else MessageBox.Show("No salvado:");
            }
        }

        private void btnEditBlocks_Click(object sender, EventArgs e)
        {
            using (var form = new Edit_Tree_Blocks(this))
            {
                this.Hide();
                var result = form.ShowDialog();

                this.Show();
                if (result == DialogResult.OK)
                {
                    
                    //coordenada.ListaDatos = ListaDatos;
                    MessageBox.Show("SAVED");
                }
                else MessageBox.Show("NOT SAVED");

            }

        }

        private void btnCompare_Click_1(object sender, EventArgs e)
        {
            bool prueba=ArbolDeNodos.Compare(ArbolDeNodos.comienzo,Arbol2DeNodos);
            if (prueba) MessageBox.Show("Son iguales");
        }

        private void btnInformation_Click(object sender, EventArgs e)
        {
            var form = new BasicInformation();
            form.Show();
        }

        private void btnLoadNode_Click(object sender, EventArgs e)
        {
            int nodoid=0; string cadena = "";
            using (var form = new Form2("Please introduce a node reference "))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    cadena = form.ReturnValue;
                    
                }
            }
            try
            {
                nodoid = int.Parse(cadena);
                ArbolDeNodos.Find(nodoid);
                CrearPanelBotones(0, nodoid);
            }
            catch (Exception ex) { MessageBox.Show("Wrong node reference"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new VisualTree(ArbolDeNodos))
            {
                var result = form.ShowDialog();

            }

        }
    }
}
