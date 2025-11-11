using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;
using MyMarket.Servicios;

namespace MyMarket.Formularios.Usuarios;

/// <summary>
///     Interfaz de administración de empleados con operaciones de alta, edición y cambio de estado.
/// </summary>
public partial class FrmGestionUsuarios : Form
{
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

    private readonly EmpleadoRepository _empleadoRepository;
    private readonly EmpleadoDto _empleadoActual;
    private readonly bool _puedeAdministrarUsuarios;
    private readonly SortableBindingList<EmpleadoDto> _todosEmpleados = new();
    private readonly SortableBindingList<EmpleadoDto> _empleadosFiltrados = new();
    private TextBox? _txtBuscar;
    private ComboBox? _cmbEstadoFiltro;

    /// <summary>
    ///     Configura dependencias, inicializa la grilla y determina los permisos disponibles.
    /// </summary>
    public FrmGestionUsuarios(EmpleadoRepository empleadoRepository, EmpleadoDto empleadoActual)
    {
        _empleadoRepository = empleadoRepository ?? throw new ArgumentNullException(nameof(empleadoRepository));
        _empleadoActual = empleadoActual ?? throw new ArgumentNullException(nameof(empleadoActual));
        _puedeAdministrarUsuarios = EsGerente(_empleadoActual);

        InitializeComponent();

        bindingSourceUsuarios.DataSource = _empleadosFiltrados;
        dgvUsuarios.DataSource = bindingSourceUsuarios;
        dgvUsuarios.SelectionChanged += DgvUsuarios_SelectionChanged;

        // Habilitar ordenamiento por columnas
        DataGridViewHelper.HabilitarOrdenamientoPorColumna(dgvUsuarios);

        btnEliminar.Visible = _puedeAdministrarUsuarios;
        btnEliminar.Enabled = false;
        btnEditar.Visible = _puedeAdministrarUsuarios;
        btnEditar.Enabled = false;

        txtCuil.KeyPress += TxtCuil_KeyPress;
        txtEmail.KeyPress += TxtEmail_KeyPress;

        Load += FrmGestionUsuarios_Load;
    }

    private void FrmGestionUsuarios_Load(object? sender, EventArgs e)
    {
        AgregarControlesBusqueda();
        CargarRoles();
        CargarEmpleados();
    }

    private void AgregarControlesBusqueda()
    {
        // Agregar controles de búsqueda en el panel de búsqueda (encima de la grilla)
        var lblBuscar = new Label
        {
            Text = "Buscar:",
            AutoSize = true,
            Location = new Point(12, 16),
            Anchor = AnchorStyles.Left
        };

        _txtBuscar = new TextBox
        {
            Width = 220,
            PlaceholderText = "Buscar por nombre, email, CUIL o rol...",
            Location = new Point(75, 13),
            Anchor = AnchorStyles.Left
        };
        _txtBuscar.TextChanged += (_, _) => FiltrarEmpleados();

        var lblEstadoFiltro = new Label
        {
            Text = "Estado:",
            AutoSize = true,
            Location = new Point(310, 16),
            Anchor = AnchorStyles.Left
        };

        _cmbEstadoFiltro = new ComboBox
        {
            Width = 100,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(370, 13),
            Anchor = AnchorStyles.Left
        };
        _cmbEstadoFiltro.Items.AddRange(new object[] { "Todos", "Activo", "Inactivo" });
        _cmbEstadoFiltro.SelectedIndex = 0;
        _cmbEstadoFiltro.SelectedIndexChanged += (_, _) => FiltrarEmpleados();

        panelBusqueda.Controls.Add(lblBuscar);
        panelBusqueda.Controls.Add(_txtBuscar);
        panelBusqueda.Controls.Add(lblEstadoFiltro);
        panelBusqueda.Controls.Add(_cmbEstadoFiltro);
    }

