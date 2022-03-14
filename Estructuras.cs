using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMA_ModelEditor
{
    [Serializable]
    public class Arbol : List<Coordenada>
    {

        public int comienzo = 0;
        internal void Arbolar()
        {
            foreach (Coordenada dato in this)
            {
                var listaprueba = from _Coordenada in this where ((dato.comienzo < _Coordenada.comienzo) && (dato.final > _Coordenada.final) && _Coordenada.parent == -1) select _Coordenada;
                List<Coordenada> lista2 = listaprueba.ToList();
                foreach (Coordenada _Coordenada in lista2)
                {
                    _Coordenada.parent = dato.id;
                }
                if (lista2.Count > 0)
                {
                    dato.haschildren = true;
                    dato.children = (from Data in lista2 select Data.id).ToList();
                }

            }
        }
        internal int EncontrarMarcarVacias()
        {
            var listaprueba2 = from _Coordenada in this where (_Coordenada.comienzo == (_Coordenada.final - 1)) select _Coordenada;
            int contador = listaprueba2.Count();
            foreach (Coordenada coordenada in listaprueba2)
            {
                coordenada.vacia = true;
            }
            return contador;
        }
        public Coordenada Find(int _id)
        {
            var listaprueba2 = from _Coordenada in this where _Coordenada.id == _id select _Coordenada;
            if (listaprueba2.Count() == 0) throw new Exception("ID not found");
            return listaprueba2.First();
        }
        //public object Clone()
        //{
        //    return this.MemberwiseClone();
        //}
        public Arbol Clone()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (Arbol)formatter.Deserialize(stream);
            }
        }


        public List<Coordenada> FindText(string cadena)
        {
            StringComparison comp = StringComparison.OrdinalIgnoreCase;
            ;
            var listaprueba2 = from _Coordenada in this where (_Coordenada.ToString()).ToLower().Contains(cadena.ToLower()) select _Coordenada;
            if (listaprueba2.Count() == 0) return null;
            return listaprueba2.ToList();
        }

        internal Arbol Sumar(Arbol nodosarray)
        {
            Arbol resultado = new Arbol();
            foreach (Coordenada nodo in nodosarray) this.Add(nodo);


            var test = from nodo in this orderby nodo.comienzo ascending select nodo;
            int contador = 0;
            foreach (Coordenada nodo in test)
            {
                nodo.id = contador;
                //contador--;
                contador++;
                resultado.Add(nodo);
            }
            resultado.Reverse();
            return resultado;
        }

        internal void ExtraerDatos(int id, string text)
        {
            //List<string> tempdata = new List<string>();
            string tempdata = "";
            //ir recorriendo arbol desde id=0
            Coordenada CoordenadaInicial = this.Find(id);
            if (!CoordenadaInicial.vacia)
            {
                List<Coordenada> listahijos = new List<Coordenada>();
                if (CoordenadaInicial.haschildren)
                {
                    foreach (int idnodo in CoordenadaInicial.children)
                    {
                        listahijos.Add(this.Find(idnodo));
                    }
                    var listahijosordenada = from coordenada in listahijos orderby coordenada.comienzo descending select coordenada;



                    string cadena = text.Substring(CoordenadaInicial.comienzo + 1, CoordenadaInicial.final - CoordenadaInicial.comienzo - 1);

                    foreach (Coordenada hijo in listahijosordenada)
                    {
                        int comienzo = hijo.comienzo - CoordenadaInicial.comienzo - 1;
                        int final = comienzo + (hijo.final - hijo.comienzo) + 1;
                        string cadenainiciotemp = cadena.Substring(0, comienzo);
                        string cadenafinaltemp = cadena.Substring(final);
                        //string marcador=string.Format("[Nodo:{0}]",hijo.id);
                        //string marcador = "{"+ 0 + "}";
                        string marcador = "{" + hijo.id + "}";
                        //MessageBox.Show(string.Format(marcador, "fdsfdf"));
                        //cadena = cadena.Remove(hijo.comienzo - CoordenadaInicial.comienzo-1, (hijo.final - hijo.comienzo)+1);
                        cadena = cadenainiciotemp + marcador + cadenafinaltemp;
                        ExtraerDatos(hijo.id, text);
                    }
                    //tempdata.Add(cadena);
                    tempdata = cadena;
                }
                else  //Notener hijos en coordenada
                {
                    //tempdata.Add(text.Substring(CoordenadaInicial.comienzo+1, CoordenadaInicial.final - CoordenadaInicial.comienzo-1));
                    tempdata = (text.Substring(CoordenadaInicial.comienzo + 1, CoordenadaInicial.final - CoordenadaInicial.comienzo - 1));
                }
                //CoordenadaInicial.data = tempdata;
                CoordenadaInicial.ListaDatos = PasarStringAlista(tempdata);
            }
            //else CoordenadaInicial.data = null;//final de if de vacia
            else CoordenadaInicial.ListaDatos = null;//final de if de vacia
        }
        internal List<string> PasarStringAlista(string texto)
        {
            List<string> ListaDatos = new List<string>();
            if (!string.IsNullOrEmpty(texto))
            {
                var cadenas = texto.Split(',');
                foreach (var cadena in cadenas)
                    ListaDatos.Add(cadena);
            }
            return ListaDatos;
        }

        public override string ToString()
        {
            string retorno = "";
            Coordenada coordenada = this.Find(0);
            retorno = RenderText(coordenada);
            return retorno;
        }

        private string RenderText(Coordenada coordenada)
        {
            string cadena = "", retorno = coordenada.ToString();
            if (coordenada.haschildren)
            {
                foreach (int nodoid in coordenada.children)
                {
                    Coordenada nodohijo = this.Find(nodoid);
                    string marcador = "{" + nodoid + "}";
                    int index = retorno.IndexOf(marcador);
                    string cadenainicio = retorno.Substring(0, index);
                    string cadenafinal = retorno.Substring(index + marcador.Length);
                    retorno = cadenainicio + RenderText(nodohijo) + cadenafinal;
                }
            }
            else retorno = coordenada.ToString();


            return retorno;
        }

        public bool Compare(int indicenodo, Arbol arbol2)
        {
            bool retorno = true;
            //bool temp = true;
            Coordenada coordenada1 = this.Find(indicenodo);
            Coordenada coordenada2 = arbol2.Find(indicenodo);

            if (!coordenada1.Compare(coordenada2)) { return false; }

            if (coordenada1.haschildren)
                foreach (int children in coordenada1.children)
                {
                    retorno = this.Compare(children, arbol2);
                    if (!retorno) break;
                }
            return retorno;
        }

        public int Size(int nodoid)
        {
            int resultado = 1;
            Coordenada coordenada = this.Find(nodoid);
            if (coordenada.haschildren)
            {
                foreach (int hijo in coordenada.children)
                {
                    resultado += this.Size(hijo);
                }
            }
            return resultado;
        }
        public bool BorrarNodoConHijos(int nodoid)
        {
            bool resultadok;
            Coordenada coordenada = this.Find(nodoid);
            if (coordenada.haschildren)
            {
                foreach (int hijo in coordenada.children)
                {
                    BorrarNodoConHijos(hijo);
                }
            }

            resultadok = this.Remove(coordenada);

            return resultadok;
        }

        internal void Replace(int NodoId1, Arbol arbol2DeNodos)
        {
            //Para sustitucion si los arboles son iguales.
            Coordenada coordenadaarbol1 = this.Find(NodoId1);
            Coordenada coordenadaarbol2 = arbol2DeNodos.Find(NodoId1);
            if (coordenadaarbol2.haschildren)
                foreach (int children in coordenadaarbol2.children)
                {
                    this.Replace(children, arbol2DeNodos);
                }

            this.Remove(coordenadaarbol1);
            this.Add(coordenadaarbol2);
            //coordenadaarbol1 = coordenadaarbol2;

        }

        public Arbol ExtraerArbolDeNodo(int idnodo)
        {
            Arbol retorno = new Arbol();
            Coordenada coordenadaactual = this.Find(idnodo);
            retorno.Add(coordenadaactual);
            if (coordenadaactual.haschildren)
            {
                foreach (int childrenindex in coordenadaactual.children)
                {
                    Arbol temporal = ExtraerArbolDeNodo(childrenindex);
                    foreach (Coordenada coordenadatemp in temporal)
                        retorno.Add(coordenadatemp);
                    temporal = null;
                }

            }
            retorno.comienzo = idnodo;
            return retorno;
        }

        public int ReEnumerar(int nodoid, int nuevoid)
        {
            if (114 == nuevoid)
                MessageBox.Show("pausa");
            Coordenada coordenada;
            try
            {
                coordenada = this.Find(nodoid);
            }
            catch (Exception ex) { return nodoid; }
            int resultado = nuevoid + 1;
            if (coordenada.haschildren)
            {
                List<int> hijos = new List<int>();
                foreach (int hijo in coordenada.children)
                {
                    string cadenaantigua = "{" + hijo.ToString() + "}";
                    string cadenanueva = "{" + resultado.ToString() + "}";
                    for (int i = 0; i < coordenada.ListaDatos.Count; i++)
                        coordenada.ListaDatos[i] = coordenada.ListaDatos[i].Replace(cadenaantigua, cadenanueva);
                    hijos.Add(resultado);
                    resultado = this.ReEnumerar(hijo, resultado);
                }


                coordenada.children.Clear();
                coordenada.children = hijos;
            }
            coordenada.id = nuevoid;
            return resultado;
        }

        /// <summary>
        /// return true if the tree contains info, false if not, and save that info into the tree or not
        /// </summary>
        /// <param name="indice"></param>
        /// <returns></returns>
        public bool MarcarSubArbolesVacios(int indice, bool save = false)
        {
            //List<Coordenada> listavacias = (from Coordenada coordenada in this where coordenada.vacia select coordenada).ToList();
            Coordenada coordenada = this.Find(indice);

            bool ArbolTieneDatos = false; //asumo que esta vacio

            if (coordenada.haschildren)
            {
                foreach (int indice2 in coordenada.children)
                {
                    ArbolTieneDatos = ArbolTieneDatos | MarcarSubArbolesVacios(indice2, save);
                    //if (!ArbolVacio) break;
                }
 
            }
            else ArbolTieneDatos = !coordenada.vacia;

            if (save) coordenada.FinalizaCadenaVacia = !ArbolTieneDatos;

            return ArbolTieneDatos;
        }

        public void MarcarTiposDeCoordenadas()
        {
            foreach (Coordenada coordenada in this)
                coordenada.MarcarTipoDeCoordenada();
        }
    }

