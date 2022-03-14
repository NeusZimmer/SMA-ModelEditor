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
    public partial class VisualTree : Form
    {
        int contador = 0;
        BotonVisualizadorArbol boton = new BotonVisualizadorArbol();
        public VisualTree(Arbol arbolDeNodos)
        {
            InitializeComponent();
            panelVisualizadorArbol1.ArbolDeNodos = arbolDeNodos;
        }

        private void btnLoadArbol_Click(object sender, EventArgs e)
        {
            panelVisualizadorArbol1.ArbolDeNodos.MarcarSubArbolesVacios(panelVisualizadorArbol1.ArbolDeNodos.comienzo,true);
            panelVisualizadorArbol1.ArbolDeNodos.MarcarTiposDeCoordenadas();
            panelVisualizadorArbol1.ProcesarArbol();
        }
    }
}
