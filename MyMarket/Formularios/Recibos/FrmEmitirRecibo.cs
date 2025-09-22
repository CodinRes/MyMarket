using System;
using System.Windows.Forms;

namespace MyMarket.Formularios.Recibos;

/// <summary>
///     Formulario prototipo para emitir recibos con datos simulados.
/// </summary>
public partial class FrmEmitirRecibo : Form
{
    public FrmEmitirRecibo()
    {
        InitializeComponent();
        ConfigurarCombos();
        ConfigurarDataGridView();
        InicializarTotales();
        AsociarEventosPlaceholder();
    }

    /// <summary>
    ///     Carga opciones básicas de clientes y métodos de pago.
    /// </summary>
    private void ConfigurarCombos()
    {
        cmbCliente.Items.Clear();
        cmbCliente.Items.AddRange(new object[]
        {
            "Seleccionar cliente...",
            "Juan Pérez",
            "María Rodríguez",
            "Comercial ACME"
        });
        cmbCliente.SelectedIndex = 0;

        cmbMetodoPago.Items.Clear();
        cmbMetodoPago.Items.AddRange(new object[]
        {
            "Seleccionar método...",
            "Efectivo",
            "Tarjeta de crédito",
            "Transferencia"
        });
        cmbMetodoPago.SelectedIndex = 0;
    }

    /// <summary>
    ///     Prepara las columnas de la grilla que representa el detalle del recibo.
    /// </summary>
    private void ConfigurarDataGridView()
    {
        dgvDetalle.Columns.Clear();
        dgvDetalle.Columns.Add("colDescripcion", "Descripción");
        dgvDetalle.Columns.Add("colCantidad", "Cantidad");
        dgvDetalle.Columns.Add("colPrecioUnitario", "Precio unitario");
        dgvDetalle.Columns.Add("colSubtotal", "Subtotal");

        dgvDetalle.Columns["colCantidad"].FillWeight = 20;
        dgvDetalle.Columns["colPrecioUnitario"].FillWeight = 25;
        dgvDetalle.Columns["colSubtotal"].FillWeight = 25;
    }

    /// <summary>
    ///     Resetea los totales mostrados al usuario.
    /// </summary>
    private void InicializarTotales()
    {
        txtSubtotal.Text = "$ 0,00";
        txtImpuestos.Text = "$ 0,00";
        txtTotal.Text = "$ 0,00";
    }

    /// <summary>
    ///     Asocia eventos temporales que explican que la lógica está pendiente.
    /// </summary>
    private void AsociarEventosPlaceholder()
    {
        btnNuevoItem.Click += (_, _) => MostrarMensajePlaceholder("Nuevo ítem");
        btnQuitarItem.Click += (_, _) => MostrarMensajePlaceholder("Quitar ítem");
        btnEmitirRecibo.Click += (_, _) => MostrarMensajePlaceholder("Emitir recibo");
    }

    private static void MostrarMensajePlaceholder(string accion)
    {
        MessageBox.Show(
            $"Acción \"{accion}\" pendiente de implementación.",
            "Emitir recibo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }
}
