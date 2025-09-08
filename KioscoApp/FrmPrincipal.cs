using System;
using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class FrmPrincipal : Form
    {
        private Panel panelMenu;
        private Panel panelTop;
        private Panel panelContenedor;

        private Label lblTitulo;
        private Label lblUsuario;

        private Button btnEmitirRecibo;
        private Button btnRecibosEmitidos;
        private Button btnControlStock;
        private Button btnAnalisisDatos;
        private Button btnConfiguracion;
        private Button btnSalir;

        private readonly string _empleado;
        private readonly string _rol;

        public FrmPrincipal(string empleado, string rol)
        {
            _empleado = empleado;
            _rol = rol;

            // Config general del form
            Text = "Sistema de Gestión - Kiosco";
            Width = 1100;
            Height = 700;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            BuildUI();
            Load += FrmPrincipal_Load;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            AbrirEnPanel(new FrmBienvenida());
        }

        private void BuildUI()
        {
            // ===== Panel Lateral =====
            panelMenu = new Panel
            {
                Dock = DockStyle.Left,
                Width = 220,
                BackColor = Color.FromArgb(35, 40, 45)
            };
            Controls.Add(panelMenu);

            // Botones del menú (Dock = Top)
            btnEmitirRecibo = CreateMenuButton("Emitir Recibo");
            btnEmitirRecibo.Click += (s, e) => AbrirEnPanel(new FrmEmitirRecibo());

            btnRecibosEmitidos = CreateMenuButton("Recibos Emitidos");
            btnRecibosEmitidos.Click += (s, e) => AbrirEnPanel(new FrmRecibosEmitidos());

            btnControlStock = CreateMenuButton("Control de Stock");
            btnControlStock.Click += (s, e) => AbrirEnPanel(new FrmControlStock());

            btnAnalisisDatos = CreateMenuButton("Análisis de Datos");
            btnAnalisisDatos.Click += (s, e) => AbrirEnPanel(new FrmAnalisisDatos());

            btnConfiguracion = CreateMenuButton("Configuración");
            btnConfiguracion.Click += (s, e) => MessageBox.Show("Pantalla de configuración (prototipo).");

            btnSalir = CreateMenuButton("Salir");
            btnSalir.Click += (s, e) => Close();

            // Agregar en orden de arriba hacia abajo
            panelMenu.Controls.Add(btnSalir);
            panelMenu.Controls.Add(btnConfiguracion);
            panelMenu.Controls.Add(btnAnalisisDatos);
            panelMenu.Controls.Add(btnControlStock);
            panelMenu.Controls.Add(btnRecibosEmitidos);
            panelMenu.Controls.Add(btnEmitirRecibo);

            // Encabezado pequeño del menú
            lblTitulo = new Label
            {
                Text = "KIOSCO",
                Dock = DockStyle.Top,
                Height = 70,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelMenu.Controls.Add(lblTitulo);
            panelMenu.Controls.SetChildIndex(lblTitulo, panelMenu.Controls.Count - 1); // título arriba del todo

            // ===== Panel Superior =====
            panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = Color.FromArgb(55, 130, 200)
            };
            Controls.Add(panelTop);

            lblUsuario = new Label
            {
                Dock = DockStyle.Right,
                Width = 450,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Padding = new Padding(0, 0, 16, 0),
                Text = $"Empleado: {_empleado}  |  Rol: {_rol}"
            };
            panelTop.Controls.Add(lblUsuario);

            // ===== Panel Contenedor =====
            panelContenedor = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };
            Controls.Add(panelContenedor);
            panelContenedor.BringToFront();
        }

        private Button CreateMenuButton(string text)
        {
            return new Button
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 48,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 50, 55),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                TabStop = false
            };
        }

        private void AbrirEnPanel(Form form)
        {
            // Cargar un Form hijo dentro del contenedor
            panelContenedor.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(form);
            form.Show();
        }
    }
}
