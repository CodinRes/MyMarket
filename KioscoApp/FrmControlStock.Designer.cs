namespace KioscoApp
{
    partial class FrmControlStock
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Control de Stock";
            this.Padding = new System.Windows.Forms.Padding(16);

            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnReponer = new System.Windows.Forms.Button();
            this.btnDescontar = new System.Windows.Forms.Button();
            this.panelBottom = new System.Windows.Forms.FlowLayoutPanel();

            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.Columns.Add("Codigo", "Código");
            this.dgv.Columns.Add("Producto", "Producto");
            this.dgv.Columns.Add("Stock", "Stock");
            this.Controls.Add(this.dgv);

            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Height = 48;
            this.panelBottom.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.btnReponer.Text = "Reponer +1";
            this.btnReponer.Width = 110;
            this.btnReponer.Height = 28;
            this.btnDescontar.Text = "Descontar -1";
            this.btnDescontar.Width = 110;
            this.btnDescontar.Height = 28;
            this.panelBottom.Controls.Add(this.btnReponer);
            this.panelBottom.Controls.Add(this.btnDescontar);
            this.Controls.Add(this.panelBottom);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnReponer, btnDescontar;
        private System.Windows.Forms.FlowLayoutPanel panelBottom;
    }
}