    private void FiltrarEmpleados()
    {
        var textoBusqueda = _txtBuscar?.Text.Trim().ToLowerInvariant() ?? string.Empty;
        var estadoSeleccionado = _cmbEstadoFiltro?.SelectedItem?.ToString() ?? "Todos";

        _empleadosFiltrados.RaiseListChangedEvents = false;
        _empleadosFiltrados.Clear();

        var empleadosFiltrados = _todosEmpleados.AsEnumerable();

        // Filtrar por texto de búsqueda
        if (!string.IsNullOrWhiteSpace(textoBusqueda))
        {
            empleadosFiltrados = empleadosFiltrados.Where(e =>
                e.CuilCuit.ToLowerInvariant().Contains(textoBusqueda) ||
                e.Email.ToLowerInvariant().Contains(textoBusqueda) ||
                (e.Nombre != null && e.Nombre.ToLowerInvariant().Contains(textoBusqueda)) ||
                (e.Apellido != null && e.Apellido.ToLowerInvariant().Contains(textoBusqueda)) ||
                e.RolDescripcion.ToLowerInvariant().Contains(textoBusqueda));
        }

        // Filtrar por estado
        if (estadoSeleccionado != "Todos")
        {
            var activo = estadoSeleccionado == "Activo";
            empleadosFiltrados = empleadosFiltrados.Where(e => e.Activo == activo);
        }

        foreach (var empleado in empleadosFiltrados)
        {
            _empleadosFiltrados.Add(empleado);
        }

        _empleadosFiltrados.RaiseListChangedEvents = true;
        bindingSourceUsuarios.ResetBindings(false);
        ActualizarEstadoBoton();
    }

    private void BtnRefrescar_Click(object? sender, EventArgs e)
    {
        CargarEmpleados();
    }

