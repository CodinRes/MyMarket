namespace MyMarket.Formularios.Autenticacion;

partial class FrmLogin
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
            txtCuil = new TextBox();
            txtPassword = new TextBox();
            lblTitulo = new Label();
            btnIniciarSesion = new Button();
            btnCancelar = new Button();
            lblCuil = new Label();
            lblPassword = new Label();
            SuspendLayout();
            // 
            // txtCuil
            // 
            txtCuil.Location = new Point(88, 92);
            txtCuil.MaxLength = 13;
            txtCuil.Name = "txtCuil";
            txtCuil.PlaceholderText = "Ingrese el CUIL sin guiones";
            txtCuil.Size = new Size(304, 23);
            txtCuil.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(88, 148);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Ingrese la contraseña";
            txtPassword.Size = new Size(304, 23);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(88, 24);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(280, 45);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "INICIO DE SESIÓN";
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.Location = new Point(88, 208);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(140, 38);
            btnIniciarSesion.TabIndex = 3;
            btnIniciarSesion.Text = "Iniciar sesión";
            btnIniciarSesion.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(252, 208);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(140, 38);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // lblCuil
            // 
            lblCuil.AutoSize = true;
            lblCuil.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCuil.Location = new Point(210, 68);
            lblCuil.Name = "lblCuil";
            lblCuil.Size = new Size(45, 20);
            lblCuil.TabIndex = 5;
            lblCuil.Text = "CUIL";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPassword.Location = new Point(189, 124);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(88, 20);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Contraseña";
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(484, 286);
            Controls.Add(lblPassword);
            Controls.Add(lblCuil);
            Controls.Add(btnCancelar);
            Controls.Add(btnIniciarSesion);
            Controls.Add(lblTitulo);
            Controls.Add(txtPassword);
            Controls.Add(txtCuil);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Inicio de sesión";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCuil;
        private TextBox txtPassword;
        private Label lblTitulo;
        private Button btnIniciarSesion;
        private Button btnCancelar;
        private Label lblCuil;
        private Label lblPassword;
    }