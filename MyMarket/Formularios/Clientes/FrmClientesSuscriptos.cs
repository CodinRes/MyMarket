using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;

namespace MyMarket.Formularios.Clientes;

/// <summary>
///     Pantalla de administración de clientes suscriptos conectada a la base de datos.
/// </summary>
public partial class FrmClientesSuscriptos : Form
{
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

    private readonly BindingList<ClienteViewModel> _clientes = new();
    private readonly ClienteRepository _clienteRepository;
    private ClienteViewModel? _clienteSeleccionado;
    private bool _esEdicion;

    public FrmClientesSuscriptos(ClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));

        InitializeComponent();

        dgvClientes.DataSource = bindingSourceClientes;
        bindingSourceClientes.DataSource = _clientes;
        dgvClientes.SelectionChanged += DgvClientes_SelectionChanged;

        btnEditar.Enabled = false;

        cboEstado.Items.AddRange(new object[] { "Activo", "Inactivo" });
        if (cboEstado.Items.Count > 0)
        {
            cboEstado.SelectedIndex = 0;
        }
    }

    private void FrmClientesSuscriptos_Load(object? sender, EventArgs e)
    {
        CargarClientesDesdeBase();
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

        _esEdicion = true;
        grpFormulario.Text = "Editar cliente";
        txtDni.Text = _clienteSeleccionado.Dni;
        txtNombre.Text = _clienteSeleccionado.Nombre;
        txtApellido.Text = _clienteSeleccionado.Apellido;
        txtDireccion.Text = _clienteSeleccionado.Direccion;
        txtEmail.Text = _clienteSeleccionado.Email;
        cboEstado.SelectedItem = _clienteSeleccionado.Estado;
        txtDni.Focus();
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
            var dniOriginal = _clienteSeleccionado.Dni;

            if (!string.Equals(clienteDto.Dni, dniOriginal, StringComparison.OrdinalIgnoreCase) &&
                _clienteRepository.ExisteClientePorDni(clienteDto.Dni))
            {
                throw new InvalidOperationException("Ya existe un cliente con el DNI ingresado.");
            }

            if (_clienteRepository.ExisteClientePorEmail(clienteDto.Email, dniOriginal))
            {
                throw new InvalidOperationException("Ya existe un cliente con el correo electrónico ingresado.");
            }

            _clienteRepository.ActualizarCliente(clienteDto, dniOriginal);
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
        }
        else
        {
            _clienteSeleccionado = null;
            btnEditar.Enabled = false;
        }
    }

    private bool CargarClientesDesdeBase(string? dniSeleccionado = null)
    {
        try
        {
            var clientes = _clienteRepository.ObtenerClientes();

            _clientes.Clear();
            foreach (var cliente in clientes)
            {
                _clientes.Add(new ClienteViewModel
                {
                    Dni = cliente.Dni,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Direccion = cliente.Direccion,
                    Email = cliente.Email,
                    Activo = cliente.Activo,
                    FechaRegistro = cliente.FechaRegistro
                });
            }

            bindingSourceClientes.ResetBindings(false);
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

    private sealed class ClienteViewModel
    {
        public string Dni { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado => Activo ? "Activo" : "Inactivo";
    }
}
