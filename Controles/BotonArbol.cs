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
    public partial class BotonArbol : UserControl
    {
        
        public int? parentid = null;
        public int nodoid;
        public bool desplegado = false;
        List<int> children = new List<int>();
        public int orden;
        public delegate void ClickBoton(object sender, EventArgs args);
        public ClickBoton clickBoton;


        public BotonArbol()
        {
            InitializeComponent();
            //this.BotonDesplegar.Name = nodoid.ToString();
            this.BotonDesplegar.Click += new System.EventHandler(this.CambiarEstadoClick);

        }
        public override string Text
        {
            get
            {
                return Texto.Text;
            }
            set { Texto.Text = value; }
        }


        private void CambiarEstadoClick(object sender, EventArgs e)
        {
            clickBoton(sender, e);

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
