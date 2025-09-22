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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";

            // ===== Panel Lateral =====
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Width = 220;
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(35, 40, 45);
            this.Controls.Add(this.panelMenu);

            // Botones del menú (Dock = Top)
            this.btnEmitirRecibo = CreateMenuButton("Venta");
            this.btnRecibosEmitidos = CreateMenuButton("Recibos Emitidos");
            this.btnClientesSuscriptos = CreateMenuButton("Clientes suscriptos");
            this.btnControlStock = CreateMenuButton("Control de Stock");
            this.btnAnalisisDatos = CreateMenuButton("Análisis de Datos");
            this.btnGestionUsuarios = CreateMenuButton("Gestión de Usuarios");
            this.btnSalir = CreateMenuButton("Salir");

            this.panelMenu.Controls.Add(this.btnSalir);
            this.panelMenu.Controls.Add(this.btnGestionUsuarios);
            this.panelMenu.Controls.Add(this.btnAnalisisDatos);
            this.panelMenu.Controls.Add(this.btnControlStock);
            this.panelMenu.Controls.Add(this.btnClientesSuscriptos);
            this.panelMenu.Controls.Add(this.btnRecibosEmitidos);
            this.panelMenu.Controls.Add(this.btnEmitirRecibo);

            // Encabezado pequeño del menú
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblTitulo.Text = "MyMarket";
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitulo.Height = 70;
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.panelMenu.Controls.Add(this.lblTitulo);
            this.panelMenu.Controls.SetChildIndex(this.lblTitulo, this.panelMenu.Controls.Count - 1);

            // ===== Panel Superior =====
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 48;
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            this.Controls.Add(this.panelTop);

            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblUsuario.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUsuario.Width = 450;
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.lblUsuario.Padding = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.lblUsuario.Text = "Iniciar sesión";
            this.panelTop.Controls.Add(this.lblUsuario);

            // ===== Panel Contenedor =====
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.panelContenedor);
            this.panelContenedor.BringToFront();

            // Config general del form
            this.Text = "Sistema de Gestión - MyMarket";
            this.Width = 1100;
            this.Height = 700;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
        }

        #endregion
    }