[Serializable]
public class Coordenada:IDisposable
    {
        public bool m_id = false;
        public bool keyvalue = false;
        public bool FinalizaCadenaVacia=false;
        public bool passtrough = false;
        public bool IsListOfNodes = false;
        public bool IsData = false;
        public int id;
        public int comienzo;
        public int final;
        public bool haschildren = false;
        public bool vacia = false;
        public bool desplegado = false;
        public int parent=-1;
        public List<int> children;
        public bool IsArray = false;
        public string data { get; private set; }
        public List<string> ListaDatos;
        private bool disposedValue;

        public bool IsList { get { return !IsArray; } }
        public Coordenada(int _comienzo,int _final,int _id)
        {
            comienzo = _comienzo;
            final = _final;
            id = _id;
        }

        public string ToString(bool extra)
        {
            string cadena = "";

            if (this.vacia) return this.ToString();


            if (this.m_id)
            {
                cadena = this.ListaDatos[0].Substring("m_id:".Length + 2);
                cadena = cadena.Replace('\"', ' ');
            }
            else if (this.keyvalue) cadena = "List of Key/Value Pairings";
            else if (this.IsListOfNodes) cadena = "List of " + children.Count + " nodes";
            else if (this.passtrough) cadena = "Node is pasthrough";
            else cadena = this.ToString();

            return cadena;
        }
        public override string ToString()
        {
            string cadena = "",cadena2="";
            if (!vacia) cadena2 = ".+.";

            if (desplegado)
            {
                int contador = 0;
                if (ListaDatos != null)
                    { 
                    cadena2 = ListaDatos[contador];
                    contador++;
                    while (contador != ListaDatos.Count)
                    {
                        cadena2 = cadena2 + ',' + ListaDatos[contador];
                        contador++;
                    }
                        //cadena2 = data;
                }

            }
            if (IsArray)
            {
                cadena = "[" + cadena2 + "]";
            }
            else cadena = "{" + cadena2 + "}";

            return cadena;
        }

        public bool Compare(Coordenada CoordenadaDos, bool checkid = true, bool checkchildrens=true, bool checkListaDatos = true)
        {
            if (this.vacia && CoordenadaDos.vacia)
                            { return true; }

            if (this.vacia != CoordenadaDos.vacia)
                            { return false; }//resultado = false

            if (this.haschildren != CoordenadaDos.haschildren) 
                            { return false; }//resultado = false

            if (this.haschildren && CoordenadaDos.haschildren)
            { 
                if (this.children.Count != CoordenadaDos.children.Count) 
                            { return false; }//resultado = false
                
                if (checkid)
                    if (this.id!= CoordenadaDos.id) 
                            { return false; }//resultado = false
                
                if (checkchildrens)
                    foreach (int hijo in this.children)
                        if (!CoordenadaDos.children.Contains(hijo)) 
                            { return false; }//resultado = false

                if (checkListaDatos)
                    if (this.ListaDatos.Count != CoordenadaDos.ListaDatos.Count) 
                            { return false; }//resultado = false
            }

            
            return true;//resultado = true
        }
        public Coordenada Clone()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (Coordenada)formatter.Deserialize(stream);
            }
        }

        public void MarcarTipoDeCoordenada()
        {
            //m_id= this.ToString().Contains("m_id")?true:false;
            if (this.ToString().Contains("m_id")){ m_id = true; return; }
            if (this.ToString().Contains("keys")) {keyvalue = true; return;}

            else if (this.haschildren)
            {
                if (this.children.Count == 1) {passtrough = true;return; }
                else { IsListOfNodes = true;return; }
            } IsData = true; 
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    children = null;
                    data = null;
                    ListaDatos = null;
                    disposedValue = true;
                }
            }

        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

}
