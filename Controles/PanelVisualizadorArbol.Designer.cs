
namespace SMA_ModelEditor
{
    partial class PanelVisualizadorArbol
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
            this.button1 = new System.Windows.Forms.Button();
            this.panelKeyValue = new System.Windows.Forms.Panel();
            this.keyValuecontrol = new SMA_ModelEditor.KeyValue();
            this.panelKeyValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(546, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // panelKeyValue
            // 
            this.panelKeyValue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelKeyValue.Controls.Add(this.keyValuecontrol);
            this.panelKeyValue.Controls.Add(this.button1);
            this.panelKeyValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelKeyValue.Location = new System.Drawing.Point(467, 0);
            this.panelKeyValue.Margin = new System.Windows.Forms.Padding(0);
            this.panelKeyValue.Name = "panelKeyValue";
            this.panelKeyValue.Size = new System.Drawing.Size(546, 478);
            this.panelKeyValue.TabIndex = 6;
            // 
            // keyValuecontrol
            // 
            this.keyValuecontrol.AutoSize = true;
            this.keyValuecontrol.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.keyValuecontrol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keyValuecontrol.Location = new System.Drawing.Point(0, 23);
            this.keyValuecontrol.Margin = new System.Windows.Forms.Padding(0);
            this.keyValuecontrol.MinimumSize = new System.Drawing.Size(100, 200);
            this.keyValuecontrol.Name = "keyValuecontrol";
            this.keyValuecontrol.Size = new System.Drawing.Size(546, 455);
            this.keyValuecontrol.TabIndex = 1;
            // 
            // PanelVisualizadorArbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panelKeyValue);
            this.Name = "PanelVisualizadorArbol";
            this.Size = new System.Drawing.Size(1013, 478);
            this.panelKeyValue.ResumeLayout(false);
            this.panelKeyValue.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private KeyValue keyValuecontrol;
        private System.Windows.Forms.Panel panelKeyValue;
    }
}
