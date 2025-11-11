namespace MyMarket.Formularios.Principal;

partial class FrmBienvenida
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
            lblBienvenida = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // lblBienvenida
            // 
            lblBienvenida.Dock = System.Windows.Forms.DockStyle.Fill;
            lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblBienvenida.Location = new System.Drawing.Point(0, 0);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new System.Drawing.Size(800, 450);
            lblBienvenida.TabIndex = 0;
            lblBienvenida.Text = "Bienvenido al Sistema de Gestión de MyMarket";
            lblBienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmBienvenida
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(lblBienvenida);
            Name = "FrmBienvenida";
            Text = "FrmBienvenida";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblBienvenida;
    }