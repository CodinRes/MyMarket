namespace MyMarket.Formularios.Principal;

partial class FrmPrincipal
{
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelMenu = new System.Windows.Forms.Panel();
            lblTitulo = new System.Windows.Forms.Label();
            btnSalir = new System.Windows.Forms.Button();
            btnConfiguracion = new System.Windows.Forms.Button();
            btnGestionUsuarios = new System.Windows.Forms.Button();
            btnAnalisisDatos = new System.Windows.Forms.Button();
            btnControlStock = new System.Windows.Forms.Button();
            btnClientesSuscriptos = new System.Windows.Forms.Button();
            btnRecibosEmitidos = new System.Windows.Forms.Button();
            btnEmitirRecibo = new System.Windows.Forms.Button();
            panelTop = new System.Windows.Forms.Panel();
            lblUsuario = new System.Windows.Forms.Label();
            panelContenedor = new System.Windows.Forms.Panel();
            panelMenu.SuspendLayout();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = System.Drawing.Color.FromArgb(35, 40, 45);
            panelMenu.Controls.Add(btnSalir);
            panelMenu.Controls.Add(btnConfiguracion);
            panelMenu.Controls.Add(btnGestionUsuarios);
            panelMenu.Controls.Add(btnAnalisisDatos);
            panelMenu.Controls.Add(btnControlStock);
            panelMenu.Controls.Add(btnClientesSuscriptos);
            panelMenu.Controls.Add(btnRecibosEmitidos);
            panelMenu.Controls.Add(btnEmitirRecibo);
            panelMenu.Controls.Add(lblTitulo);
            panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            panelMenu.Location = new System.Drawing.Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new System.Drawing.Size(220, 700);
            panelMenu.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTitulo.ForeColor = System.Drawing.Color.White;
            lblTitulo.Location = new System.Drawing.Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(220, 70);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "MyMarket";
            lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEmitirRecibo
            // 
            btnEmitirRecibo.BackColor = System.Drawing.Color.FromArgb(45, 50, 55);
            btnEmitirRecibo.Dock = System.Windows.Forms.DockStyle.Top;
            btnEmitirRecibo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnEmitirRecibo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            btnEmitirRecibo.ForeColor = System.Drawing.Color.White;
            btnEmitirRecibo.Location = new System.Drawing.Point(0, 70);
            btnEmitirRecibo.Name = "btnEmitirRecibo";
            btnEmitirRecibo.Size = new System.Drawing.Size(220, 48);
            btnEmitirRecibo.TabIndex = 1;
            btnEmitirRecibo.TabStop = false;
            btnEmitirRecibo.Text = "Venta";
            btnEmitirRecibo.UseVisualStyleBackColor = false;
            // 
            // btnRecibosEmitidos
            // 
            btnRecibosEmitidos.BackColor = System.Drawing.Color.FromArgb(45, 50, 55);
            btnRecibosEmitidos.Dock = System.Windows.Forms.DockStyle.Top;
            btnRecibosEmitidos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRecibosEmitidos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            btnRecibosEmitidos.ForeColor = System.Drawing.Color.White;
            btnRecibosEmitidos.Location = new System.Drawing.Point(0, 118);
            btnRecibosEmitidos.Name = "btnRecibosEmitidos";
            btnRecibosEmitidos.Size = new System.Drawing.Size(220, 48);
            btnRecibosEmitidos.TabIndex = 2;
            btnRecibosEmitidos.TabStop = false;
            btnRecibosEmitidos.Text = "Recibos Emitidos";
            btnRecibosEmitidos.UseVisualStyleBackColor = false;
            // 
            // btnClientesSuscriptos
            // 
            btnClientesSuscriptos.BackColor = System.Drawing.Color.FromArgb(45, 50, 55);
            btnClientesSuscriptos.Dock = System.Windows.Forms.DockStyle.Top;
            btnClientesSuscriptos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClientesSuscriptos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            btnClientesSuscriptos.ForeColor = System.Drawing.Color.White;
            btnClientesSuscriptos.Location = new System.Drawing.Point(0, 166);
            btnClientesSuscriptos.Name = "btnClientesSuscriptos";
            btnClientesSuscriptos.Size = new System.Drawing.Size(220, 48);
            btnClientesSuscriptos.TabIndex = 3;
            btnClientesSuscriptos.TabStop = false;
            btnClientesSuscriptos.Text = "Clientes suscriptos";
            btnClientesSuscriptos.UseVisualStyleBackColor = false;
            // 
            // btnControlStock
            // 
            btnControlStock.BackColor = System.Drawing.Color.FromArgb(45, 50, 55);
            btnControlStock.Dock = System.Windows.Forms.DockStyle.Top;
            btnControlStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnControlStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            btnControlStock.ForeColor = System.Drawing.Color.White;
            btnControlStock.Location = new System.Drawing.Point(0, 214);
            btnControlStock.Name = "btnControlStock";
            btnControlStock.Size = new System.Drawing.Size(220, 48);
            btnControlStock.TabIndex = 4;
            btnControlStock.TabStop = false;
            btnControlStock.Text = "Control de Stock";
            btnControlStock.UseVisualStyleBackColor = false;
            // 
            // btnAnalisisDatos
            // 
            btnAnalisisDatos.BackColor = System.Drawing.Color.FromArgb(45, 50, 55);
            btnAnalisisDatos.Dock = System.Windows.Forms.DockStyle.Top;
            btnAnalisisDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAnalisisDatos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            btnAnalisisDatos.ForeColor = System.Drawing.Color.White;
            btnAnalisisDatos.Location = new System.Drawing.Point(0, 262);
            btnAnalisisDatos.Name = "btnAnalisisDatos";
            btnAnalisisDatos.Size = new System.Drawing.Size(220, 48);
            btnAnalisisDatos.TabIndex = 5;
            btnAnalisisDatos.TabStop = false;
            btnAnalisisDatos.Text = "Análisis de Datos";
            btnAnalisisDatos.UseVisualStyleBackColor = false;
            // 
            // btnGestionUsuarios
            // 
            btnGestionUsuarios.BackColor = System.Drawing.Color.FromArgb(45, 50, 55);
            btnGestionUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            btnGestionUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGestionUsuarios.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            btnGestionUsuarios.ForeColor = System.Drawing.Color.White;
            btnGestionUsuarios.Location = new System.Drawing.Point(0, 310);
            btnGestionUsuarios.Name = "btnGestionUsuarios";
            btnGestionUsuarios.Size = new System.Drawing.Size(220, 48);
            btnGestionUsuarios.TabIndex = 6;
            btnGestionUsuarios.TabStop = false;
            btnGestionUsuarios.Text = "Gestión de Usuarios";
            btnGestionUsuarios.UseVisualStyleBackColor = false;
            // 
            // btnConfiguracion
            // 
            btnConfiguracion.BackColor = System.Drawing.Color.FromArgb(45, 50, 55);
            btnConfiguracion.Dock = System.Windows.Forms.DockStyle.Top;
            btnConfiguracion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnConfiguracion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            btnConfiguracion.ForeColor = System.Drawing.Color.White;
            btnConfiguracion.Location = new System.Drawing.Point(0, 358);
            btnConfiguracion.Name = "btnConfiguracion";
            btnConfiguracion.Size = new System.Drawing.Size(220, 48);
            btnConfiguracion.TabIndex = 7;
            btnConfiguracion.TabStop = false;
            btnConfiguracion.Text = "Configuración";
            btnConfiguracion.UseVisualStyleBackColor = false;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = System.Drawing.Color.FromArgb(45, 50, 55);
            btnSalir.Dock = System.Windows.Forms.DockStyle.Top;
            btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSalir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            btnSalir.ForeColor = System.Drawing.Color.White;
            btnSalir.Location = new System.Drawing.Point(0, 406);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new System.Drawing.Size(220, 48);
            btnSalir.TabIndex = 8;
            btnSalir.TabStop = false;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            // 
            // panelTop
            // 
            panelTop.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            panelTop.Controls.Add(lblUsuario);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Location = new System.Drawing.Point(220, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new System.Drawing.Size(880, 48);
            panelTop.TabIndex = 1;
            // 
            // lblUsuario
            // 
            lblUsuario.Dock = System.Windows.Forms.DockStyle.Right;
            lblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblUsuario.ForeColor = System.Drawing.Color.White;
            lblUsuario.Location = new System.Drawing.Point(430, 0);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Padding = new System.Windows.Forms.Padding(0, 0, 16, 0);
            lblUsuario.Size = new System.Drawing.Size(450, 48);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Iniciar sesión";
            lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelContenedor
            // 
            panelContenedor.BackColor = System.Drawing.Color.WhiteSmoke;
            panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContenedor.Location = new System.Drawing.Point(220, 48);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new System.Drawing.Size(880, 652);
            panelContenedor.TabIndex = 2;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1100, 700);
            Controls.Add(panelContenedor);
            Controls.Add(panelTop);
            Controls.Add(panelMenu);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = true;
            Name = "FrmPrincipal";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Sistema de Gestión - MyMarket";
            panelMenu.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Button btnEmitirRecibo;
        private System.Windows.Forms.Button btnRecibosEmitidos;
        private System.Windows.Forms.Button btnControlStock;
        private System.Windows.Forms.Button btnClientesSuscriptos;
        private System.Windows.Forms.Button btnAnalisisDatos;
        private System.Windows.Forms.Button btnGestionUsuarios;
        private System.Windows.Forms.Button btnConfiguracion;
        private System.Windows.Forms.Button btnSalir;
    }