using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyMarket.Formularios.Recibos;

/// <summary>
///     Pantalla prototipo que lista recibos emitidos recientemente.
/// </summary>
public partial class FrmRecibosEmitidos : Form
{
    public FrmRecibosEmitidos()
    {
        InitializeComponent();
        btnVerDetalle.Click += BtnVerDetalle_Click;
        Load += FrmRecibosEmitidos_Load;
    }

    private void FrmRecibosEmitidos_Load(object? sender, EventArgs e)
    {
        dgv.Columns.Clear();
        dgv.Columns.Add("Nro", "Nro");
        dgv.Columns.Add("Fecha", "Fecha");
        dgv.Columns.Add("Cliente", "Cliente");
        dgv.Columns.Add("Total", "Total");
        dgv.Rows.Add("0001-00001234", DateTime.Today.ToShortDateString(), "Juan López", 2500);
        dgv.Rows.Add("0001-00001235", DateTime.Today.ToShortDateString(), "Ana Ruiz", 1800);
        dgv.Rows.Add("0001-00001236", DateTime.Today.ToShortDateString(), "Carlos Pérez", 4200);
    }

    /// <summary>
    ///     Simula la visualización del detalle de un recibo.
    /// </summary>
    private void BtnVerDetalle_Click(object? sender, EventArgs e)
    {
        if (dgv.CurrentRow is null)
        {
            MessageBox.Show("Seleccione un recibo para ver el detalle.", "Detalle de recibos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var numero = dgv.CurrentRow.Cells["Nro"].Value?.ToString() ?? "(desconocido)";
        MessageBox.Show($"Detalle del recibo {numero} (prototipo).", "Detalle de recibos",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
