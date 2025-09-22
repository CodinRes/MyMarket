using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MyMarket.Data;
using MyMarket.Data.Models;

namespace MyMarket;

public partial class FrmGestionUsuarios : Form
{
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

    private readonly EmpleadoRepository _empleadoRepository;
    private readonly EmpleadoDto _empleadoActual;
    private readonly bool _puedeAdministrarUsuarios;

    public FrmGestionUsuarios(EmpleadoRepository empleadoRepository, EmpleadoDto empleadoActual)
    {
        _empleadoRepository = empleadoRepository ?? throw new ArgumentNullException(nameof(empleadoRepository));
        _empleadoActual = empleadoActual ?? throw new ArgumentNullException(nameof(empleadoActual));
        _puedeAdministrarUsuarios = EsGerente(_empleadoActual);

        InitializeComponent();

        dgvUsuarios.DataSource = bindingSourceUsuarios;
        dgvUsuarios.SelectionChanged += DgvUsuarios_SelectionChanged;

        btnEliminar.Visible = _puedeAdministrarUsuarios;
        btnEliminar.Enabled = false;

        txtCuil.KeyPress += TxtCuil_KeyPress;
        txtEmail.KeyPress += TxtEmail_KeyPress;

        Load += FrmGestionUsuarios_Load;
    }

    private void FrmGestionUsuarios_Load(object? sender, EventArgs e)
    {
        CargarRoles();
        CargarEmpleados();
    }

    private void BtnRefrescar_Click(object? sender, EventArgs e)
    {
        CargarEmpleados();
    }

    private void BtnAgregar_Click(object? sender, EventArgs e)
    {
        var cuil = txtCuil.Text.Trim();
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
        try
        {
            var nuevoEmpleado = new EmpleadoDto
            {
                CuilCuit = cuil,
                Email = email,
                Activo = activo,
                IdRol = rolSeleccionado.IdRol,
                RolDescripcion = rolSeleccionado.Descripcion
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

    private void CargarEmpleados()
    {
        try
        {
            var empleados = _empleadoRepository.ObtenerEmpleados();
            bindingSourceUsuarios.DataSource = empleados.ToList();
            ActualizarEstadoBoton();
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

    private void LimpiarFormulario()
    {
        txtCuil.Clear();
        txtEmail.Clear();
        txtContrasenia.Clear();
        chkActivo.Checked = true;
        if (cboRol.Items.Count > 0)
        {
            cboRol.SelectedIndex = 0;
        }
        txtCuil.Focus();
    }

    private void ActualizarEstadoBoton()
    {
        if (!_puedeAdministrarUsuarios)
        {
            btnEliminar.Enabled = false;
            return;
        }

        if (dgvUsuarios.CurrentRow?.DataBoundItem is not EmpleadoDto empleado)
        {
            btnEliminar.Enabled = false;
            btnEliminar.Text = "Cambiar estado";
            return;
        }

        btnEliminar.Text = empleado.Activo ? "Desactivar usuario" : "Activar usuario";
        btnEliminar.Enabled = PuedeGestionarEmpleado(empleado);
    }

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

        if (_empleadoActual.IdRol > 0 && empleado.IdRol > 0)
        {
            return _empleadoActual.IdRol < empleado.IdRol;
        }

        var jerarquiaActual = ObtenerPrioridadRol(_empleadoActual.RolDescripcion);
        var jerarquiaObjetivo = ObtenerPrioridadRol(empleado.RolDescripcion);

        return jerarquiaActual > jerarquiaObjetivo;
    }

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
}
