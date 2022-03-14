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
    public partial class Form2 : Form
    {
        string pregunta;
        int informacion = 0;
        public string ReturnValue { get; set; }
        public Form2(string cadena="No questiions????",int _informacion=0)
        {
            
            InitializeComponent();
            lblPregunta.Text = cadena;
            informacion = _informacion;
            switch (informacion)
            {
                case 0:
                    txtBoxInfo.Visible = false;

                    break;
                case 1:
                    txtBoxInfo.Visible = true;
                    txtBoxInfo.Text = "Parameters are defined in spanish language,use it for your search, below a short list of possible search examples(EN:ES):"+
                        "\n\n-Tits: Seno" +
                        "\n-Vagina: Vag"+
                        "\n-Character evolution: Consent & Concent"+
                        "\nLegs:Piernas"+
                        "\nAss:Culo"+
                        "\nSentibility: Sensibilidad"+
                        "\n\nYou could also search for:" +
                        "\n-Anger and Rabia" +
                        "\n-Erogenidad";
                    break;
            
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.ReturnValue = txtRespuesta.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
