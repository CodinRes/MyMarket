using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace MyMarket.Formularios.Recibos;

public partial class FrmEmitirRecibo : Form
{
    private readonly TextBox _txtCliente;
    private readonly ComboBox _cboProducto;
    private readonly NumericUpDown _nudCantidad;
    private readonly Button _btnAgregar;
    private readonly Button _btnEmitir;
    private readonly DataGridView _dgvItems;
    private readonly Label _lblTotal;
    private decimal _total;

    public FrmEmitirRecibo()
    {
        Text = "Emitir Recibo";
        Padding = new Padding(16);

        var panelTop = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 80,
            FlowDirection = FlowDirection.LeftToRight
        };
        Controls.Add(panelTop);

        panelTop.Controls.Add(new Label { Text = "Cliente:", Width = 60, TextAlign = ContentAlignment.MiddleLeft });
        _txtCliente = new TextBox { Width = 220 };
        panelTop.Controls.Add(_txtCliente);

        panelTop.Controls.Add(new Label { Text = "Producto:", Width = 70, TextAlign = ContentAlignment.MiddleLeft });
        _cboProducto = new ComboBox { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
        _cboProducto.Items.AddRange(new object[] { "Gaseosa 500ml - $800", "Chocolate - $650", "Papas fritas - $900" });
        _cboProducto.SelectedIndex = 0;
        panelTop.Controls.Add(_cboProducto);

        panelTop.Controls.Add(new Label { Text = "Cant.:", Width = 50, TextAlign = ContentAlignment.MiddleLeft });
        _nudCantidad = new NumericUpDown { Minimum = 1, Maximum = 100, Value = 1, Width = 60 };
        panelTop.Controls.Add(_nudCantidad);

        _btnAgregar = new Button { Text = "Agregar", Width = 100, Height = 28 };
        _btnAgregar.Click += BtnAgregar_Click;
        panelTop.Controls.Add(_btnAgregar);

        _dgvItems = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false };
        _dgvItems.Columns.Add("Producto", "Producto");
        _dgvItems.Columns.Add("Cantidad", "Cantidad");
        _dgvItems.Columns.Add("PrecioUnitario", "P. Unit.");
        _dgvItems.Columns.Add("Subtotal", "Subtotal");
        Controls.Add(_dgvItems);

        var panelBottom = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 50,
            FlowDirection = FlowDirection.RightToLeft
        };
        Controls.Add(panelBottom);

        _btnEmitir = new Button { Text = "Emitir Recibo", Width = 140, Height = 30 };
        _btnEmitir.Click += (s, e) =>
        {
            MessageBox.Show("Recibo emitido (prototipo).", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _dgvItems.Rows.Clear();
            _total = 0m;
            UpdateTotal();
        };
        panelBottom.Controls.Add(_btnEmitir);

        _lblTotal = new Label
        {
            Text = "Total: $0",
            AutoSize = true,
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            Padding = new Padding(0, 6, 16, 0)
        };
        panelBottom.Controls.Add(_lblTotal);
    }

    private void BtnAgregar_Click(object? sender, EventArgs e)
    {
        var producto = _cboProducto.SelectedItem?.ToString();
        if (string.IsNullOrWhiteSpace(producto))
        {
            MessageBox.Show("Debe seleccionar un producto.", "Emitir recibo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var precio = ExtraerPrecio(producto);
        var cantidad = (int)_nudCantidad.Value;
        var subtotal = precio * cantidad;

        _dgvItems.Rows.Add(producto, cantidad, precio, subtotal);
        _total += subtotal;
        UpdateTotal();
    }

    private void UpdateTotal()
    {
        _lblTotal.Text = $"Total: ${_total:N2}";
    }

    private static decimal ExtraerPrecio(string descripcion)
    {
        var index = descripcion.LastIndexOf('$');
        if (index < 0)
        {
            return 0m;
        }

        var valor = descripcion[(index + 1)..].Trim();
        return decimal.TryParse(valor, NumberStyles.Number, CultureInfo.CurrentCulture, out var precio)
            ? precio
            : 0m;
    }
}
