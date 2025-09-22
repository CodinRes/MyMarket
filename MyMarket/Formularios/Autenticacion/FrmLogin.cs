using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;

namespace MyMarket.Formularios.Autenticacion;

/// <summary>
///     Diálogo modal responsable de autenticar a un empleado.
/// </summary>
public partial class FrmLogin : Form
{
    private readonly EmpleadoRepository _empleadoRepository;

    /// <summary>
    ///     Resultado de la autenticación cuando el inicio de sesión fue exitoso.
    /// </summary>
    public EmpleadoDto? EmpleadoAutenticado { get; private set; }

    /// <summary>
    ///     Recibe el repositorio de empleados para validar credenciales y configura eventos del formulario.
    /// </summary>
    public FrmLogin(EmpleadoRepository empleadoRepository)
    {
        _empleadoRepository = empleadoRepository ?? throw new ArgumentNullException(nameof(empleadoRepository));
        InitializeComponent();

        // Permite iniciar sesión presionando Enter y cancelar con Escape.
        AcceptButton = btnIniciarSesion;
        CancelButton = btnCancelar;

        btnIniciarSesion.Click += BtnIniciarSesion_Click;
        btnCancelar.Click += (s, e) => CloseWithResult(DialogResult.Cancel);
    }

    /// <summary>
    ///     Valida los campos ingresados, intenta autenticar y maneja los posibles errores.
    /// </summary>
    private void BtnIniciarSesion_Click(object? sender, EventArgs e)
    {
        var cuil = txtCuil.Text.Trim();
        var password = txtPassword.Text;

        if (string.IsNullOrWhiteSpace(cuil))
        {
            MessageBox.Show("Debe ingresar el CUIL del empleado.", "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtCuil.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            MessageBox.Show("Debe ingresar la contraseña.", "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtPassword.Focus();
            return;
        }

        try
        {
            var empleado = _empleadoRepository.Autenticar(cuil, password);
            if (empleado is null)
            {
                MessageBox.Show("Las credenciales ingresadas no son válidas o el empleado no está activo.",
                    "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            EmpleadoAutenticado = empleado;
            CloseWithResult(DialogResult.OK);
        }
        catch (SqlException ex)
        {
            MessageBox.Show($"No fue posible conectar con la base de datos. Detalle: {ex.Message}",
                "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(ex.Message, "Configuración inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ocurrió un error inesperado al intentar iniciar sesión. Detalle: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    ///     Cierra el diálogo estableciendo el resultado indicado.
    /// </summary>
    private void CloseWithResult(DialogResult result)
    {
        DialogResult = result;
        Close();
    }
}
