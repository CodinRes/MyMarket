using System;
using System.Windows.Forms;

namespace MyMarket.Formularios.Recibos;

/// <summary>
///     Diálogo para ingresar los datos de un nuevo método de pago.
/// </summary>
public partial class FrmNuevoMetodoPago : Form
{
    public FrmNuevoMetodoPago()
    {
        InitializeComponent();

        btnAceptar.Click += BtnAceptar_Click;
        btnCancelar.Click += (_, _) => DialogResult = DialogResult.Cancel;

        // Limitar entrada solo a números
        txtIdentificacion.KeyPress += TxtIdentificacion_KeyPress;
    }

    public long IdentificacionPago { get; private set; }
    public string ProveedorPago { get; private set; } = string.Empty;
    public decimal ComisionProveedor { get; private set; }

    private void BtnAceptar_Click(object? sender, EventArgs e)
    {
        var identificacionText = txtIdentificacion.Text.Trim();

        // Validar que tenga exactamente 18 dígitos
        if (identificacionText.Length != 18)
        {
            MessageBox.Show("El número de identificación debe tener exactamente 18 dígitos.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtIdentificacion.Focus();
            return;
        }

        if (!long.TryParse(identificacionText, out var identificacion))
        {
            MessageBox.Show("Ingrese un número de identificación válido.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtIdentificacion.Focus();
            return;
        }

        var proveedor = txtProveedor.Text.Trim();
        if (string.IsNullOrWhiteSpace(proveedor))
        {
            MessageBox.Show("Ingrese el nombre del proveedor.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtProveedor.Focus();
            return;
        }

        if (proveedor.Length > 100)
        {
            MessageBox.Show("El nombre del proveedor no puede superar los 100 caracteres.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtProveedor.Focus();
            return;
        }

        var comision = nudComision.Value;
        if (comision < 0 || comision > 100)
        {
            MessageBox.Show("La comisión debe estar entre 0 y 100%.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            nudComision.Focus();
            return;
        }

        IdentificacionPago = identificacion;
        ProveedorPago = proveedor;
        ComisionProveedor = comision;

        DialogResult = DialogResult.OK;
    }

    private static void TxtIdentificacion_KeyPress(object? sender, KeyPressEventArgs e)
    {
        // Solo permitir dígitos y teclas de control (backspace, delete, etc.)
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
