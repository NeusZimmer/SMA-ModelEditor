
namespace SMA_ModelEditor
{
    partial class BotonArbol
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
            this.Contenedor = new System.Windows.Forms.TableLayoutPanel();
            this.Texto = new System.Windows.Forms.Label();
            this.BotonDesplegar = new System.Windows.Forms.Button();
            this.PanelMarcador = new System.Windows.Forms.Panel();
            this.Contenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // Contenedor
            // 
            this.Contenedor.AutoSize = true;
            this.Contenedor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Contenedor.ColumnCount = 3;
            this.Contenedor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.Contenedor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Contenedor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.Contenedor.Controls.Add(this.Texto, 1, 0);
            this.Contenedor.Controls.Add(this.BotonDesplegar, 0, 0);
            this.Contenedor.Controls.Add(this.PanelMarcador, 2, 0);
            this.Contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Contenedor.Location = new System.Drawing.Point(0, 0);
            this.Contenedor.MaximumSize = new System.Drawing.Size(0, 25);
            this.Contenedor.MinimumSize = new System.Drawing.Size(0, 25);
            this.Contenedor.Name = "Contenedor";
            this.Contenedor.RowCount = 1;
            this.Contenedor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Contenedor.Size = new System.Drawing.Size(198, 25);
            this.Contenedor.TabIndex = 0;
            // 
            // Texto
            // 
            this.Texto.AutoSize = true;
            this.Texto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Texto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Texto.Location = new System.Drawing.Point(25, 0);
            this.Texto.Margin = new System.Windows.Forms.Padding(0);
            this.Texto.MaximumSize = new System.Drawing.Size(0, 25);
            this.Texto.MinimumSize = new System.Drawing.Size(0, 25);
            this.Texto.Name = "Texto";
            this.Texto.Size = new System.Drawing.Size(113, 25);
            this.Texto.TabIndex = 0;
            this.Texto.Text = "Texto a mostrar";
            this.Texto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BotonDesplegar
            // 
            this.BotonDesplegar.AutoSize = true;
            this.BotonDesplegar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BotonDesplegar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BotonDesplegar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BotonDesplegar.FlatAppearance.BorderSize = 0;
            this.BotonDesplegar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BotonDesplegar.Location = new System.Drawing.Point(0, 0);
            this.BotonDesplegar.Margin = new System.Windows.Forms.Padding(0);
            this.BotonDesplegar.Name = "BotonDesplegar";
            this.BotonDesplegar.Size = new System.Drawing.Size(25, 25);
            this.BotonDesplegar.TabIndex = 1;
            this.BotonDesplegar.Text = "+";
            this.BotonDesplegar.UseVisualStyleBackColor = false;
            this.BotonDesplegar.Click += new System.EventHandler(this.CambiarEstadoClick);
            // 
            // PanelMarcador
            // 
            this.PanelMarcador.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanelMarcador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMarcador.Location = new System.Drawing.Point(138, 0);
            this.PanelMarcador.Margin = new System.Windows.Forms.Padding(0);
            this.PanelMarcador.Name = "PanelMarcador";
            this.PanelMarcador.Size = new System.Drawing.Size(60, 25);
            this.PanelMarcador.TabIndex = 2;
            // 
            // VisualizadorArbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.Contenedor);
            this.MaximumSize = new System.Drawing.Size(0, 25);
            this.MinimumSize = new System.Drawing.Size(0, 25);
            this.Name = "VisualizadorArbol";
            this.Size = new System.Drawing.Size(198, 25);
            this.Contenedor.ResumeLayout(false);
            this.Contenedor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel Contenedor;
        private System.Windows.Forms.Label Texto;
        private System.Windows.Forms.Button BotonDesplegar;
        private System.Windows.Forms.Panel PanelMarcador;

    }
}