    /// <summary>
    ///     Realiza validaciones básicas y crea un nuevo usuario en la base de datos.
    /// </summary>
    private void BtnAgregar_Click(object? sender, EventArgs e)
    {
        var cuil = txtCuil.Text.Trim();
        var nombre = txtNombre.Text.Trim();
        var apellido = txtApellido.Text.Trim();
        var email = txtEmail.Text.Trim();
        var contrasenia = txtContrasenia.Text;
        var rolSeleccionado = cboRol.SelectedItem as RolDto;
        var activo = chkActivo.Checked;

        if (string.IsNullOrWhiteSpace(cuil))
        {
            MessageBox.Show("Debe ingresar el CUIL/CUIT.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtCuil.Focus();
            return;
        }

        if (cuil.Length != 11 || !cuil.All(char.IsDigit))
        {
            MessageBox.Show("El CUIL/CUIT debe contener 11 dígitos numéricos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtCuil.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(nombre))
        {
            MessageBox.Show("Debe ingresar el nombre del empleado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtNombre.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(apellido))
        {
            MessageBox.Show("Debe ingresar el apellido del empleado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtApellido.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            MessageBox.Show("Debe ingresar un correo electrónico.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtEmail.Focus();
            return;
        }

        if (!EmailRegex.IsMatch(email))
        {
            MessageBox.Show("Debe ingresar un correo electrónico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtEmail.Focus();
            return;
        }
        if (rolSeleccionado is null)
        {
            MessageBox.Show("Debe seleccionar un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cboRol.Focus();
            return;
        }

        // Validar que un gerente no pueda crear otro gerente
        if (EsGerente(rolSeleccionado.Descripcion))
        {
            MessageBox.Show("No tiene permisos para crear usuarios con rol de gerente.", "Acceso denegado",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cboRol.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(contrasenia))
        {
            MessageBox.Show("Debe ingresar una contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtContrasenia.Focus();
            return;
        }

        if (contrasenia.Length < 6)
        {
            MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtContrasenia.Focus();
            return;
        }

        var empleadosActuales = bindingSourceUsuarios.Cast<EmpleadoDto>().ToList();

        if (empleadosActuales.Any(e => string.Equals(e.CuilCuit, cuil, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show("Ya existe un usuario registrado con el mismo CUIL/CUIT.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtCuil.Focus();
            return;
        }

        if (empleadosActuales.Any(e => string.Equals(e.Email, email, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show("Ya existe un usuario registrado con el mismo correo electrónico.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtEmail.Focus();
            return;
        }
        try
        {
            var nuevoEmpleado = new EmpleadoDto
            {
                CuilCuit = cuil,
                Email = email,
                Activo = activo,
                IdRol = rolSeleccionado.IdRol,
                RolDescripcion = rolSeleccionado.Descripcion,
                Nombre = nombre,
                Apellido = apellido
            };

            _empleadoRepository.CrearEmpleado(nuevoEmpleado, contrasenia);
            MessageBox.Show("Usuario creado correctamente.", "Gestión de usuarios",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
            CargarEmpleados();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible crear el usuario. Detalle: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    ///     Cambia el estado activo/inactivo del empleado seleccionado.
    /// </summary>
    private void BtnEliminar_Click(object? sender, EventArgs e)
    {
        if (!_puedeAdministrarUsuarios)
        {
            MessageBox.Show("No tiene permisos para cambiar el estado de los usuarios.", "Acceso denegado",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (dgvUsuarios.CurrentRow?.DataBoundItem is not EmpleadoDto empleado)
        {
            MessageBox.Show("Debe seleccionar un usuario.", "Gestión de usuarios",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!PuedeGestionarEmpleado(empleado))
        {
            MessageBox.Show("Solo puede modificar el estado de usuarios con un rol inferior.", "Acción no permitida",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var activar = !empleado.Activo;
        var mensajeConfirmacion = activar
            ? $"¿Confirma que desea activar al usuario {empleado.NombreParaMostrar}?"
            : $"¿Confirma que desea desactivar al usuario {empleado.NombreParaMostrar}?";

        if (MessageBox.Show(mensajeConfirmacion,
                "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        try
        {
            _empleadoRepository.CambiarEstadoEmpleado(empleado.IdEmpleado, activar);
            CargarEmpleados();
            var mensaje = activar
                ? "El usuario se activó correctamente."
                : "El usuario se desactivó correctamente.";
            MessageBox.Show(mensaje, "Gestión de usuarios",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible cambiar el estado del usuario. Detalle: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    ///     Obtiene la lista de empleados desde la base y la enlaza a la grilla.
    /// </summary>
    private void CargarEmpleados()
    {
        try
        {
            var empleados = _empleadoRepository.ObtenerEmpleados();
            
            _todosEmpleados.RaiseListChangedEvents = false;
            _todosEmpleados.Clear();
            foreach (var empleado in empleados)
            {
                _todosEmpleados.Add(empleado);
            }
            _todosEmpleados.RaiseListChangedEvents = true;
            
            FiltrarEmpleados();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible obtener la lista de usuarios. Detalle: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DgvUsuarios_SelectionChanged(object? sender, EventArgs e)
    {
        ActualizarEstadoBoton();
    }
    /// <summary>
    ///     Carga los roles disponibles para asignar a un empleado.
    /// </summary>
    private void CargarRoles()
    {
        try
        {
            var roles = _empleadoRepository.ObtenerRoles();
            cboRol.DataSource = roles.ToList();
            cboRol.DisplayMember = nameof(RolDto.Descripcion);
            cboRol.ValueMember = nameof(RolDto.IdRol);
            cboRol.SelectedIndex = roles.Count > 0 ? 0 : -1;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible cargar los roles. Detalle: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    ///     Restablece los campos del formulario de alta.
    /// </summary>
    private void LimpiarFormulario()
    {
        txtCuil.Clear();
        txtNombre.Clear();
        txtApellido.Clear();
        txtEmail.Clear();
        txtContrasenia.Clear();
        chkActivo.Checked = true;
        if (cboRol.Items.Count > 0)
        {
            cboRol.SelectedIndex = 0;
        }
        txtCuil.Focus();
    }

    /// <summary>
    ///     Actualiza el estado de los botones de acciones según el usuario seleccionado y los permisos.
    /// </summary>
    private void ActualizarEstadoBoton()
    {
        if (!_puedeAdministrarUsuarios)
        {
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;
            return;
        }

        if (dgvUsuarios.CurrentRow?.DataBoundItem is not EmpleadoDto empleado)
        {
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Text = "Cambiar estado";
            return;
        }

        var puedeGestionar = PuedeGestionarEmpleado(empleado);
        btnEliminar.Text = empleado.Activo ? "Desactivar usuario" : "Activar usuario";
        btnEliminar.Enabled = puedeGestionar;
        btnEditar.Enabled = puedeGestionar;
    }

    /// <summary>
    ///     Muestra un diálogo modal para editar el usuario seleccionado.
    /// </summary>
    private void BtnEditar_Click(object? sender, EventArgs e)
    {
        if (dgvUsuarios.CurrentRow?.DataBoundItem is not EmpleadoDto empleado)
        {
            MessageBox.Show("Debe seleccionar un usuario.", "Gestión de usuarios",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!PuedeGestionarEmpleado(empleado))
        {
            MessageBox.Show("Solo puede editar usuarios con un rol inferior.", "Acción no permitida",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        List<RolDto> rolesDisponibles;
        if (cboRol.DataSource is IEnumerable<RolDto> rolesDataSource)
        {
            rolesDisponibles = rolesDataSource.ToList();
        }
        else
        {
            try
            {
                rolesDisponibles = _empleadoRepository.ObtenerRoles().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No fue posible obtener los roles. Detalle: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Filtrar roles para que un gerente no pueda asignar el rol de gerente
        rolesDisponibles = rolesDisponibles.Where(r => !EsGerente(r.Descripcion)).ToList();

        if (rolesDisponibles.Count == 0)
        {
            MessageBox.Show("No hay roles disponibles para asignar.", "Gestión de usuarios",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialogo = new EditarUsuarioDialog(empleado, rolesDisponibles);
        if (dialogo.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var email = dialogo.Email;
        var rolSeleccionado = dialogo.RolSeleccionado;
        var activo = dialogo.Activo;
        var contrasenia = dialogo.Contrasenia;

        if (string.IsNullOrWhiteSpace(email))
        {
            MessageBox.Show("Debe ingresar un correo electrónico.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!EmailRegex.IsMatch(email))
        {
            MessageBox.Show("Debe ingresar un correo electrónico válido.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (rolSeleccionado is null)
        {
            MessageBox.Show("Debe seleccionar un rol.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!string.IsNullOrEmpty(contrasenia) && contrasenia.Length < 6)
        {
            MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var empleadoActualizado = new EmpleadoDto
        {
            IdEmpleado = empleado.IdEmpleado,
            CuilCuit = empleado.CuilCuit,
            Email = email,
            Activo = activo,
            IdRol = rolSeleccionado.IdRol,
            RolDescripcion = rolSeleccionado.Descripcion,
            Nombre = dialogo.Nombre,
            Apellido = dialogo.Apellido
        };

        try
        {
            _empleadoRepository.ActualizarEmpleado(empleadoActualizado, contrasenia);
            MessageBox.Show("El usuario se actualizó correctamente.", "Gestión de usuarios",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            var idSeleccionado = empleadoActualizado.IdEmpleado;
            CargarEmpleados();
            SeleccionarFilaPorId(idSeleccionado);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible actualizar el usuario. Detalle: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    ///     Comprueba si el usuario actual puede modificar al empleado indicado.
    /// </summary>
    private bool PuedeGestionarEmpleado(EmpleadoDto empleado)
    {
        if (!_puedeAdministrarUsuarios)
        {
            return false;
        }

        if (empleado.IdEmpleado == _empleadoActual.IdEmpleado)
        {
            return false;
        }

        // Un gerente no puede gestionar a otro gerente
        if (EsGerente(empleado.RolDescripcion))
        {
            return false;
        }

        if (_empleadoActual.IdRol > 0 && empleado.IdRol > 0)
        {
            return _empleadoActual.IdRol < empleado.IdRol;
        }

        var jerarquiaActual = ObtenerPrioridadRol(_empleadoActual.RolDescripcion);
        var jerarquiaObjetivo = ObtenerPrioridadRol(empleado.RolDescripcion);

        return jerarquiaActual > jerarquiaObjetivo;
    }

    /// <summary>
    ///     Asigna prioridades numéricas a los roles conocidos para compararlos.
    /// </summary>
    private static int ObtenerPrioridadRol(string? rolDescripcion)
    {
        if (string.IsNullOrWhiteSpace(rolDescripcion))
        {
            return 0;
        }

        var rol = rolDescripcion.Trim().ToLowerInvariant();

        if (rol.Contains("gerente"))
        {
            return 4;
        }

        if (rol.Contains("admin"))
        {
            return 3;
        }

        if (rol.Contains("supervisor") || rol.Contains("encarg"))
        {
            return 2;
        }

        if (rol.Contains("vend") || rol.Contains("caj") || rol.Contains("operario"))
        {
            return 1;
        }

        return 1;
    }

    /// <summary>
    ///     Determina si el empleado tiene rol de gerente.
    /// </summary>
    private static bool EsGerente(EmpleadoDto empleado)
    {
        if (empleado is null)
        {
            return false;
        }

        if (empleado.IdRol == 1)
        {
            return true;
        }

        return !string.IsNullOrWhiteSpace(empleado.RolDescripcion) &&
               empleado.RolDescripcion.Trim().ToLowerInvariant().Contains("gerente");
    }

    /// <summary>
    ///     Determina si una descripción de rol corresponde a un gerente.
    /// </summary>
    private static bool EsGerente(string? rolDescripcion)
    {
        if (string.IsNullOrWhiteSpace(rolDescripcion))
        {
            return false;
        }

        return rolDescripcion.Trim().ToLowerInvariant().Contains("gerente");
    }

    private static void TxtCuil_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (char.IsControl(e.KeyChar))
        {
            return;
        }

        if (!char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private static void TxtEmail_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (char.IsControl(e.KeyChar))
        {
            return;
        }

        if (char.IsWhiteSpace(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    /// <summary>
    ///     Busca en la grilla la fila correspondiente al identificador indicado.
    /// </summary>
    private void SeleccionarFilaPorId(int idEmpleado)
    {
        foreach (DataGridViewRow row in dgvUsuarios.Rows)
        {
            if (row.DataBoundItem is not EmpleadoDto empleado)
            {
                continue;
            }

            if (empleado.IdEmpleado != idEmpleado)
            {
                continue;
            }

            row.Selected = true;
            if (row.Cells.Count > 0)
            {
                dgvUsuarios.CurrentCell = row.Cells[0];
            }

            break;
        }
    }

    private void TxtBuscar_TextChanged(object? sender, EventArgs e)
    {
        AplicarFiltro();
    }

    private void CmbEstadoFiltro_SelectedIndexChanged(object? sender, EventArgs e)
    {
        AplicarFiltro();
    }

    /// <summary>
    ///     Aplica el filtro a la lista de empleados mostrados en la grilla según los criterios de búsqueda.
    /// </summary>
    private void AplicarFiltro()
    {
        var textoBuscar = _txtBuscar?.Text.Trim().ToLowerInvariant() ?? string.Empty;
        var estadoActivo = _cmbEstadoFiltro?.SelectedItem?.ToString() == "Activos";

        _empleadosFiltrados.RaiseListChangedEvents = false;
        _empleadosFiltrados.Clear();

        foreach (var empleado in _todosEmpleados)
        {
            var incluir = true;

            if (!string.IsNullOrWhiteSpace(textoBuscar))
            {
                incluir = empleado.Nombre?.ToLowerInvariant().Contains(textoBuscar) == true ||
                          empleado.Apellido?.ToLowerInvariant().Contains(textoBuscar) == true ||
                          empleado.Email?.ToLowerInvariant().Contains(textoBuscar) == true;
            }

            if (incluir && _cmbEstadoFiltro != null)
            {
                incluir = estadoActivo == empleado.Activo;
            }

            if (incluir)
            {
                _empleadosFiltrados.Add(empleado);
            }
        }

        _empleadosFiltrados.RaiseListChangedEvents = true;
        bindingSourceUsuarios.DataSource = _empleadosFiltrados;
        bindingSourceUsuarios.ResetBindings(false);

        ActualizarEstadoBoton();
    }

    /// <summary>
    ///     Cuadro de diálogo utilizado para editar la información de un usuario.
    /// </summary>
    private sealed class EditarUsuarioDialog : Form
    {
        private readonly TextBox _txtNombre;
        private readonly TextBox _txtApellido;
        private readonly TextBox _txtEmail;
        private readonly ComboBox _cboRol;
        private readonly CheckBox _chkActivo;
        private readonly TextBox _txtContrasenia;

        public string Nombre => _txtNombre.Text.Trim();
        public string Apellido => _txtApellido.Text.Trim();
        public string Email => _txtEmail.Text.Trim();
        public RolDto? RolSeleccionado => _cboRol.SelectedItem as RolDto;
        public bool Activo => _chkActivo.Checked;
        public string? Contrasenia => string.IsNullOrWhiteSpace(_txtContrasenia.Text) ? null : _txtContrasenia.Text;

        public EditarUsuarioDialog(EmpleadoDto empleado, IReadOnlyCollection<RolDto> roles)
        {
            if (empleado is null)
            {
                throw new ArgumentNullException(nameof(empleado));
            }

            if (roles is null)
            {
                throw new ArgumentNullException(nameof(roles));
            }

            Text = "Editar usuario";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(12);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));

            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var lblCuilTitulo = new Label
            {
                Text = "CUIL/CUIT:",
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            var lblCuilValor = new Label
            {
                Text = empleado.CuilCuit,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };

            var lblNombre = new Label
            {
                Text = "Nombre:",
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            _txtNombre = new TextBox
            {
                Text = empleado.Nombre ?? string.Empty,
                Dock = DockStyle.Fill,
                MaxLength = 100
            };

            var lblApellido = new Label
            {
                Text = "Apellido:",
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            _txtApellido = new TextBox
            {
                Text = empleado.Apellido ?? string.Empty,
                Dock = DockStyle.Fill,
                MaxLength = 100
            };

            var lblEmail = new Label
            {
                Text = "Correo electrónico:",
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            _txtEmail = new TextBox
            {
                Text = empleado.Email,
                Dock = DockStyle.Fill
            };

            var lblContrasenia = new Label
            {
                Text = "Contraseña (opcional):",
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            _txtContrasenia = new TextBox
            {
                Dock = DockStyle.Fill,
                UseSystemPasswordChar = true
            };

            var lblRol = new Label
            {
                Text = "Rol:",
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            _cboRol = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = roles.ToList(),
                DisplayMember = nameof(RolDto.Descripcion),
                ValueMember = nameof(RolDto.IdRol)
            };

            var rolSeleccionado = roles.FirstOrDefault(r => r.IdRol == empleado.IdRol);
            if (rolSeleccionado is not null)
            {
                _cboRol.SelectedItem = rolSeleccionado;
            }

            _chkActivo = new CheckBox
            {
                Text = "Usuario activo",
                Checked = empleado.Activo,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };

            layout.Controls.Add(lblCuilTitulo, 0, 0);
            layout.Controls.Add(lblCuilValor, 1, 0);
            layout.Controls.Add(lblNombre, 0, 1);
            layout.Controls.Add(_txtNombre, 1, 1);
            layout.Controls.Add(lblApellido, 0, 2);
            layout.Controls.Add(_txtApellido, 1, 2);
            layout.Controls.Add(lblEmail, 0, 3);
            layout.Controls.Add(_txtEmail, 1, 3);
            layout.Controls.Add(lblContrasenia, 0, 4);
            layout.Controls.Add(_txtContrasenia, 1, 4);
            layout.Controls.Add(lblRol, 0, 5);
            layout.Controls.Add(_cboRol, 1, 5);
            layout.Controls.Add(_chkActivo, 1, 6);

            var panelBotones = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 12, 0, 0)
            };

            var btnCancelar = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                AutoSize = true
            };

            var btnGuardar = new Button
            {
                Text = "Guardar cambios",
                AutoSize = true,
                BackColor = Color.FromArgb(55, 130, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += (_, _) =>
            {
                if (string.IsNullOrWhiteSpace(Nombre))
                {
                    MessageBox.Show("Debe ingresar el nombre del empleado.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Apellido))
                {
                    MessageBox.Show("Debe ingresar el apellido del empleado.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _txtApellido.Focus();
                    return;
                }

                if (_cboRol.SelectedItem is null)
                {
                    MessageBox.Show("Debe seleccionar un rol.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            };

            panelBotones.Controls.Add(btnCancelar);
            panelBotones.Controls.Add(btnGuardar);

            layout.Controls.Add(panelBotones, 0, 7);
            layout.SetColumnSpan(panelBotones, 2);

            Controls.Add(layout);

            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
        }
    }
}
