using SMA_ModelEditor.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMA_ModelEditor
{
    public partial class KeyValue : UserControl
    {
        Coordenada key_coordenada, value_coordenada;

        public KeyValue()
        {
            InitializeComponent();
        }
        public void AddKeys(Coordenada coordenada)
        {
            key_coordenada = coordenada;
            int contador = 0;
            foreach (string cadena in coordenada.ListaDatos)
            {
                Deslizador datos = new Deslizador(cadena, contador);
                datos.DelegateCambioDeValorCallback += new Deslizador.DelegateCambioDeValor(FuncionNoHacerNada);
                flowKeys.Controls.Add(datos);
                datos.DisallowModifications();
                datos.Visible = true;
                contador++;
            }
        
        }

        private void FuncionNoHacerNada(string valor)
        {
            //para evitar excepcion al no estar asignado
        }

        private void FuncionActualizarCoordenada(string valor)
        {

            List<Deslizador> listavalores = flowValues.Controls.OfType<Deslizador>().ToList();

            foreach (Deslizador deslizador in listavalores)
            {
                value_coordenada.ListaDatos[deslizador.id]=deslizador.Value;
            }
        
        }

        public void AddValues(Coordenada coordenada) 
        {
            value_coordenada = coordenada;
            int contador = 0;
            foreach (string cadena in coordenada.ListaDatos)
            {
                string cadena2 = cadena;
                Deslizador datos = new Deslizador(cadena2, contador, true);
                datos.DelegateCambioDeValorCallback += new Deslizador.DelegateCambioDeValor(FuncionActualizarCoordenada);
                flowValues.Controls.Add(datos);
                datos.Visible = true;
                contador++;
            }
        }

        private Label CreateLabel(string cadena) 
        {
            Label label = new Label();
            //label.Dock = System.Windows.Forms.DockStyle.Top;
            label.Location = new System.Drawing.Point(0, 0);
            label.Text = cadena;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            return label;
        }
        public void Clear()
        {
            flowKeys.Controls.Clear();
            flowValues.Controls.Clear();
        }

        internal void Check()
        {
            if (this.flowKeys.Controls.Count != this.flowValues.Controls.Count)
            { 
            flowKeys.Controls.Clear();
            for (int i = 0; i < flowValues.Controls.Count; i++)
                    {
                    Deslizador label = new Deslizador("Value " + (i + 1) + ":",i);
                        //Label label = CreateLabel("Value "+ (i+1) +":");
                        label.Visible = true;
                    label.DisallowModifications();
                        flowKeys.Controls.Add(label);
                    }
            }

        }

        private TrackBar CrearDesplazador(int orden)
        {
            TrackBar desplazador = new TrackBar();
            desplazador.LargeChange = 20;
            desplazador.Location = new System.Drawing.Point(0, 0);
            desplazador.Margin = new System.Windows.Forms.Padding(0);
            desplazador.Maximum = 1000;
            desplazador.Name = "TB"+orden;
            desplazador.SmallChange = 10;
            desplazador.TabIndex = 0;
            desplazador.TickFrequency = 25;
            desplazador.Value = 0;

            return desplazador;
        }
    }
}
