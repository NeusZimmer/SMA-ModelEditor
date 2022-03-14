
using System;

namespace SMA_ModelEditor.Controles
{
    partial class Deslizador
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvlValor = new System.Windows.Forms.Label();
            this.btnCambiar = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.trackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // lvlValor
            // 
            this.lvlValor.Dock = System.Windows.Forms.DockStyle.Left;
            this.lvlValor.Location = new System.Drawing.Point(0, 0);
            this.lvlValor.Margin = new System.Windows.Forms.Padding(0);
            this.lvlValor.MaximumSize = new System.Drawing.Size(60, 35);
            this.lvlValor.MinimumSize = new System.Drawing.Size(60, 35);
            this.lvlValor.Name = "lvlValor";
            this.lvlValor.Size = new System.Drawing.Size(60, 35);
            this.lvlValor.TabIndex = 0;
            this.lvlValor.Text = "Value";
            this.lvlValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCambiar
            // 
            this.btnCambiar.BackColor = System.Drawing.Color.Turquoise;
            this.btnCambiar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCambiar.FlatAppearance.BorderSize = 0;
            this.btnCambiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiar.Location = new System.Drawing.Point(230, 0);
            this.btnCambiar.Margin = new System.Windows.Forms.Padding(0);
            this.btnCambiar.MaximumSize = new System.Drawing.Size(20, 35);
            this.btnCambiar.MinimumSize = new System.Drawing.Size(20, 35);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(20, 35);
            this.btnCambiar.TabIndex = 3;
            this.btnCambiar.Text = "<>";
            this.btnCambiar.UseVisualStyleBackColor = false;
            this.btnCambiar.Click += new System.EventHandler(this.btnCambiar_Click);
            // 
            // txtValue
            // 
            this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtValue.Location = new System.Drawing.Point(60, 0);
            this.txtValue.Margin = new System.Windows.Forms.Padding(0);
            this.txtValue.MaximumSize = new System.Drawing.Size(150, 35);
            this.txtValue.MinimumSize = new System.Drawing.Size(150, 35);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(150, 35);
            this.txtValue.TabIndex = 7;
            this.txtValue.Leave += new System.EventHandler(this.LanzarFuera);
            // 
            // trackBar
            // 
            this.trackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar.LargeChange = 50;
            this.trackBar.Location = new System.Drawing.Point(60, 0);
            this.trackBar.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar.Maximum = 1000;
            this.trackBar.MaximumSize = new System.Drawing.Size(150, 35);
            this.trackBar.MinimumSize = new System.Drawing.Size(150, 35);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(150, 35);
            this.trackBar.SmallChange = 10;
            this.trackBar.TabIndex = 8;
            this.trackBar.TickFrequency = 50;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // Deslizador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.btnCambiar);
            this.Controls.Add(this.lvlValor);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(250, 35);
            this.Name = "Deslizador";
            this.Size = new System.Drawing.Size(250, 35);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.Label lvlValor;
        private System.Windows.Forms.Button btnCambiar;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TrackBar trackBar;
    }
}
