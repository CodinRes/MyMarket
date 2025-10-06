using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MyMarket.Datos.Modelos;

namespace MyMarket.Formularios.Recibos;

/// <summary>
///     Diálogo simple para elegir un producto disponible y la cantidad a vender.
/// </summary>
internal sealed class FrmSeleccionarProducto : Form
{
    private readonly ComboBox _cmbProductos;
    private readonly Label _lblPrecio;
    private readonly NumericUpDown _nudCantidad;
    public FrmSeleccionarProducto(IEnumerable<ProductoVentaDisponible> productosDisponibles)
    {
        if (productosDisponibles is null)
        {
            throw new ArgumentNullException(nameof(productosDisponibles));
        }

        var productos = productosDisponibles.ToList();
        if (productos.Count == 0)
        {
            throw new ArgumentException("Debe haber al menos un producto disponible.", nameof(productosDisponibles));
        }

        Text = "Seleccionar producto";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        var layout = new TableLayoutPanel
        {
            ColumnCount = 2,
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

        var lblProducto = new Label
        {
            Text = "Producto",
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(0, 6, 8, 6)
        };
        _cmbProductos = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Dock = DockStyle.Fill,
            Margin = new Padding(0, 6, 0, 6)
        };
        _cmbProductos.SelectedIndexChanged += (_, _) => ActualizarProductoSeleccionado();

        layout.Controls.Add(lblProducto, 0, 0);
        layout.Controls.Add(_cmbProductos, 1, 0);

        var lblCantidad = new Label
        {
            Text = "Cantidad",
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(0, 6, 8, 6)
        };
        _nudCantidad = new NumericUpDown
        {
            Minimum = 1,
            Maximum = 1,
            Dock = DockStyle.Left,
            Width = 120,
            Margin = new Padding(0, 6, 0, 6)
        };

        layout.Controls.Add(lblCantidad, 0, 1);
        layout.Controls.Add(_nudCantidad, 1, 1);

        var lblPrecioTitulo = new Label
        {
            Text = "Precio unitario",
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(0, 6, 8, 6)
        };
        _lblPrecio = new Label
        {
            Text = "$ 0,00",
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(0, 6, 0, 6)
        };

        layout.Controls.Add(lblPrecioTitulo, 0, 2);
        layout.Controls.Add(_lblPrecio, 1, 2);

        var panelBotones = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.RightToLeft,
            Dock = DockStyle.Fill,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Margin = new Padding(0, 12, 0, 0)
        };

        var btnAceptar = new Button
        {
            Text = "Agregar",
            DialogResult = DialogResult.OK,
            AutoSize = true,
            Padding = new Padding(12, 6, 12, 6)
        };
        var btnCancelar = new Button
        {
            Text = "Cancelar",
            DialogResult = DialogResult.Cancel,
            AutoSize = true,
            Padding = new Padding(12, 6, 12, 6)
        };

        panelBotones.Controls.Add(btnAceptar);
        panelBotones.Controls.Add(btnCancelar);

        layout.Controls.Add(panelBotones, 0, 3);
        layout.SetColumnSpan(panelBotones, 2);

        Controls.Add(layout);

        AcceptButton = btnAceptar;
        CancelButton = btnCancelar;

        _cmbProductos.DataSource = productos;
        _cmbProductos.DisplayMember = nameof(ProductoVentaDisponible.Descripcion);
        _cmbProductos.SelectedIndex = 0;
        ActualizarProductoSeleccionado();

        btnAceptar.Click += (_, _) =>
        {
            ProductoSeleccionado = (_cmbProductos.SelectedItem as ProductoVentaDisponible)?.Producto;
            CantidadSeleccionada = (short)_nudCantidad.Value;
        };
    }

    public ProductoDto? ProductoSeleccionado { get; private set; }

    public short CantidadSeleccionada { get; private set; } = 1;

    private void ActualizarProductoSeleccionado()
    {
        if (_cmbProductos.SelectedItem is not ProductoVentaDisponible seleccionado)
        {
            return;
        }

        var maximo = Math.Max(1, (int)seleccionado.StockDisponible);
        _nudCantidad.Maximum = maximo;
        if (_nudCantidad.Value > maximo)
        {
            _nudCantidad.Value = maximo;
        }

        _lblPrecio.Text = seleccionado.Producto.Precio.ToString("C2", CultureInfo.CurrentCulture);
    }
}
