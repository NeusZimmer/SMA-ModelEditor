using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMA_ModelEditor
{
    //class PanelBotones : FlowLayoutPanel
        class PanelBotones : TableLayoutPanel
        {
        public delegate void BotonEditarDelegate(int nodoid);
        public delegate void BotonMarcarDelegate(int nodoid);
        public BotonEditarDelegate BotonEditarDelegateCallback;
        public BotonMarcarDelegate BotonMarcarDelegateCallback;

        public int nodoid { get; private set; }
        public int parent { get; private set; }
        public int id;
        public int children;

        public ControlCollection Controles;
        private Button botoncerrar=new Button(), botonmarcar = new Button(), botoneditar = new Button();
        //public PanelBotones(int _nodoid, int parentid)
        private FlowLayoutPanel CrearPanel()
        {
            FlowLayoutPanel PanelLayoutBotones = new FlowLayoutPanel();
            Controles = PanelLayoutBotones.Controls;

            //inicializar_caracteristicas();
            PanelLayoutBotones.AutoSize = true;
            PanelLayoutBotones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            //this.Dock = System.Windows.Forms.DockStyle.Top;
            PanelLayoutBotones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            PanelLayoutBotones.Margin = new System.Windows.Forms.Padding(0);
            PanelLayoutBotones.BackColor = System.Drawing.Color.AliceBlue;
            PanelLayoutBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.Name = "PanelNodos" + nodoid;
            return PanelLayoutBotones;
        }

        public PanelBotones(int _nodoid, int parentid)
        {
            nodoid = _nodoid;
            parent = parentid;
            id=parent+1;
            this.Name = "PanelNodos" + id;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ColumnCount = 4;
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.RowCount = 1;
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            FlowLayoutPanel PanelLayoutBotones = CrearPanel();
            this.Controls.Add(PanelLayoutBotones);
            ConfigurarBotonCerrar(botoncerrar);
            ConfigurarBotonEditar(botoneditar);
            ConfigurarBotonMarcar(botonmarcar);
            this.Controls.Add(botonmarcar);
            this.Controls.Add(botoneditar);
            this.Controls.Add(botoncerrar);
        }
        //public delegate void BotonEditarDelegate(int nodoid);
        private void ConfigurarBotonEditar(Button botoneditar) 
        {
            botoneditar.AutoSize = true;
            botoneditar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            botoneditar.BackColor = System.Drawing.Color.Yellow;
            botoneditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            botoneditar.FlatAppearance.BorderSize = 0;
            botoneditar.Margin = new System.Windows.Forms.Padding(0);
            botoneditar.Text = "E";
            botoneditar.UseVisualStyleBackColor = false;
            botoneditar.Dock = System.Windows.Forms.DockStyle.Fill;
            botoneditar.Click += new System.EventHandler(this.Editar);
        }

        private void Editar(object sender, EventArgs e)
        {
            BotonEditarDelegateCallback(this.nodoid);
        }
       
        private void ConfigurarBotonMarcar(Button botonmarcar) 
        {
            botonmarcar.AutoSize = true;
            botonmarcar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            botonmarcar.BackColor = System.Drawing.Color.Green;
            botonmarcar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            botonmarcar.FlatAppearance.BorderSize = 0;
            botonmarcar.Margin = new System.Windows.Forms.Padding(0);
            botonmarcar.Text = "M";
            botonmarcar.UseVisualStyleBackColor = false;
            botonmarcar.Dock = System.Windows.Forms.DockStyle.Fill;
            //botoncerrar.Click += new System.EventHandler(this.Borrar);
            botonmarcar.Click += new System.EventHandler(this.Marcar);
        }


        private void Marcar(object sender, EventArgs e)
        {
            BotonMarcarDelegateCallback(this.nodoid);
        }

        private void ConfigurarBotonCerrar(Button botoncerrar)
        {
            botoncerrar.AutoSize = true;
            botoncerrar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            botoncerrar.BackColor = System.Drawing.Color.Red;
            botoncerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            botoncerrar.FlatAppearance.BorderSize = 0;
            //botoncerrar.Location = new System.Drawing.Point(0, 0);
            botoncerrar.Margin = new System.Windows.Forms.Padding(0);
            botoncerrar.Text = "X";
            botoncerrar.UseVisualStyleBackColor = false;
            botoncerrar.Dock = System.Windows.Forms.DockStyle.Fill;
            botoncerrar.Click += new System.EventHandler(this.Borrar);
        }

        public void Borrar(object sender, EventArgs e)
        {
            this.SuspendLayout();
            Button button = (Button)sender;

            if(children>0)
            { 
            var colleccion=this.Parent.Controls.Find("PanelNodos" + children,true);
                foreach (PanelBotones control in colleccion)
                {
                    control.Borrar(sender, e);
                }
            }
            foreach (Control control in this.Controles)
                control.Dispose();
            foreach (Control control in this.Controls)
                control.Dispose();
            this.Dispose();
        }
        }

    class Boton : Button
    {
        public int nodoid = 0;
        public int parentid = 0;
        public bool IsInfo
        {
            get
            { return !this.Enabled; }
            set
            {
                this.Enabled = !value;
                if (value) AsignarParametrosTextoPlano();//Si es Informacion 
                else AsignarParametrosNodo();
            }
        }
        public Boton(int id = -1, int parent = -1)
        {
            nodoid = id;
            parentid = parent;
            AsignarParametrosStandar();
        }
        public Boton(int id = 0)
        {
            nodoid = id;
            AsignarParametrosStandar();
        }
        private void AsignarParametrosTextoPlano()
        {
            //this.BackColor = System.Drawing.Color.White;
            this.BackColor = System.Drawing.Color.GhostWhite;
            //this.FlatAppearance.BorderSize = 1;
        }
        private void AsignarParametrosStandar()
        {
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Name = "btn" + nodoid;
            this.Size = new System.Drawing.Size(56, 25);
            this.Text = "btn" + nodoid;
            this.UseVisualStyleBackColor = false;
            this.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
        }
        private void AsignarParametrosNodo()
        {
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            //this.FlatAppearance.BorderSize = 1;
        }

    }

}
