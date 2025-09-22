using System;
using System.Linq;
using System.Windows.Forms;
using MyMarket.Data;
using MyMarket.Data.Models;

namespace MyMarket;

public partial class FrmGestionUsuarios : Form
{
    private readonly EmpleadoRepository _empleadoRepository;
    private readonly bool _puedeEliminarUsuarios;

    public FrmGestionUsuarios(EmpleadoRepository empleadoRepository, bool puedeEliminarUsuarios)
    {
        _empleadoRepository = empleadoRepository ?? throw new ArgumentNullException(nameof(empleadoRepository));
        _puedeEliminarUsuarios = puedeEliminarUsuarios;

        InitializeComponent();

        dgvUsuarios.DataSource = bindingSourceUsuarios;
        btnEliminar.Visible = _puedeEliminarUsuarios;
        btnEliminar.Enabled = _puedeEliminarUsuarios;

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

        if (string.IsNullOrWhiteSpace(email))
        {
            MessageBox.Show("Debe ingresar un correo electrónico.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        if (!_puedeEliminarUsuarios)
        {
            MessageBox.Show("No tiene permisos para quitar usuarios.", "Acceso denegado",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (dgvUsuarios.CurrentRow?.DataBoundItem is not EmpleadoDto empleado)
        {
            MessageBox.Show("Debe seleccionar un usuario.", "Gestión de usuarios",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (MessageBox.Show($"¿Confirma que desea desactivar al usuario {empleado.NombreParaMostrar}?",
                "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        try
        {
            _empleadoRepository.DesactivarEmpleado(empleado.IdEmpleado);
            CargarEmpleados();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible desactivar el usuario. Detalle: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CargarEmpleados()
    {
        try
        {
            var empleados = _empleadoRepository.ObtenerEmpleados();
            bindingSourceUsuarios.DataSource = empleados.ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible obtener la lista de usuarios. Detalle: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
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
}
