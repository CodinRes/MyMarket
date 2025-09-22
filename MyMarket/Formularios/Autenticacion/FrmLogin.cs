using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;

namespace MyMarket.Formularios.Autenticacion;

public partial class FrmLogin : Form
{
        private readonly EmpleadoRepository _empleadoRepository;

        public EmpleadoDto? EmpleadoAutenticado { get; private set; }

        public FrmLogin(EmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository ?? throw new ArgumentNullException(nameof(empleadoRepository));
            InitializeComponent();

            AcceptButton = btnIniciarSesion;
            CancelButton = btnCancelar;

            btnIniciarSesion.Click += BtnIniciarSesion_Click;
            btnCancelar.Click += (s, e) => CloseWithResult(DialogResult.Cancel);
        }

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

        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }
    }
