using System;
using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class FrmControlStock : Form
    {
        private DataGridView dgv;
        private Button btnReponer, btnDescontar;

        public FrmControlStock()
        {
            Text = "Control de Stock";
            Padding = new Padding(16);

            dgv = new DataGridView { Dock = DockStyle.Fill, AllowUserToAddRows = false };
            dgv.Columns.Add("Codigo", "Código");
            dgv.Columns.Add("Producto", "Producto");
            dgv.Columns.Add("Stock", "Stock");
            Controls.Add(dgv);

            var panelBottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 48, FlowDirection = FlowDirection.RightToLeft };
            btnReponer = new Button { Text = "Reponer +1", Width = 110, Height = 28 };
            btnDescontar = new Button { Text = "Descontar -1", Width = 110, Height = 28 };

            btnReponer.Click += (s, e) => ModificarStock(+1);
            btnDescontar.Click += (s, e) => ModificarStock(-1);

            panelBottom.Controls.Add(btnReponer);
            panelBottom.Controls.Add(btnDescontar);
            Controls.Add(panelBottom);

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
