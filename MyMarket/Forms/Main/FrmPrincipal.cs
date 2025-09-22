using System;
using System.Drawing;
using System.Windows.Forms;
using MyMarket.Data.Infrastructure;
using MyMarket.Data.Models;
using MyMarket.Data.Repositories;
using MyMarket.Forms.Analytics;
using MyMarket.Forms.Authentication;
using MyMarket.Forms.Inventory;
using MyMarket.Forms.Receipts;
using MyMarket.Forms.Users;
using MyMarket.Services.State;
using MyMarket.Services.State.Models;

namespace MyMarket.Forms.Main;

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
        private Button btnGestionUsuarios;
        private Button btnSalir;

        private readonly SqlConnectionFactory _connectionFactory;
        private readonly EmpleadoRepository _empleadoRepository;
        private readonly AppStateStorage _appStateStorage;
        private EmpleadoDto? _empleadoAutenticado;
        private readonly ContextMenuStrip _menuSesion;

        public FrmPrincipal(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _empleadoRepository = new EmpleadoRepository(_connectionFactory);
            _appStateStorage = new AppStateStorage();
            _menuSesion = new ContextMenuStrip();

            InitializeComponent();

            btnEmitirRecibo.Click += (s, e) => AbrirEnPanel(new FrmEmitirRecibo());
            btnRecibosEmitidos.Click += (s, e) => AbrirEnPanel(new FrmRecibosEmitidos());
            btnControlStock.Click += (s, e) => AbrirEnPanel(new FrmControlStock());
            btnAnalisisDatos.Click += (s, e) => AbrirEnPanel(new FrmAnalisisDatos());
            btnGestionUsuarios.Click += BtnGestionUsuarios_Click;
            btnSalir.Click += (s, e) => Close();

            lblUsuario.Cursor = Cursors.Hand;
            lblUsuario.Click += LblUsuario_Click;
            lblUsuario.ContextMenuStrip = _menuSesion;

            Load += FrmPrincipal_Load;
            FormClosing += FrmPrincipal_FormClosing;

            ActualizarEstadoSesion(null);
            RestaurarEstadoAnterior();
        }

        private void FrmPrincipal_Load(object? sender, EventArgs e)
        {
            AbrirEnPanel(new FrmBienvenida());
        }

        private void BtnGestionUsuarios_Click(object? sender, EventArgs e)
        {
            if (_empleadoAutenticado is null)
            {
                MessageBox.Show("Debe iniciar sesión para gestionar usuarios.", "Sesión requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!EsGerente(_empleadoAutenticado.RolDescripcion))
            {
                MessageBox.Show("Solo los gerentes pueden acceder a la gestión de usuarios.", "Acceso denegado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AbrirEnPanel(new FrmGestionUsuarios(_empleadoRepository, _empleadoAutenticado));
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
                GuardarEstadoActual();
            }
        }

        private void ActualizarEstadoSesion(EmpleadoDto? empleado)
        {
            _empleadoAutenticado = empleado;
            _menuSesion.Items.Clear();

            if (empleado is null)
            {
                lblUsuario.Text = "Iniciar sesión";
                btnGestionUsuarios.Visible = false;
                DeshabilitarOpciones();
                _menuSesion.Items.Add(new ToolStripMenuItem("Iniciar sesión", null, (_, _) => MostrarDialogoLogin()));
                return;
            }

            lblUsuario.Text = $"Empleado: {empleado.NombreParaMostrar}";
            btnGestionUsuarios.Visible = true;

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
            GuardarEstadoActual();
        }

        private void DeshabilitarOpciones()
        {
            btnEmitirRecibo.Enabled = false;
            btnRecibosEmitidos.Enabled = false;
            btnControlStock.Enabled = false;
            btnAnalisisDatos.Enabled = false;
            btnGestionUsuarios.Enabled = false;
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
                btnGestionUsuarios.Enabled = true;
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

        private void RestaurarEstadoAnterior()
        {
            var estado = _appStateStorage.Load();

            if (estado?.SesionActiva is not null)
            {
                var empleado = new EmpleadoDto
                {
                    IdEmpleado = estado.SesionActiva.IdEmpleado,
                    CuilCuit = estado.SesionActiva.CuilCuit,
                    Email = estado.SesionActiva.Email,
                    Activo = estado.SesionActiva.Activo,
                    IdRol = estado.SesionActiva.IdRol,
                    RolDescripcion = estado.SesionActiva.RolDescripcion,
                    Nombre = estado.SesionActiva.Nombre,
                    Apellido = estado.SesionActiva.Apellido
                };

                ActualizarEstadoSesion(empleado);
            }

            if (!string.IsNullOrWhiteSpace(estado?.EstadoVentana) &&
                Enum.TryParse<FormWindowState>(estado.EstadoVentana, true, out var estadoVentana))
            {
                WindowState = estadoVentana;
            }
        }

        private void FrmPrincipal_FormClosing(object? sender, FormClosingEventArgs e)
        {
            GuardarEstadoActual();
        }

        private void GuardarEstadoActual()
        {
            var estado = new AppState
            {
                EstadoVentana = WindowState.ToString()
            };

            if (_empleadoAutenticado is not null)
            {
                estado.SesionActiva = new EmpleadoState
                {
                    IdEmpleado = _empleadoAutenticado.IdEmpleado,
                    CuilCuit = _empleadoAutenticado.CuilCuit,
                    Email = _empleadoAutenticado.Email,
                    Activo = _empleadoAutenticado.Activo,
                    IdRol = _empleadoAutenticado.IdRol,
                    RolDescripcion = _empleadoAutenticado.RolDescripcion,
                    Nombre = _empleadoAutenticado.Nombre,
                    Apellido = _empleadoAutenticado.Apellido
                };
            }

            _appStateStorage.Save(estado);
        }

        private static bool EsGerente(string? rolDescripcion)
        {
            if (string.IsNullOrWhiteSpace(rolDescripcion))
            {
                return false;
            }

            return rolDescripcion.Trim().ToLowerInvariant().Contains("gerente");
        }

        private void AbrirEnPanel(Form form)
        {
            panelContenedor.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
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