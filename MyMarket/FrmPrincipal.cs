using System;
using System.Drawing;
using System.Windows.Forms;
using MyMarket.Data;
using MyMarket.Data.Models;

namespace MyMarket
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

        private readonly SqlConnectionFactory _connectionFactory;
        private readonly EmpleadoRepository _empleadoRepository;
        private EmpleadoDto? _empleadoAutenticado;
        private readonly ContextMenuStrip _menuSesion;

        public FrmPrincipal(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _empleadoRepository = new EmpleadoRepository(_connectionFactory);
            _menuSesion = new ContextMenuStrip();

            InitializeComponent();

            btnEmitirRecibo.Click += (s, e) => AbrirEnPanel(new FrmEmitirRecibo());
            btnRecibosEmitidos.Click += (s, e) => AbrirEnPanel(new FrmRecibosEmitidos());
            btnControlStock.Click += (s, e) => AbrirEnPanel(new FrmControlStock());
            btnAnalisisDatos.Click += (s, e) => AbrirEnPanel(new FrmAnalisisDatos());
            btnConfiguracion.Click += (s, e) => MessageBox.Show("Pantalla de configuración (prototipo).", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSalir.Click += (s, e) => Close();

            lblUsuario.Cursor = Cursors.Hand;
            lblUsuario.Click += LblUsuario_Click;
            lblUsuario.ContextMenuStrip = _menuSesion;

            Load += FrmPrincipal_Load;

            ActualizarEstadoSesion(null);
        }

        private void FrmPrincipal_Load(object? sender, EventArgs e)
        {
            AbrirEnPanel(new FrmBienvenida());
        }

        private void LblUsuario_Click(object? sender, EventArgs e)
        {
            if (_empleadoAutenticado is null)
            {
                MostrarDialogoLogin();
            }
            else
            {
                MostrarMenuSesion();
            }
        }

        private void MostrarMenuSesion()
        {
            if (_menuSesion.Items.Count == 0)
            {
                return;
            }

            var location = lblUsuario.PointToScreen(new Point(lblUsuario.Width, lblUsuario.Height));
            _menuSesion.Show(location, ToolStripDropDownDirection.BelowLeft);
        }

        private void MostrarDialogoLogin()
        {
            using var login = new FrmLogin(_empleadoRepository);
            if (login.ShowDialog(this) == DialogResult.OK && login.EmpleadoAutenticado is not null)
            {
                ActualizarEstadoSesion(login.EmpleadoAutenticado);
            }
        }

        private void ActualizarEstadoSesion(EmpleadoDto? empleado)
        {
            _empleadoAutenticado = empleado;
            _menuSesion.Items.Clear();

            if (empleado is null)
            {
                lblUsuario.Text = "Iniciar sesión";
                DeshabilitarOpciones();
                _menuSesion.Items.Add(new ToolStripMenuItem("Iniciar sesión", null, (_, _) => MostrarDialogoLogin()));
                return;
            }

            lblUsuario.Text = $"Empleado: {empleado.NombreParaMostrar}";

            var rolItem = new ToolStripMenuItem($"Rol: {empleado.RolDescripcion}")
            {
                Enabled = false
            };
            var cerrarSesionItem = new ToolStripMenuItem("Cerrar sesión", null, (_, _) => CerrarSesion());
            _menuSesion.Items.Add(rolItem);
            _menuSesion.Items.Add(new ToolStripSeparator());
            _menuSesion.Items.Add(cerrarSesionItem);

            AplicarPermisosPorRol(empleado.RolDescripcion);
        }

        private void CerrarSesion()
        {
            ActualizarEstadoSesion(null);
            AbrirEnPanel(new FrmBienvenida());
        }

        private void DeshabilitarOpciones()
        {
            btnEmitirRecibo.Enabled = false;
            btnRecibosEmitidos.Enabled = false;
            btnControlStock.Enabled = false;
            btnAnalisisDatos.Enabled = false;
            btnConfiguracion.Enabled = false;
        }

        private void AplicarPermisosPorRol(string rolDescripcion)
        {
            DeshabilitarOpciones();

            if (string.IsNullOrWhiteSpace(rolDescripcion))
            {
                return;
            }

            var rolNormalizado = rolDescripcion.Trim().ToLowerInvariant();

            if (rolNormalizado.Contains("gerente"))
            {
                btnEmitirRecibo.Enabled = true;
                btnRecibosEmitidos.Enabled = true;
                btnControlStock.Enabled = true;
                btnAnalisisDatos.Enabled = true;
                btnConfiguracion.Enabled = true;
                return;
            }

            if (rolNormalizado.Contains("admin") || rolNormalizado.Contains("gestor"))
            {
                btnEmitirRecibo.Enabled = true;
                btnRecibosEmitidos.Enabled = true;
                btnControlStock.Enabled = true;
                return;
            }

            if (rolNormalizado.Contains("vend"))
            {
                btnEmitirRecibo.Enabled = true;
                btnRecibosEmitidos.Enabled = true;
            }
        }

        private void AbrirEnPanel(Form form)
        {
            panelContenedor.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(form);
            form.Show();
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
    }
}
