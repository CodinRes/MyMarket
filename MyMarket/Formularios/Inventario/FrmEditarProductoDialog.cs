using System;
using System.Windows.Forms;
using MyMarket.Datos.Modelos;

namespace MyMarket.Formularios.Inventario;

internal sealed class FrmEditarProductoDialog : Form
{
    private TextBox _txtCodigo;
    private TextBox _txtNombre;
    private TextBox _txtDescripcion;
    private NumericUpDown _nudPrecio;
    private NumericUpDown _nudStock;
    private NumericUpDown _nudIdCategoria;
    private CheckBox _chkActivo;

    public FrmEditarProductoDialog()
    {
        InitializeComponents();
    }

    public FrmEditarProductoDialog(ProductoDto producto) : this()
    {
        _txtCodigo.Text = producto.CodigoProducto.ToString();
        _txtNombre.Text = producto.Nombre;
        _txtDescripcion.Text = producto.Descripcion ?? string.Empty;
        _nudPrecio.Value = producto.Precio;
        _nudStock.Value = producto.Stock;
        _nudIdCategoria.Value = producto.IdCategoria;
        _chkActivo.Checked = producto.Estado;
        _txtCodigo.ReadOnly = true;
    }

    private void InitializeComponents()
    {
        Text = "Editar producto";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Padding = new Padding(12);

        var layout = new TableLayoutPanel { ColumnCount = 2, Dock = DockStyle.Fill, AutoSize = true };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));

        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        layout.Controls.Add(new Label { Text = "Código:", Anchor = AnchorStyles.Left }, 0, 0);
        _txtCodigo = new TextBox { Dock = DockStyle.Fill, MaxLength = 50 };
        layout.Controls.Add(_txtCodigo, 1, 0);

        layout.Controls.Add(new Label { Text = "Nombre:", Anchor = AnchorStyles.Left }, 0, 1);
        _txtNombre = new TextBox { Dock = DockStyle.Fill, MaxLength = 200 };
        layout.Controls.Add(_txtNombre, 1, 1);

        layout.Controls.Add(new Label { Text = "Descripción:", Anchor = AnchorStyles.Left }, 0, 2);
        _txtDescripcion = new TextBox { Dock = DockStyle.Fill, MaxLength = 500 };
        layout.Controls.Add(_txtDescripcion, 1, 2);

        layout.Controls.Add(new Label { Text = "Precio:", Anchor = AnchorStyles.Left }, 0, 3);
        _nudPrecio = new NumericUpDown { Dock = DockStyle.Left, DecimalPlaces = 2, Maximum = 1000000, Minimum = 0 };
        layout.Controls.Add(_nudPrecio, 1, 3);

        layout.Controls.Add(new Label { Text = "Stock:", Anchor = AnchorStyles.Left }, 0, 4);
        _nudStock = new NumericUpDown { Dock = DockStyle.Left, Maximum = 1000000, Minimum = 0 };
        layout.Controls.Add(_nudStock, 1, 4);

        layout.Controls.Add(new Label { Text = "Id categoría:", Anchor = AnchorStyles.Left }, 0, 5);
        _nudIdCategoria = new NumericUpDown { Dock = DockStyle.Left, Maximum = 1000000, Minimum = 0 };
        layout.Controls.Add(_nudIdCategoria, 1, 5);

        _chkActivo = new CheckBox { Text = "Activo", Anchor = AnchorStyles.Left, Checked = true };
        layout.Controls.Add(_chkActivo, 1, 6);

        var panelButtons = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill, AutoSize = true, Padding = new Padding(0, 8, 0, 0) };
        var btnCancel = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, AutoSize = true };
        var btnOk = new Button { Text = "Aceptar", DialogResult = DialogResult.OK, AutoSize = true };
        btnOk.Click += (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(_txtCodigo.Text)) { MessageBox.Show("Código requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); _txtCodigo.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_txtNombre.Text)) { MessageBox.Show("Nombre requerido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); _txtNombre.Focus(); return; }
            DialogResult = DialogResult.OK;
            Close();
        };
        panelButtons.Controls.Add(btnCancel);
        panelButtons.Controls.Add(btnOk);

        layout.Controls.Add(panelButtons, 0, 7);
        layout.SetColumnSpan(panelButtons, 2);

        Controls.Add(layout);

        AcceptButton = btnOk;
        CancelButton = btnCancel;
    }

    public ProductoDto ToProductoDto()
    {
        return new ProductoDto
        {
            CodigoProducto = long.TryParse(_txtCodigo.Text, out var c) ? c : 0,
            Nombre = _txtNombre.Text.Trim(),
            Descripcion = string.IsNullOrWhiteSpace(_txtDescripcion.Text) ? null : _txtDescripcion.Text.Trim(),
            Precio = _nudPrecio.Value,
            Stock = (short)_nudStock.Value,
            IdCategoria = (int)_nudIdCategoria.Value,
            Estado = _chkActivo.Checked
        };
    }
}
