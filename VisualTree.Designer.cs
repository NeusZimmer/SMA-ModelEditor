
namespace SMA_ModelEditor
{
    partial class VisualTree
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoadArbol = new System.Windows.Forms.Button();
            this.panelVisualizadorArbol1 = new SMA_ModelEditor.PanelVisualizadorArbol();
            this.SuspendLayout();
            // 
            // btnLoadArbol
            // 
            this.btnLoadArbol.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLoadArbol.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLoadArbol.Location = new System.Drawing.Point(0, 0);
            this.btnLoadArbol.Name = "btnLoadArbol";
            this.btnLoadArbol.Size = new System.Drawing.Size(1139, 23);
            this.btnLoadArbol.TabIndex = 3;
            this.btnLoadArbol.Text = "Load Root Node";
            this.btnLoadArbol.UseVisualStyleBackColor = true;
            this.btnLoadArbol.Click += new System.EventHandler(this.btnLoadArbol_Click);
            // 
            // panelVisualizadorArbol1
            // 
            this.panelVisualizadorArbol1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVisualizadorArbol1.Location = new System.Drawing.Point(0, 23);
            this.panelVisualizadorArbol1.Name = "panelVisualizadorArbol1";
            this.panelVisualizadorArbol1.Size = new System.Drawing.Size(1139, 519);
            this.panelVisualizadorArbol1.TabIndex = 4;
            // 
            // VisualTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 542);
            this.Controls.Add(this.panelVisualizadorArbol1);
            this.Controls.Add(this.btnLoadArbol);
            this.Name = "VisualTree";
            this.Text = "VisualTree";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadArbol;
        private PanelVisualizadorArbol panelVisualizadorArbol1;
    }
}