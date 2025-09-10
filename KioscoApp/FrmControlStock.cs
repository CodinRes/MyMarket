using System;
using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class FrmControlStock : Form
    {
        public FrmControlStock()
        {
            InitializeComponent();
            btnReponer.Click += (s, e) => ModificarStock(+1);
            btnDescontar.Click += (s, e) => ModificarStock(-1);
            // Datos fake
            dgv.Rows.Add("P001", "Gaseosa 500ml", 12);
            dgv.Rows.Add("P002", "Chocolate", 6);
            dgv.Rows.Add("P003", "Papas fritas", 3);
        }

        private void ModificarStock(int delta)
        {
            if (dgv.CurrentRow == null) return;
            int stock = Convert.ToInt32(dgv.CurrentRow.Cells["Stock"].Value);
            stock = Math.Max(0, stock + delta);
            dgv.CurrentRow.Cells["Stock"].Value = stock;
        }
    }
}
