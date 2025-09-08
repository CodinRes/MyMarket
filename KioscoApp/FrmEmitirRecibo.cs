using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class FrmEmitirRecibo : Form
    {
        private TextBox txtCliente;
        private ComboBox cboProducto;
        private NumericUpDown nudCantidad;
        private Button btnAgregar, btnEmitir;
        private DataGridView dgvItems;
        private Label lblTotal;
        private decimal total = 0m;

        public FrmEmitirRecibo()
        {
            Text = "Emitir Recibo";
            Padding = new Padding(16);

            // Línea superior
            var panelTop = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 80, FlowDirection = FlowDirection.LeftToRight };
            Controls.Add(panelTop);

            panelTop.Controls.Add(new Label { Text = "Cliente:", Width = 60, TextAlign = ContentAlignment.MiddleLeft });
            txtCliente = new TextBox { Width = 220 };
            panelTop.Controls.Add(txtCliente);

            panelTop.Controls.Add(new Label { Text = "Producto:", Width = 70, TextAlign = ContentAlignment.MiddleLeft });
            cboProducto = new ComboBox { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cboProducto.Items.AddRange(new object[] { "Gaseosa 500ml - $800", "Chocolate - $650", "Papas fritas - $900" });
            cboProducto.SelectedIndex = 0;
            panelTop.Controls.Add(cboProducto);

            panelTop.Controls.Add(new Label { Text = "Cant.:", Width = 50, TextAlign = ContentAlignment.MiddleLeft });
            nudCantidad = new NumericUpDown { Minimum = 1, Maximum = 100, Value = 1, Width = 60 };
            panelTop.Controls.Add(nudCantidad);

            btnAgregar = new Button { Text = "Agregar", Width = 100, Height = 28 };
            btnAgregar.Click += BtnAgregar_Click;
            panelTop.Controls.Add(btnAgregar);

            // Grilla
            dgvItems = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false };
            dgvItems.Columns.Add("Producto", "Producto");
            dgvItems.Columns.Add("Cantidad", "Cantidad");
            dgvItems.Columns.Add("PrecioUnitario", "P. Unit.");
            dgvItems.Columns.Add("Subtotal", "Subtotal");
            Controls.Add(dgvItems);

            // Pie
            var panelBottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 50, FlowDirection = FlowDirection.RightToLeft };
            Controls.Add(panelBottom);

            btnEmitir = new Button { Text = "Emitir Recibo", Width = 140, Height = 30 };
            btnEmitir.Click += (s, e) =>
            {
                MessageBox.Show("Recibo emitido (prototipo).", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvItems.Rows.Clear();
                total = 0m;
                UpdateTotal();
            };
            panelBottom.Controls.Add(btnEmitir);

            lblTotal = new Label { Text = "Total: $0", AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), Padding = new Padding(0, 6, 16, 0) };
            panelBottom.Controls.Add(lblTotal);
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            var producto = cboProducto.SelectedItem.ToString();
            decimal precio = ExtraerPrecio(producto);
            int cant = (int)nudCantidad.Value;
            decimal sub = precio * cant;

            dgvItems.Rows.Add(producto, cant, precio, sub);
            total += sub;
            UpdateTotal();
        }

        private void UpdateTotal() => lblTotal.Text = $"Total: ${total:N2}";

        private decimal ExtraerPrecio(string text)
        {
            // "Gaseosa 500ml - $800" -> 800
            int idx = text.LastIndexOf('$');
            if (idx >= 0 && decimal.TryParse(text.Substring(idx + 1).Trim(), out var p)) return p;
            return 0m;
        }
    }
}
