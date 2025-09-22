using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MyMarket.Formularios.Clientes;

public partial class FrmClientesSuscriptos : Form
{
    private readonly BindingList<ClienteViewModel> _clientes = new();
    private ClienteViewModel? _clienteSeleccionado;
    private bool _esEdicion;

    public FrmClientesSuscriptos()
    {
        InitializeComponent();

        dgvClientes.DataSource = bindingSourceClientes;
        bindingSourceClientes.DataSource = _clientes;
        dgvClientes.SelectionChanged += DgvClientes_SelectionChanged;

        btnEditar.Enabled = false;

        cboEstado.Items.AddRange(new object[] { "Activo", "Inactivo", "Suspendido" });
        if (cboEstado.Items.Count > 0)
        {
            cboEstado.SelectedIndex = 0;
        }
    }

    private void FrmClientesSuscriptos_Load(object? sender, EventArgs e)
    {
        CargarClientesDemo();
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
        CargarClientesDemo();
        MessageBox.Show("La lista de clientes se refrescó con datos de demostración.", "Clientes suscriptos",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void BtnGuardar_Click(object? sender, EventArgs e)
    {
        if (!ValidarFormulario(out var mensajeValidacion))
        {
            MessageBox.Show(mensajeValidacion, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var clienteForm = new ClienteViewModel
        {
            Dni = txtDni.Text.Trim(),
            Nombre = txtNombre.Text.Trim(),
            Apellido = txtApellido.Text.Trim(),
            Direccion = txtDireccion.Text.Trim(),
            Email = txtEmail.Text.Trim(),
            Estado = cboEstado.SelectedItem?.ToString() ?? string.Empty
        };

        if (_esEdicion && _clienteSeleccionado is not null)
        {
            _clienteSeleccionado.Dni = clienteForm.Dni;
            _clienteSeleccionado.Nombre = clienteForm.Nombre;
            _clienteSeleccionado.Apellido = clienteForm.Apellido;
            _clienteSeleccionado.Direccion = clienteForm.Direccion;
            _clienteSeleccionado.Email = clienteForm.Email;
            _clienteSeleccionado.Estado = clienteForm.Estado;
            dgvClientes.Refresh();
        }
        else
        {
            _clientes.Add(clienteForm);
        }

        MessageBox.Show("Los datos del cliente se almacenaron temporalmente. Integre con la base de datos en futuras versiones.",
            "Clientes suscriptos", MessageBoxButtons.OK, MessageBoxIcon.Information);

        LimpiarFormulario();
        grpFormulario.Text = "Detalle de cliente";
        _esEdicion = false;
        _clienteSeleccionado = null;
        btnEditar.Enabled = false;
    }

    private void BtnCancelar_Click(object? sender, EventArgs e)
    {
        LimpiarFormulario();
        grpFormulario.Text = "Detalle de cliente";
        _esEdicion = false;
        _clienteSeleccionado = null;
        btnEditar.Enabled = dgvClientes.CurrentRow is not null;
    }

    private void DgvClientes_SelectionChanged(object? sender, EventArgs e)
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

    private void CargarClientesDemo()
    {
        var datosDemo = ObtenerClientesDemo().ToList();

        _clientes.Clear();
        foreach (var cliente in datosDemo)
        {
            _clientes.Add(cliente);
        }

        bindingSourceClientes.ResetBindings(false);
        if (dgvClientes.Rows.Count > 0)
        {
            dgvClientes.ClearSelection();
            dgvClientes.Rows[0].Selected = true;
        }
        btnEditar.Enabled = dgvClientes.CurrentRow is not null;
        _clienteSeleccionado = dgvClientes.CurrentRow?.DataBoundItem as ClienteViewModel;
    }

    private static IEnumerable<ClienteViewModel> ObtenerClientesDemo()
    {
        yield return new ClienteViewModel
        {
            Dni = "30111222",
            Nombre = "Carla",
            Apellido = "Gómez",
            Direccion = "Av. Siempre Viva 123",
            Email = "carla.gomez@example.com",
            Estado = "Activo"
        };
        yield return new ClienteViewModel
        {
            Dni = "28333444",
            Nombre = "Martín",
            Apellido = "Pereyra",
            Direccion = "Belgrano 456",
            Email = "martin.pereyra@example.com",
            Estado = "Activo"
        };
        yield return new ClienteViewModel
        {
            Dni = "31222333",
            Nombre = "Lucía",
            Apellido = "Alonso",
            Direccion = "Mitre 890",
            Email = "lucia.alonso@example.com",
            Estado = "Inactivo"
        };
    }

    private static bool ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        const string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, patron, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
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
        if (!ValidarEmail(email))
        {
            mensaje = "Debe ingresar un correo electrónico válido.";
            txtEmail.Focus();
            return false;
        }

        if (cboEstado.SelectedItem is not string || string.IsNullOrWhiteSpace(cboEstado.SelectedItem.ToString()))
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
        public string Estado { get; set; } = string.Empty;
    }
}
