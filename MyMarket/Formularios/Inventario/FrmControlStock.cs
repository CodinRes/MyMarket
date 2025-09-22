using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyMarket.Formularios.Inventario;

/// <summary>
///     Pantalla de ejemplo para modificar el stock de productos de manera manual.
/// </summary>
public partial class FrmControlStock : Form
{
    public FrmControlStock()
    {
        InitializeComponent();
        btnReponer.Click += (s, e) => ModificarStock(+1);
        btnDescontar.Click += (s, e) => ModificarStock(-1);
        // Datos ficticios utilizados para la demostración.
        dgv.Rows.Add("P001", "Gaseosa 500ml", 12);
        dgv.Rows.Add("P002", "Chocolate", 6);
        dgv.Rows.Add("P003", "Papas fritas", 3);
    }

    /// <summary>
    ///     Ajusta el stock del producto seleccionado sumando el delta indicado.
    /// </summary>
    private void ModificarStock(int delta)
    {
        if (dgv.CurrentRow == null)
        {
            return;
        }

        var stock = Convert.ToInt32(dgv.CurrentRow.Cells["Stock"].Value);
        stock = Math.Max(0, stock + delta);
        dgv.CurrentRow.Cells["Stock"].Value = stock;
    }
}
