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

namespace SMA_ModelEditor.Controles
{
    public partial class Deslizador : UserControl
    {
        public string Value { get { return this.ToString(); } }
        public int id { get; private set; }
     
        public bool Slider=false;
        public delegate void DelegateCambioDeValor(string valor);
        public DelegateCambioDeValor DelegateCambioDeValorCallback;
        private bool quitadascomillas = false;
        private static void lanzar(string valor)
        {
                MessageBox.Show("test");
        }

        public override string ToString()
        {
            if (quitadascomillas)
                return "\"" + txtValue.Text + "\"";
            else return txtValue.Text;
        }
        public Deslizador(string value,int _id, bool _slider = false)
        {
            InitializeComponent();
            if ((value.First() == '"') & (value.Last() == '"')) 
            { 
                value = value.Substring(1, value.Length - 2);
                quitadascomillas = true;
            }

            txtValue.Text = value;
            if (value.Length>4) lvlValor.Text = value.Substring(0,4);
            else lvlValor.Text = value;
            id = _id;
            if (ComprobarOpciones())
                Slider = _slider;
            else Slider = false;
            Ajustar();
            this.Name = "Deslizador" + id;
        }

        private void Ajustar() 
        {
            if (Slider)
            { this.trackBar.Visible = true; this.txtValue.Visible = false; }
            else { this.trackBar.Visible = false; this.txtValue.Visible = true; }
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            if (ComprobarOpciones())
            {
                Slider = !Slider;
            }
            else { MessageBox.Show("Is not a number or Values not within trackbar parameters, (force possible using textbox)");Slider = false; }
            Ajustar();
        }


        /// <summary>
        /// Returns false if data does not fit with trackbar values.
        /// </summary>
        /// <returns></returns>
        private bool ComprobarOpciones() 
        {
            NumberStyles style = NumberStyles.AllowDecimalPoint;
            CultureInfo provider = new CultureInfo("en-US");
            bool resultado = true;
            decimal valor=0;
            try
            {
                valor = decimal.Parse(txtValue.Text,style,provider);
            }
            catch { return false; }
            if (valor > 1) resultado = false;
            else trackBar.Value = (int)(valor * 1000);
            
            return resultado; 
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            lvlValor.Text = decimal.Divide(trackBar.Value, 1000).ToString("0.00", new CultureInfo("en-US"));
            txtValue.Text = lvlValor.Text;
            DelegateCambioDeValorCallback(lvlValor.Text);
        }

        private void LanzarFuera(object sender, EventArgs e)
        {
            
            DelegateCambioDeValorCallback(lvlValor.Text);
        }
        public void DisallowModifications()
        {
            this.lvlValor.Visible = false;
            this.txtValue.ReadOnly = true;
            this.trackBar.Visible = false;
            this.btnCambiar.Visible = false;
            //this.txtValue.BorderStyle = BorderStyle.None;
            this.txtValue.Dock = DockStyle.Fill;
            this.txtValue.TextAlign =HorizontalAlignment.Center; 
        }
    }
}
