using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MyMarket.Datos.Infraestructura;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;
using MyMarket.Formularios.Analitica;
using MyMarket.Formularios.Autenticacion;
using MyMarket.Formularios.Inventario;
using MyMarket.Formularios.Clientes;
using MyMarket.Formularios.Configuracion;
using MyMarket.Formularios.Recibos;
using MyMarket.Formularios.Usuarios;
using MyMarket.Servicios.Estado;
using MyMarket.Servicios.Estado.Modelos;

namespace MyMarket.Formularios.Principal;

/// <summary>
///     Ventana principal de la aplicación que actúa como contenedor para el resto de módulos.
/// </summary>
public partial class FrmPrincipal : Form
{
    private readonly SqlConnectionFactory _connectionFactory;
    private readonly EmpleadoRepository _empleadoRepository;
    private readonly ClienteRepository _clienteRepository;
    private readonly FacturaRepository _facturaRepository;
    private readonly AlmacenEstadoAplicacion _almacenEstadoAplicacion;
    private EmpleadoDto? _empleadoAutenticado;
    private readonly ContextMenuStrip _menuSesion;
    private Button? btnBackup;

    /// <summary>
    ///     Inicializa los servicios y configura los eventos principales de la interfaz.
    /// </summary>
    public FrmPrincipal(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        _empleadoRepository = new EmpleadoRepository(_connectionFactory);
        _clienteRepository = new ClienteRepository(_connectionFactory);
        _facturaRepository = new FacturaRepository(_connectionFactory);
        _almacenEstadoAplicacion = new AlmacenEstadoAplicacion();
        _menuSesion = new ContextMenuStrip();

        InitializeComponent();

        // rename Analisis button to Reportes in the UI
        try
        {
            if (btnAnalisisDatos != null)
            {
                btnAnalisisDatos.Text = "Reportes";
            }
        }
        catch
        {
            // ignore if designer control not present
        }

        // create backup button and place it visually in the left menu between Configuracion and Salir
        try
        {
            btnBackup = new Button
            {
                Text = "Realizar backup",
                AutoSize = false,
                BackColor = Color.FromArgb(55, 130, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false // visibility controlled by role
            };
            btnBackup.FlatAppearance.BorderSize = 0;
            btnBackup.Click += BtnBackup_Click;

            var menuParent = btnConfiguracion?.Parent ?? btnSalir?.Parent;
            if (menuParent != null)
            {
                // add as sibling and dock to bottom so it always stays at the bottom of the left menu
                menuParent.Controls.Add(btnBackup);

                // size to match an existing menu button if available
                if (btnConfiguracion != null)
                {
                    try { btnBackup.Size = btnConfiguracion.Size; } catch { btnBackup.Size = new Size(160, 40); }
                }
                else if (btnSalir != null)
                {
                    try { btnBackup.Size = new Size(btnSalir.Width, btnSalir.Height); } catch { btnBackup.Size = new Size(160, 40); }
                }
                else
                {
                    btnBackup.Size = new Size(160, 40);
                }

                // Dock to bottom so it stays at the very bottom of the menu container
                btnBackup.Dock = DockStyle.Bottom;
                btnBackup.BringToFront();
            }
            else
            {
                // Agregar al contenedor principal si no hay parent disponible
                Controls.Add(btnBackup);
                btnBackup.Size = new Size(160, 40);
                btnBackup.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                btnBackup.Location = new Point(12, Math.Max(8, Height - btnBackup.Height - 40));
            }
        }
        catch
        {
            btnBackup = null;
        }

        // Configura los botones del menú lateral para que abran los formularios correspondientes.
        // Nota: Los controles btnEmitirRecibo, btnRecibosEmitidos, etc. son inicializados por InitializeComponent()
        // y garantizados como no nulos en tiempo de ejecución del formulario.
        btnEmitirRecibo!.Click += (s, e) => AbrirEnPanel(new FrmEmitirRecibo(_connectionFactory, () => _empleadoAutenticado));
        btnRecibosEmitidos!.Click += (s, e) => AbrirEnPanel(new FrmRecibosEmitidos(_facturaRepository));
        btnClientesSuscriptos!.Click += BtnClientesSuscriptos_Click;
        btnControlStock!.Click += (s, e) => AbrirEnPanel(new FrmControlStock());
        btnAnalisisDatos!.Click += (s, e) => AbrirEnPanel(new FrmAnalisisDatos());
        btnGestionUsuarios!.Click += BtnGestionUsuarios_Click;
        btnConfiguracion!.Click += BtnConfiguracion_Click;
        btnSalir!.Click += (s, e) => Close();

        lblUsuario!.Cursor = Cursors.Hand;
        lblUsuario.Click += LblUsuario_Click;
        lblUsuario.ContextMenuStrip = _menuSesion;

        Load += FrmPrincipal_Load;
        FormClosing += FrmPrincipal_FormClosing;

        ActualizarEstadoSesion(null);
        RestaurarEstadoAnterior();
    }

    private static string? FindProjectRootContainingCsproj(string startDir)
    {
        try
        {
            var dir = new DirectoryInfo(startDir);
            for (int i = 0; i < 8 && dir != null; i++)
            {
                if (dir.GetFiles("*.csproj").Any()) return dir.FullName;
                dir = dir.Parent;
            }
        }
        catch
        {
        }

        return null;
    }

    /// <summary>
    ///     Carga por defecto la pantalla de bienvenida.
    /// </summary>
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

    /// <summary>
    ///     Abre el diálogo de login o el menú contextual según corresponda.
    /// </summary>
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

    /// <summary>
    ///     Despliega el formulario de inicio de sesión y actualiza la sesión en memoria.
    /// </summary>
    private void MostrarDialogoLogin()
    {
        using var login = new FrmLogin(_empleadoRepository);
        if (login.ShowDialog(this) == DialogResult.OK && login.EmpleadoAutenticado is not null)
        {
            ActualizarEstadoSesion(login.EmpleadoAutenticado);
            GuardarEstadoActual();
        }
    }

    /// <summary>
    ///     Configura la interfaz en función del empleado autenticado.
    /// </summary>
    private void ActualizarEstadoSesion(EmpleadoDto? empleado)
    {
        _empleadoAutenticado = empleado;
        _menuSesion.Items.Clear();

        if (empleado is null)
        {
            lblUsuario.Text = "Iniciar sesión";
            btnGestionUsuarios.Visible = false;
            AplicarPermisosPorRol(string.Empty);
            _menuSesion.Items.Add(new ToolStripMenuItem("Iniciar sesión", null, (_, _) => MostrarDialogoLogin()));

            if (btnBackup != null) btnBackup.Visible = false;
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

        if (btnBackup != null)
        {
            var rolNormalizado = empleado.RolDescripcion?.Trim().ToLowerInvariant() ?? string.Empty;
            // hide for cajero roles entirely
            btnBackup.Visible = !rolNormalizado.Contains("cajer");
            btnBackup.Enabled = !rolNormalizado.Contains("cajer");
        }
    }

    private void BtnBackup_Click(object? sender, EventArgs e)
    {
        if (btnBackup == null) return;

        // compute project root as default folder
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var projectRoot = FindProjectRootContainingCsproj(baseDir) ?? baseDir;

        using var dlg = new SaveFileDialog
        {
            Filter = "Backup files (*.bak)|*.bak|All files (*.*)|*.*",
            DefaultExt = "bak",
            Title = "Guardar backup de la base de datos",
            InitialDirectory = projectRoot
        };

        // determine db name
        string dbName;
        try
        {
            using var conn = _connectionFactory.CreateOpenConnection();
            using var cmdDb = conn.CreateCommand();
            cmdDb.CommandText = "SELECT DB_NAME()";
            var res = cmdDb.ExecuteScalar();
            dbName = res?.ToString() ?? "MyMarketDB";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible determinar el nombre de la base de datos. Detalle: {ex.Message}", "Backup",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        dlg.FileName = $"{dbName}_{timestamp}.bak";

        if (dlg.ShowDialog(this) != DialogResult.OK) return;
        var path = dlg.FileName;

        try
        {
            using var conn = _connectionFactory.CreateOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandTimeout = 600; // allow longer time for backup
            cmd.CommandText = $"BACKUP DATABASE [{dbName}] TO DISK = @path WITH INIT, NAME = @name";
            var p = cmd.CreateParameter();
            p.ParameterName = "@path";
            p.Value = path;
            cmd.Parameters.Add(p);
            var n = cmd.CreateParameter();
            n.ParameterName = "@name";
            n.Value = $"Backup {dbName} - {timestamp}";
            cmd.Parameters.Add(n);
            cmd.ExecuteNonQuery();

            MessageBox.Show($"Backup completado correctamente en:\n{path}", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ocurrió un error al realizar el backup. Detalle: {ex.Message}", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    ///     Restablece la aplicación a su estado inicial y limpia la sesión.
    /// </summary>
    private void CerrarSesion()
    {
        ActualizarEstadoSesion(null);
        AbrirEnPanel(new FrmBienvenida());
        GuardarEstadoActual();
    }

    /// <summary>
    ///     Deshabilita todas las opciones del menú lateral.
    /// </summary>
    private void DeshabilitarOpciones()
    {
        btnEmitirRecibo.Enabled = false;
        btnRecibosEmitidos.Enabled = false;
        btnControlStock.Enabled = false;
        btnClientesSuscriptos.Enabled = false;
        btnAnalisisDatos.Enabled = false;
        btnGestionUsuarios.Enabled = false;
        btnConfiguracion.Enabled = false;
    }

    /// <summary>
    ///     Activa las opciones según los permisos del rol del empleado.
    /// </summary>
    private void AplicarPermisosPorRol(string rolDescripcion)
    {
        DeshabilitarOpciones();

        if (string.IsNullOrWhiteSpace(rolDescripcion))
        {
            return;
        }

        var puedeGestionarClientesSuscriptos = TienePermisoClientesSuscriptos(rolDescripcion);
        var rolNormalizado = rolDescripcion.Trim().ToLowerInvariant();

        if (rolNormalizado.Contains("gerente"))
        {
            btnEmitirRecibo.Enabled = true;
            btnRecibosEmitidos.Enabled = true;
            btnControlStock.Enabled = true;
            btnAnalisisDatos.Enabled = true;
            btnGestionUsuarios.Enabled = true;
            btnConfiguracion.Enabled = true;
        }
        else if (rolNormalizado.Contains("admin") || rolNormalizado.Contains("gestor"))
        {
            btnEmitirRecibo.Enabled = true;
            btnRecibosEmitidos.Enabled = true;
            btnControlStock.Enabled = true;
        }
        else if (rolNormalizado.Contains("vend") || rolNormalizado.Contains("cajer"))
        {
            btnEmitirRecibo.Enabled = true;
            btnRecibosEmitidos.Enabled = true;
        }

        btnClientesSuscriptos.Enabled = puedeGestionarClientesSuscriptos;
    }

    /// <summary>
    ///     Controla el acceso al módulo de clientes suscriptos.
    /// </summary>
    private void BtnClientesSuscriptos_Click(object? sender, EventArgs e)
    {
        if (!TienePermisoClientesSuscriptos(_empleadoAutenticado?.RolDescripcion))
        {
            MessageBox.Show("Solo los gerentes o administradores pueden acceder a los clientes suscriptos.",
                "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        AbrirEnPanel(new FrmClientesSuscriptos(_clienteRepository));
    }

    private void BtnConfiguracion_Click(object? sender, EventArgs e)
    {
        if (_empleadoAutenticado is null)
        {
            MessageBox.Show("Debe iniciar sesión para acceder a la configuración.", "Sesión requerida",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!EsGerente(_empleadoAutenticado.RolDescripcion))
        {
            MessageBox.Show("Solo los gerentes pueden acceder a la configuración.", "Acceso denegado",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var form = new FrmConfiguracion();
        form.ShowDialog(this);
    }

    /// <summary>
    ///     Recupera del almacenamiento el estado de la aplicación para continuar donde se dejó.
    /// </summary>
    private void RestaurarEstadoAnterior()
    {
        var estado = _almacenEstadoAplicacion.Cargar();

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

    /// <summary>
    ///     Antes de cerrar, guarda la sesión actual y el estado de la ventana.
    /// </summary>
    private void FrmPrincipal_FormClosing(object? sender, FormClosingEventArgs e)
    {
        GuardarEstadoActual();
    }

    /// <summary>
    ///     Persiste el estado actual para recuperarlo la próxima vez.
    /// </summary>
    private void GuardarEstadoActual()
    {
        var estado = new EstadoAplicacion
        {
            EstadoVentana = WindowState.ToString()
        };

        if (_empleadoAutenticado is not null)
        {
            estado.SesionActiva = new EstadoEmpleado
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

        _almacenEstadoAplicacion.Guardar(estado);
    }

    /// <summary>
    ///     Determina si un rol tiene acceso a la gestión de clientes suscriptos.
    /// </summary>
    private static bool TienePermisoClientesSuscriptos(string? rolDescripcion)
    {
        if (string.IsNullOrWhiteSpace(rolDescripcion))
        {
            return false;
        }

        var rolNormalizado = rolDescripcion.Trim().ToLowerInvariant();

        return rolNormalizado.Contains("gerente") ||
               rolNormalizado.Contains("admin") ||
               rolNormalizado.Contains("gestor");
    }

    /// <summary>
    ///     Valida si el rol indicado corresponde a un gerente.
    /// </summary>
    private static bool EsGerente(string? rolDescripcion)
    {
        if (string.IsNullOrWhiteSpace(rolDescripcion))
        {
            return false;
        }

        return rolDescripcion.Trim().ToLowerInvariant().Contains("gerente");
    }

    /// <summary>
    ///     Muestra el formulario indicado dentro del panel contenedor.
    /// </summary>
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
}
