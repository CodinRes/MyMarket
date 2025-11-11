using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;
using MyMarket.Servicios;

namespace MyMarket.Formularios.Clientes;

/// <summary>
///     Pantalla de administración de clientes suscriptos conectada a la base de datos.
/// </summary>
public partial class FrmClientesSuscriptos : Form
{
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

    private readonly SortableBindingList<ClienteViewModel> _todosClientes = new();
    private readonly SortableBindingList<ClienteViewModel> _clientesFiltrados = new();
    private readonly ClienteRepository _clienteRepository;
    private ClienteViewModel? _clienteSeleccionado;
    private bool _esEdicion;
    private TextBox? _txtBuscar;
    private ComboBox? _cmbEstadoFiltro;

    public FrmClientesSuscriptos(ClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));

        InitializeComponent();

        dgvClientes.DataSource = bindingSourceClientes;
        bindingSourceClientes.DataSource = _clientesFiltrados;
        dgvClientes.SelectionChanged += DgvClientes_SelectionChanged;

        // Habilitar ordenamiento por columnas
        DataGridViewHelper.HabilitarOrdenamientoPorColumna(dgvClientes);

        btnEditar.Enabled = false;
        btnToggleEstado.Enabled = false;

        cboEstado.Items.AddRange(new object[] { "Activo", "Inactivo" });
        if (cboEstado.Items.Count > 0)
        {
            cboEstado.SelectedIndex = 0;
        }

        // Limitar entrada del DNI a solo números
        txtDni.KeyPress += TxtDni_KeyPress;
    }

    private void FrmClientesSuscriptos_Load(object? sender, EventArgs e)
    {
        AgregarControlesBusqueda();
        CargarClientesDesdeBase();
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
            Width = 250,
            PlaceholderText = "Buscar por DNI, nombre, apellido o email...",
            Location = new Point(75, 13),
            Anchor = AnchorStyles.Left
        };
        _txtBuscar.TextChanged += TxtBuscar_TextChanged;

        var lblEstadoFiltro = new Label
        {
            Text = "Estado:",
            AutoSize = true,
            Location = new Point(340, 16),
            Anchor = AnchorStyles.Left
        };

        _cmbEstadoFiltro = new ComboBox
        {
            Width = 120,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(400, 13),
            Anchor = AnchorStyles.Left
        };
        _cmbEstadoFiltro.Items.AddRange(new object[] { "Todos", "Activo", "Inactivo" });
        _cmbEstadoFiltro.SelectedIndex = 0;
        _cmbEstadoFiltro.SelectedIndexChanged += CmbEstadoFiltro_SelectedIndexChanged;

        panelBusqueda.Controls.Add(lblBuscar);
        panelBusqueda.Controls.Add(_txtBuscar);
        panelBusqueda.Controls.Add(lblEstadoFiltro);
        panelBusqueda.Controls.Add(_cmbEstadoFiltro);
    }

    private void TxtBuscar_TextChanged(object? sender, EventArgs e)
    {
        FiltrarClientes();
    }

    private void CmbEstadoFiltro_SelectedIndexChanged(object? sender, EventArgs e)
    {
        FiltrarClientes();
    }

    private void FiltrarClientes()
    {
        var textoBusqueda = _txtBuscar?.Text.Trim().ToLowerInvariant() ?? string.Empty;
        var estadoSeleccionado = _cmbEstadoFiltro?.SelectedItem?.ToString() ?? "Todos";

        _clientesFiltrados.RaiseListChangedEvents = false;
        _clientesFiltrados.Clear();

        var clientesFiltrados = _todosClientes.AsEnumerable();

        // Filtrar por texto de búsqueda
        if (!string.IsNullOrWhiteSpace(textoBusqueda))
        {
            clientesFiltrados = clientesFiltrados.Where(c =>
                c.Dni.ToLowerInvariant().Contains(textoBusqueda) ||
                c.Nombre.ToLowerInvariant().Contains(textoBusqueda) ||
                c.Apellido.ToLowerInvariant().Contains(textoBusqueda) ||
                c.Email.ToLowerInvariant().Contains(textoBusqueda));
        }

        // Filtrar por estado
        if (estadoSeleccionado != "Todos")
        {
            var activo = estadoSeleccionado == "Activo";
            clientesFiltrados = clientesFiltrados.Where(c => c.Activo == activo);
        }

        foreach (var cliente in clientesFiltrados)
        {
            _clientesFiltrados.Add(cliente);
        }

        _clientesFiltrados.RaiseListChangedEvents = true;
        bindingSourceClientes.ResetBindings(false);
        EstablecerClienteSeleccionadoDesdeFilaActual();
    }

    private void FrmClientesSuscriptos_FormClosed(object? sender, FormClosedEventArgs e)
    {
        dgvClientes.DataSource = null;
        bindingSourceClientes.Dispose();
    }

    private void BtnNuevo_Click(object? sender, EventArgs e)
    {
        _esEdicion = false;
        _clienteSeleccionado = null;
        grpFormulario.Text = "Nuevo cliente";
        LimpiarFormulario();
        txtDni.Focus();
    }

    private void BtnEditar_Click(object? sender, EventArgs e)
    {
        if (_clienteSeleccionado is null)
        {
            MessageBox.Show("Debe seleccionar un cliente para editarlo.", "Clientes suscriptos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialogo = new EditarClienteDialog(_clienteSeleccionado);
        if (dialogo.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var clienteActualizado = dialogo.ClienteActualizado;
        if (clienteActualizado is null)
        {
            return;
        }

        try
        {
            if (!string.Equals(clienteActualizado.Dni, _clienteSeleccionado.Dni, StringComparison.OrdinalIgnoreCase) &&
                _clienteRepository.ExisteClientePorDni(clienteActualizado.Dni))
            {
                MessageBox.Show("Ya existe un cliente con el DNI ingresado.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_clienteRepository.ExisteClientePorEmail(clienteActualizado.Email, _clienteSeleccionado.Dni))
            {
                MessageBox.Show("Ya existe un cliente con el correo electrónico ingresado.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _clienteRepository.ActualizarCliente(clienteActualizado, _clienteSeleccionado.IdCliente);
            MessageBox.Show("Los datos del cliente se actualizaron correctamente.", "Clientes suscriptos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarClientesDesdeBase(clienteActualizado.Dni);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible actualizar los datos del cliente. Detalle: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnActualizar_Click(object? sender, EventArgs e)
    {
        if (CargarClientesDesdeBase())
        {
            MessageBox.Show("La lista de clientes se actualizó desde la base de datos.", "Clientes suscriptos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void BtnGuardar_Click(object? sender, EventArgs e)
    {
        if (!ValidarFormulario(out var mensajeValidacion))
        {
            MessageBox.Show(mensajeValidacion, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var estadoSeleccionado = cboEstado.SelectedItem?.ToString() ?? string.Empty;
        var clienteDto = new ClienteDto
        {
            Dni = txtDni.Text.Trim(),
            Nombre = txtNombre.Text.Trim(),
            Apellido = txtApellido.Text.Trim(),
            Direccion = txtDireccion.Text.Trim(),
            Email = txtEmail.Text.Trim(),
            Activo = string.Equals(estadoSeleccionado, "Activo", StringComparison.OrdinalIgnoreCase)
        };

        try
        {
            var mensajeExito = GuardarCliente(clienteDto);
            MessageBox.Show(mensajeExito, "Clientes suscriptos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
            grpFormulario.Text = "Detalle de cliente";
            _esEdicion = false;
            _clienteSeleccionado = null;
            btnEditar.Enabled = false;

            CargarClientesDesdeBase(clienteDto.Dni);
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible guardar los datos del cliente. Detalle: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private string GuardarCliente(ClienteDto clienteDto)
    {
        if (_esEdicion && _clienteSeleccionado is not null)
        {
            var idClienteOriginal = _clienteSeleccionado.IdCliente;

            if (!string.Equals(clienteDto.Dni, _clienteSeleccionado.Dni, StringComparison.OrdinalIgnoreCase) &&
                _clienteRepository.ExisteClientePorDni(clienteDto.Dni))
            {
                throw new InvalidOperationException("Ya existe un cliente con el DNI ingresado.");
            }

            if (_clienteRepository.ExisteClientePorEmail(clienteDto.Email, _clienteSeleccionado.Dni))
            {
                throw new InvalidOperationException("Ya existe un cliente con el correo electrónico ingresado.");
            }

            _clienteRepository.ActualizarCliente(clienteDto, idClienteOriginal);
            return "Los datos del cliente se actualizaron correctamente.";
        }

        if (_clienteRepository.ExisteClientePorDni(clienteDto.Dni))
        {
            throw new InvalidOperationException("Ya existe un cliente con el DNI ingresado.");
        }

        if (_clienteRepository.ExisteClientePorEmail(clienteDto.Email, null))
        {
            throw new InvalidOperationException("Ya existe un cliente con el correo electrónico ingresado.");
        }

        _clienteRepository.CrearCliente(clienteDto);
        return "El cliente se registró correctamente.";
    }

    private void BtnCancelar_Click(object? sender, EventArgs e)
    {
        LimpiarFormulario();
        grpFormulario.Text = "Detalle de cliente";
        _esEdicion = false;
        _clienteSeleccionado = null;
        EstablecerClienteSeleccionadoDesdeFilaActual();
    }

    private void DgvClientes_SelectionChanged(object? sender, EventArgs e)
    {
        EstablecerClienteSeleccionadoDesdeFilaActual();
    }

    private void EstablecerClienteSeleccionadoDesdeFilaActual()
    {
        if (dgvClientes.CurrentRow?.DataBoundItem is ClienteViewModel cliente)
        {
            _clienteSeleccionado = cliente;
            btnEditar.Enabled = true;
            btnToggleEstado.Enabled = true;
            btnToggleEstado.Text = cliente.Activo ? "Desactivar cliente" : "Activar cliente";
        }
        else
        {
            _clienteSeleccionado = null;
            btnEditar.Enabled = false;
            btnToggleEstado.Enabled = false;
            btnToggleEstado.Text = "Cambiar estado";
        }
    }

    private void BtnToggleEstado_Click(object? sender, EventArgs e)
    {
        if (_clienteSeleccionado is null)
        {
            MessageBox.Show("Debe seleccionar un cliente para cambiar su estado.", "Clientes suscriptos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var activar = !_clienteSeleccionado.Activo;
        var accion = activar ? "activar" : "desactivar";
        var confirmacion = MessageBox.Show(
            $"¿Está seguro de {accion} al cliente '{_clienteSeleccionado.Nombre} {_clienteSeleccionado.Apellido}'?",
            "Confirmar",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirmacion != DialogResult.Yes)
        {
            return;
        }

        try
        {
            var clienteDto = new ClienteDto
            {
                IdCliente = _clienteSeleccionado.IdCliente,
                Dni = _clienteSeleccionado.Dni,
                Nombre = _clienteSeleccionado.Nombre,
                Apellido = _clienteSeleccionado.Apellido,
                Direccion = _clienteSeleccionado.Direccion,
                Email = _clienteSeleccionado.Email,
                Activo =activar
            };

            _clienteRepository.ActualizarCliente(clienteDto, _clienteSeleccionado.IdCliente);
            MessageBox.Show($"El cliente se {(activar ? "activó" : "desactivó")} correctamente.", "Clientes suscriptos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            CargarClientesDesdeBase(_clienteSeleccionado.Dni);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue possible cambiar el estado del cliente. Detalle: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private bool CargarClientesDesdeBase(string? dniSeleccionado = null)
    {
        try
        {
            var clientes = _clienteRepository.ObtenerClientes();

            _todosClientes.RaiseListChangedEvents = false;
            _todosClientes.Clear();
            foreach (var cliente in clientes)
            {
                _todosClientes.Add(new ClienteViewModel
                {
                    IdCliente = cliente.IdCliente,
                    Dni = cliente.Dni,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Direccion = cliente.Direccion,
                    Email = cliente.Email,
                    Activo = cliente.Activo,
                    FechaRegistro = cliente.FechaRegistro
                });
            }
            _todosClientes.RaiseListChangedEvents = true;

            FiltrarClientes();
            SeleccionarClienteEnGrilla(dniSeleccionado);
            EstablecerClienteSeleccionadoDesdeFilaActual();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible obtener los clientes registrados. Detalle: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    private void SeleccionarClienteEnGrilla(string? dniSeleccionado)
    {
        if (dgvClientes.Rows.Count == 0)
        {
            return;
        }

        dgvClientes.ClearSelection();
        DataGridViewRow? filaSeleccionada = null;

        if (!string.IsNullOrWhiteSpace(dniSeleccionado))
        {
            foreach (DataGridViewRow row in dgvClientes.Rows)
            {
                if (row.DataBoundItem is ClienteViewModel cliente &&
                    string.Equals(cliente.Dni, dniSeleccionado, StringComparison.OrdinalIgnoreCase))
                {
                    filaSeleccionada = row;
                    break;
                }
            }
        }

        filaSeleccionada ??= dgvClientes.Rows[0];
        filaSeleccionada.Selected = true;
        dgvClientes.CurrentCell = filaSeleccionada.Cells[0];
    }

    private bool ValidarFormulario(out string mensaje)
    {
        var dni = txtDni.Text.Trim();
        if (string.IsNullOrWhiteSpace(dni))
        {
            mensaje = "Debe ingresar el DNI del cliente.";
            txtDni.Focus();
            return false;
        }

        if (!dni.All(char.IsDigit))
        {
            mensaje = "El DNI solo debe contener números.";
            txtDni.Focus();
            return false;
        }

        // Validar que tenga exactamente 8 dígitos
        if (dni.Length != 8)
        {
            mensaje = "El DNI debe tener exactamente 8 dígitos.";
            txtDni.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            mensaje = "Debe ingresar el nombre.";
            txtNombre.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtApellido.Text))
        {
            mensaje = "Debe ingresar el apellido.";
            txtApellido.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtDireccion.Text))
        {
            mensaje = "Debe ingresar la dirección.";
            txtDireccion.Focus();
            return false;
        }

        var email = txtEmail.Text.Trim();
        if (!EmailRegex.IsMatch(email))
        {
            mensaje = "Debe ingresar un correo electrónico válido.";
            txtEmail.Focus();
            return false;
        }

        if (cboEstado.SelectedItem is not string estado || string.IsNullOrWhiteSpace(estado))
        {
            mensaje = "Debe seleccionar un estado.";
            cboEstado.Focus();
            return false;
        }

        mensaje = string.Empty;
        return true;
    }

    private void LimpiarFormulario()
    {
        txtDni.Clear();
        txtNombre.Clear();
        txtApellido.Clear();
        txtDireccion.Clear();
        txtEmail.Clear();

        if (cboEstado.Items.Count > 0)
        {
            cboEstado.SelectedIndex = 0;
        }
    }

    private static void TxtDni_KeyPress(object? sender, KeyPressEventArgs e)
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

    private sealed class ClienteViewModel
    {
        public long IdCliente { get; set; }
        public string Dni { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado => Activo ? "Activo" : "Inactivo";
    }

    /// <summary>
    ///     Cuadro de diálogo utilizado para editar la información de un cliente.
    /// </summary>
    private sealed class EditarClienteDialog : Form
    {
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        private readonly TextBox _txtDni;
        private readonly TextBox _txtNombre;
        private readonly TextBox _txtApellido;
        private readonly TextBox _txtDireccion;
        private readonly TextBox _txtEmail;
        private readonly bool _estadoActual;

        public ClienteDto? ClienteActualizado { get; private set; }

        public EditarClienteDialog(ClienteViewModel clienteOriginal)
        {
            if (clienteOriginal is null)
            {
                throw new ArgumentNullException(nameof(clienteOriginal));
            }

            _estadoActual = clienteOriginal.Activo;

            Text = "Editar cliente";
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
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            for (int i = 0; i < 5; i++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            var lblDni = new Label { Text = "DNI:", Anchor = AnchorStyles.Left, AutoSize = true };
            _txtDni = new TextBox { Width = 350, MaxLength = 8, Text = clienteOriginal.Dni };
            _txtDni.KeyPress += TxtDni_KeyPress;

            var lblNombre = new Label { Text = "Nombre:", Anchor = AnchorStyles.Left, AutoSize = true };
            _txtNombre = new TextBox { Width = 350, MaxLength = 100, Text = clienteOriginal.Nombre };

            var lblApellido = new Label { Text = "Apellido:", Anchor = AnchorStyles.Left, AutoSize = true };
            _txtApellido = new TextBox { Width = 350, MaxLength = 100, Text = clienteOriginal.Apellido };

            var lblDireccion = new Label { Text = "Dirección:", Anchor = AnchorStyles.Left, AutoSize = true };
            _txtDireccion = new TextBox { Width = 350, MaxLength = 200, Text = clienteOriginal.Direccion };

            var lblEmail = new Label { Text = "Email:", Anchor = AnchorStyles.Left, AutoSize = true };
            _txtEmail = new TextBox { Width = 350, MaxLength = 150, Text = clienteOriginal.Email };

            layout.Controls.Add(lblDni, 0, 0);
            layout.Controls.Add(_txtDni, 1, 0);
            layout.Controls.Add(lblNombre, 0, 1);
            layout.Controls.Add(_txtNombre, 1, 1);
            layout.Controls.Add(lblApellido, 0, 2);
            layout.Controls.Add(_txtApellido, 1, 2);
            layout.Controls.Add(lblDireccion, 0, 3);
            layout.Controls.Add(_txtDireccion, 1, 3);
            layout.Controls.Add(lblEmail, 0, 4);
            layout.Controls.Add(_txtEmail, 1, 4);

            Controls.Add(layout);

            var btnAceptar = new Button
            {
                Text = "Aceptar",
                DialogResult = DialogResult.OK,
                AutoSize = true,
                Anchor = AnchorStyles.Right
            };
            btnAceptar.Click += BtnAceptar_Click;

            var btnCancelar = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                AutoSize = true,
                Anchor = AnchorStyles.Right
            };

            var btnTextoBoton = new Button
            {
                Text = clienteOriginal.Activo ? "Desactivar" : "Activar",
                DialogResult = DialogResult.OK,
                AutoSize = true,
                Anchor = AnchorStyles.Right
            };
            btnTextoBoton.Click += BtnTextoBoton_Click;

            var btnsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                AutoSize = true,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 12, 0, 0)
            };
            btnsPanel.Controls.Add(btnCancelar);
            btnsPanel.Controls.Add(btnAceptar);
            btnsPanel.Controls.Add(btnTextoBoton);

            Controls.Add(btnsPanel);

            AcceptButton = btnAceptar;
            CancelButton = btnCancelar;

            // Habilitar el botón de cierre (X) en la ventana
            ControlBox = true;

            // Foco inicial
            _txtDni.Focus();
        }

        private void BtnAceptar_Click(object? sender, EventArgs e)
        {
            if (!ValidarFormulario(out var mensajeValidacion))
            {
                MessageBox.Show(mensajeValidacion, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClienteActualizado = new ClienteDto
            {
                Dni = _txtDni.Text.Trim(),
                Nombre = _txtNombre.Text.Trim(),
                Apellido = _txtApellido.Text.Trim(),
                Direccion = _txtDireccion.Text.Trim(),
                Email = _txtEmail.Text.Trim(),
                Activo = _estadoActual
            };

            Close();
        }

        private void BtnTextoBoton_Click(object? sender, EventArgs e)
        {
            var activar = !_estadoActual;
            var accion = activar ? "activar" : "desactivar";
            var confirmacion = MessageBox.Show(
                $"¿Está seguro de {accion} al cliente '{_txtNombre.Text} {_txtApellido.Text}'?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            // Cambiar solo el estado
            ClienteActualizado = new ClienteDto
            {
                Dni = _txtDni.Text.Trim(),
                Nombre = _txtNombre.Text.Trim(),
                Apellido = _txtApellido.Text.Trim(),
                Direccion = _txtDireccion.Text.Trim(),
                Email = _txtEmail.Text.Trim(),
                Activo = activar
            };

            Close();
        }

        private bool ValidarFormulario(out string mensaje)
        {
            var dni = _txtDni.Text.Trim();
            if (string.IsNullOrWhiteSpace(dni))
            {
                mensaje = "Debe ingresar el DNI del cliente.";
                _txtDni.Focus();
                return false;
            }

            if (!dni.All(char.IsDigit))
            {
                mensaje = "El DNI solo debe contener números.";
                _txtDni.Focus();
                return false;
            }

            // Validar que tenga exactamente 8 dígitos
            if (dni.Length != 8)
            {
                mensaje = "El DNI debe tener exactamente 8 dígitos.";
                _txtDni.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(_txtNombre.Text))
            {
                mensaje = "Debe ingresar el nombre.";
                _txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(_txtApellido.Text))
            {
                mensaje = "Debe ingresar el apellido.";
                _txtApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(_txtDireccion.Text))
            {
                mensaje = "Debe ingresar la dirección.";
                _txtDireccion.Focus();
                return false;
            }

            var email = _txtEmail.Text.Trim();
            if (!EmailRegex.IsMatch(email))
            {
                mensaje = "Debe ingresar un correo electrónico válido.";
                _txtEmail.Focus();
                return false;
            }

            mensaje = string.Empty;
            return true;
        }

        private static void TxtDni_KeyPress(object? sender, KeyPressEventArgs e)
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
    }
}
