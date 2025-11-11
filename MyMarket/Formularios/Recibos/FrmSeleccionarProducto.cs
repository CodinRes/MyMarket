using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MyMarket.Datos.Modelos;

namespace MyMarket.Formularios.Recibos;

/// <summary>
///     Diálogo mejorado para elegir un producto disponible y la cantidad a vender.
///     Incluye búsqueda por nombre, código o descripción.
/// </summary>
internal sealed class FrmSeleccionarProducto : Form
{
    private readonly List<ProductoVentaDisponible> _todosLosProductos;
    private readonly BindingList<ProductoVentaDisponible> _productosFiltrados;
    private readonly TextBox _txtBuscar;
    private readonly DataGridView _dgvProductos;
    private readonly NumericUpDown _nudCantidad;
    private readonly Label _lblPrecio;
    private readonly Label _lblStock;

    public FrmSeleccionarProducto(IEnumerable<ProductoVentaDisponible> productosDisponibles)
    {
        if (productosDisponibles is null)
        {
            throw new ArgumentNullException(nameof(productosDisponibles));
        }

        _todosLosProductos = productosDisponibles.ToList();
        if (_todosLosProductos.Count == 0)
        {
            throw new ArgumentException("Debe haber al menos un producto disponible.", nameof(productosDisponibles));
        }

        // FIX: No pasar la lista directamente al constructor de BindingList
        // porque usa la misma referencia. Crear una nueva BindingList vacía.
        _productosFiltrados = new BindingList<ProductoVentaDisponible>();

        Text = "Seleccionar producto";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.Sizable;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new System.Drawing.Size(700, 500);
        MinimumSize = new System.Drawing.Size(600, 400);

        var layout = new TableLayoutPanel
        {
            ColumnCount = 2,
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            RowCount = 6
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // Búsqueda
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F)); // Cantidad
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F)); // Precio
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F)); // Stock
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Botones

        // Fila 0: Búsqueda
        var lblBuscar = new Label
        {
            Text = "Buscar:",
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(0, 8, 8, 6)
        };
        _txtBuscar = new TextBox
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(0, 6, 0, 6),
            PlaceholderText = "Buscar por nombre, código o descripción..."
        };
        _txtBuscar.TextChanged += TxtBuscar_TextChanged;

        layout.Controls.Add(lblBuscar, 0, 0);
        layout.Controls.Add(_txtBuscar, 1, 0);

        // Fila 1: Grid de productos
        _dgvProductos = new DataGridView
        {
            Dock = DockStyle.Fill,
            Margin = new Padding(0, 6, 0, 6),
            AutoGenerateColumns = false,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            RowHeadersVisible = false
        };

        _dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ProductoVentaDisponible.Codigo),
            HeaderText = "Código",
            Width = 100
        });
        _dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ProductoVentaDisponible.Nombre),
            HeaderText = "Producto",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
        _dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ProductoVentaDisponible.Descripcion),
            HeaderText = "Descripción",
            Width = 200
        });
        _dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ProductoVentaDisponible.Precio),
            HeaderText = "Precio",
            Width = 100,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
        });
        _dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ProductoVentaDisponible.StockDisponible),
            HeaderText = "Stock",
            Width = 80
        });

        _dgvProductos.DataSource = _productosFiltrados;
        _dgvProductos.SelectionChanged += DgvProductos_SelectionChanged;
        _dgvProductos.CellDoubleClick += DgvProductos_CellDoubleClick;

        layout.Controls.Add(_dgvProductos, 0, 1);
        layout.SetColumnSpan(_dgvProductos, 2);

        // Fila 2: Cantidad
        var lblCantidad = new Label
        {
            Text = "Cantidad:",
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

        layout.Controls.Add(lblCantidad, 0, 2);
        layout.Controls.Add(_nudCantidad, 1, 2);

        // Fila 3: Precio
        var lblPrecioTitulo = new Label
        {
            Text = "Precio unitario:",
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(0, 6, 8, 6)
        };
        _lblPrecio = new Label
        {
            Text = "$ 0,00",
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(0, 6, 0, 6),
            Font = new System.Drawing.Font(System.Drawing.SystemFonts.DefaultFont.FontFamily, 10, System.Drawing.FontStyle.Bold)
        };

        layout.Controls.Add(lblPrecioTitulo, 0, 3);
        layout.Controls.Add(_lblPrecio, 1, 3);

        // Fila 4: Stock disponible
        var lblStockTitulo = new Label
        {
            Text = "Stock disponible:",
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(0, 6, 8, 6)
        };
        _lblStock = new Label
        {
            Text = "0",
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(0, 6, 0, 6)
        };

        layout.Controls.Add(lblStockTitulo, 0, 4);
        layout.Controls.Add(_lblStock, 1, 4);

        // Fila 5: Botones
        var panelBotones = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.RightToLeft,
            Dock = DockStyle.Fill,
            AutoSize = false,
            Margin = new Padding(0, 10, 0, 0)
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

        layout.Controls.Add(panelBotones, 0, 5);
        layout.SetColumnSpan(panelBotones, 2);

        Controls.Add(layout);

        AcceptButton = btnAceptar;
        CancelButton = btnCancelar;

        // Poblar la lista filtrada inicialmente con todos los productos
        foreach (var producto in _todosLosProductos)
        {
            _productosFiltrados.Add(producto);
        }

        if (_dgvProductos.Rows.Count > 0)
        {
            _dgvProductos.Rows[0].Selected = true;
            ActualizarProductoSeleccionado();
        }

        btnAceptar.Click += (_, _) =>
        {
            if (_dgvProductos.CurrentRow?.DataBoundItem is ProductoVentaDisponible seleccionado)
            {
                ProductoSeleccionado = seleccionado.Producto;
                CantidadSeleccionada = (short)_nudCantidad.Value;
            }
        };
    }

    public ProductoDto? ProductoSeleccionado { get; private set; }

    public short CantidadSeleccionada { get; private set; } = 1;

    private void TxtBuscar_TextChanged(object? sender, EventArgs e)
    {
        var textoBusqueda = _txtBuscar.Text?.Trim() ?? string.Empty;

        IEnumerable<ProductoVentaDisponible> productosFiltrados;
        if (string.IsNullOrWhiteSpace(textoBusqueda))
        {
            productosFiltrados = _todosLosProductos;
        }
        else
        {
            productosFiltrados = _todosLosProductos.Where(p =>
                p.Nombre.Contains(textoBusqueda, StringComparison.OrdinalIgnoreCase) ||
                p.Codigo.ToString().Contains(textoBusqueda, StringComparison.OrdinalIgnoreCase) ||
                (p.Descripcion?.Contains(textoBusqueda, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        // Suspend change notifications to improve performance and avoid UI flickering
        _productosFiltrados.RaiseListChangedEvents = false;
        try
        {
            _productosFiltrados.Clear();
            foreach (var producto in productosFiltrados)
            {
                _productosFiltrados.Add(producto);
            }
        }
        finally
        {
            // Resume change notifications and refresh the DataGridView
            _productosFiltrados.RaiseListChangedEvents = true;
            _productosFiltrados.ResetBindings();
        }

        if (_dgvProductos.Rows.Count > 0)
        {
            _dgvProductos.Rows[0].Selected = true;
            ActualizarProductoSeleccionado();
        }
        else
        {
            // No hay resultados, limpiar la selección
            ActualizarProductoSeleccionado();
        }
    }

    private void DgvProductos_SelectionChanged(object? sender, EventArgs e)
    {
        ActualizarProductoSeleccionado();
    }

    private void DgvProductos_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && AcceptButton is Button btnAceptar)
        {
            btnAceptar.PerformClick();
        }
    }

    private void ActualizarProductoSeleccionado()
    {
        if (_dgvProductos.CurrentRow?.DataBoundItem is not ProductoVentaDisponible seleccionado)
        {
            _nudCantidad.Maximum = 1;
            _nudCantidad.Value = 1;
            _lblPrecio.Text = "$ 0,00";
            _lblStock.Text = "0";
            return;
        }

        var maximo = Math.Max(1, (int)seleccionado.StockDisponible);
        _nudCantidad.Maximum = maximo;
        if (_nudCantidad.Value > maximo)
        {
            _nudCantidad.Value = maximo; // Ajustar al máximo disponible
        }

        _lblPrecio.Text = seleccionado.Precio.ToString("C2", CultureInfo.CurrentCulture);
        _lblStock.Text = seleccionado.StockDisponible.ToString();
    }
}
