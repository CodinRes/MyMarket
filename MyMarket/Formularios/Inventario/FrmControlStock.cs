using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;
using MyMarket.Datos.Infraestructura;

namespace MyMarket.Formularios.Inventario;

/// <summary>
///     Pantalla de ejemplo para administrar productos y mostrar Precio, Estado y Categoría (nombre).
/// </summary>
public partial class FrmControlStock : Form
{
    private readonly ProductoRepository _productoRepository;
    private readonly CategoriaRepository _categoriaRepository;
    private readonly BindingList<ProductoViewModel> _productos = new();

    private bool _esEdicion;
    private const short MaxStockAllowed = 30000; // límite configurable (max short 32767)

    // Parameterless constructor for existing callers
    public FrmControlStock()
        : this(new ProductoRepository(new SqlConnectionFactory()), new CategoriaRepository(new SqlConnectionFactory()))
    {
    }

    public FrmControlStock(ProductoRepository productoRepository, CategoriaRepository categoriaRepository)
    {
        _productoRepository = productoRepository ?? throw new ArgumentNullException(nameof(productoRepository));
        _categoriaRepository = categoriaRepository ?? throw new ArgumentNullException(nameof(categoriaRepository));

        InitializeComponent();

        // Ensure stock controls exist (in case designer wasn't updated correctly)
        try
        {
            // If flowStock or txtStock are null, create them and insert into the layout at column 1, row 3
            if (tableLayoutFormulario != null)
            {
                if (flowStock == null || txtStock == null || chkAgregarStock == null || nudAgregarStock == null)
                {
                    flowStock = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        Dock = DockStyle.Fill,
                        FlowDirection = FlowDirection.LeftToRight,
                        WrapContents = false
                    };

                    // create or replace txtStock
                    txtStock = new TextBox
                    {
                        Anchor = AnchorStyles.Left | AnchorStyles.Right,
                        MaxLength = 10,
                        Name = "txtStock",
                        Width = 120
                    };

                    chkAgregarStock = new CheckBox
                    {
                        Anchor = AnchorStyles.Left,
                        AutoSize = true,
                        Name = "chkAgregarStock",
                        Text = "Agregar cantidad"
                    };

                    nudAgregarStock = new NumericUpDown
                    {
                        Name = "nudAgregarStock",
                        Minimum = 0,
                        Maximum = MaxStockAllowed,
                        Value = 0,
                        Width = 80,
                        Enabled = false
                    };

                    // add controls to flow
                    flowStock.Controls.Add(txtStock);
                    flowStock.Controls.Add(chkAgregarStock);
                    flowStock.Controls.Add(nudAgregarStock);

                    // remove any existing control at that position
                    var existing = tableLayoutFormulario.GetControlFromPosition(1, 3);
                    if (existing != null)
                    {
                        tableLayoutFormulario.Controls.Remove(existing);
                    }

                    tableLayoutFormulario.Controls.Add(flowStock, 1, 3);

                    // wire events
                    chkAgregarStock.CheckedChanged += (_, _) => nudAgregarStock.Enabled = chkAgregarStock.Checked;
                    txtStock.KeyPress += TxtEntero_KeyPress;
                    nudAgregarStock.Enabled = false;
                }
            }
            else
            {
                // tableLayoutFormulario missing — add controls into grpFormulario as fallback
                if (flowStock == null || txtStock == null || chkAgregarStock == null || nudAgregarStock == null)
                {
                    flowStock = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        Dock = DockStyle.Top,
                        FlowDirection = FlowDirection.LeftToRight,
                        WrapContents = false,
                        Padding = new Padding(6)
                    };

                    txtStock = new TextBox
                    {
                        Anchor = AnchorStyles.Left | AnchorStyles.Right,
                        MaxLength = 10,
                        Name = "txtStock",
                        Width = 120
                    };

                    chkAgregarStock = new CheckBox
                    {
                        Anchor = AnchorStyles.Left,
                        AutoSize = true,
                        Name = "chkAgregarStock",
                        Text = "Agregar cantidad"
                    };

                    nudAgregarStock = new NumericUpDown
                    {
                        Name = "nudAgregarStock",
                        Minimum = 0,
                        Maximum = MaxStockAllowed,
                        Value = 0,
                        Width = 80,
                        Enabled = false
                    };

                    flowStock.Controls.Add(txtStock);
                    flowStock.Controls.Add(chkAgregarStock);
                    flowStock.Controls.Add(nudAgregarStock);

                    if (grpFormulario != null)
                    {
                        grpFormulario.Controls.Add(flowStock);
                    }

                    chkAgregarStock.CheckedChanged += (_, _) => nudAgregarStock.Enabled = chkAgregarStock.Checked;
                    txtStock.KeyPress += TxtEntero_KeyPress;
                }
            }
        }
        catch
        {
            // ignore; designer probably correct in that case
        }

        // Bind grid to binding source and internal list
        dgv.DataSource = bindingSourceProductos;
        bindingSourceProductos.DataSource = _productos;

        // Configure grid columns explicitly to ensure correct headers and bindings
        ConfigurarColumnas();

        // Load categories into combo
        CargarCategorias();

        // Ensure estado combo has items
        cboEstado.Items.Clear();
        cboEstado.Items.AddRange(new object[] { "Activo", "Inactivo" });
        if (cboEstado.Items.Count > 0)
        {
            cboEstado.SelectedIndex = 0;
        }

        // Wire input validation for numeric fields
        txtCodigo.KeyPress += TxtEntero_KeyPress;
        if (txtStock != null)
        {
            txtStock.KeyPress += TxtEntero_KeyPress;
        }
        txtPrecio.KeyPress += TxtDecimal_KeyPress;

        // New stock controls
        if (chkAgregarStock != null && nudAgregarStock != null)
        {
            // detach possible previous handler and attach
            chkAgregarStock.CheckedChanged -= ChkAgregarStockOnCheckedChanged;
            chkAgregarStock.CheckedChanged += ChkAgregarStockOnCheckedChanged;
            nudAgregarStock.Enabled = false;

            // hide the "Agregar cantidad" controls initially (only visible in edit mode)
            chkAgregarStock.Visible = false;
            nudAgregarStock.Visible = false;
        }

        // Search handlers
        btnBuscar.Click += (_, _) => FiltrarProductos();
        txtBuscar.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) FiltrarProductos(); };

        // Initial state
        btnEditar.Enabled = false;
        _esEdicion = false;

        // Event handlers
        dgv.SelectionChanged += (_, _) => EstablecerEditarDisponibilidad();
        btnNuevo.Click += (_, _) => LimpiarFormulario();
        btnEditar.Click += (_, _) => CargarSeleccionEnFormulario();
        btnGuardar.Click += (_, _) => GuardarCambios();
        btnCancelar.Click += (_, _) => LimpiarFormulario();
        btnActualizar.Click += (_, _) => CargarDatos();

        CargarDatos();
    }

    private void ChkAgregarStockOnCheckedChanged(object? sender, EventArgs e)
    {
        if (nudAgregarStock != null && chkAgregarStock != null)
        {
            nudAgregarStock.Enabled = chkAgregarStock.Checked;
        }
    }

    private void ConfigurarColumnas()
    {
        // Ensure we control columns and bindings
        dgv.AutoGenerateColumns = false;
        dgv.Columns.Clear();

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colCodigo",
            HeaderText = "Código",
            DataPropertyName = nameof(ProductoViewModel.Codigo),
            MinimumWidth = 80,
            ReadOnly = true,
            FillWeight = 20
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colNombre",
            HeaderText = "Producto",
            DataPropertyName = nameof(ProductoViewModel.Nombre),
            MinimumWidth = 150,
            ReadOnly = true,
            FillWeight = 40
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colStock",
            HeaderText = "Stock",
            DataPropertyName = nameof(ProductoViewModel.Stock),
            AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
            ReadOnly = true
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colPrecio",
            HeaderText = "Precio",
            DataPropertyName = nameof(ProductoViewModel.Precio),
            DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", FormatProvider = CultureInfo.CurrentCulture },
            ReadOnly = true,
            MinimumWidth = 100,
            FillWeight = 20
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colCategoria",
            HeaderText = "Categoría",
            DataPropertyName = nameof(ProductoViewModel.CategoriaNombre),
            ReadOnly = true,
            MinimumWidth = 120,
            FillWeight = 30
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colEstado",
            HeaderText = "Estado",
            DataPropertyName = nameof(ProductoViewModel.EstadoTexto),
            ReadOnly = true,
            MinimumWidth = 80,
            FillWeight = 15
        });
    }

    private void CargarCategorias()
    {
        try
        {
            var categorias = _categoria_repository_obtener();
            var list = categorias.Select(kv => new { Id = kv.Key, Nombre = kv.Value }).ToList();
            cboCategoria.DataSource = list;
            cboCategoria.DisplayMember = "Nombre";
            cboCategoria.ValueMember = "Id";
            if (list.Count > 0) cboCategoria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible cargar las categorías. Detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void TxtEntero_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (char.IsControl(e.KeyChar))
        {
            return;
        }

        if (!char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private static void TxtDecimal_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (char.IsControl(e.KeyChar))
        {
            return;
        }

        var nf = CultureInfo.CurrentCulture.NumberFormat;
        var sep = nf.NumberDecimalSeparator[0];

        if (e.KeyChar == sep)
        {
            // allow only one decimal separator
            if (sender is TextBox tb && tb.Text.Contains(nf.NumberDecimalSeparator))
            {
                e.Handled = true;
            }
            return;
        }

        if (!char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private void EstablecerEditarDisponibilidad()
    {
        btnEditar.Enabled = dgv.CurrentRow?.DataBoundItem is ProductoViewModel;
    }

    private void CargarDatos()
    {
        try
        {
            var productos = _productoRepository.GetProductosActivos();
            var categorias = _categoria_repository_obtener();

            _productos.Clear();
            foreach (var p in productos)
            {
                _productos.Add(new ProductoViewModel
                {
                    Codigo = p.CodigoProducto,
                    Nombre = p.Nombre,
                    Stock = p.Stock,
                    Precio = p.Precio,
                    Descripcion = p.Descripcion ?? string.Empty,
                    IdCategoria = p.IdCategoria,
                    CategoriaNombre = categorias.TryGetValue(p.IdCategoria, out var nom) ? nom : "(Sin categoría)",
                    Estado = p.Estado
                });
            }

            bindingSourceProductos.ResetBindings(false);

            // Select first row if any
            if (dgv.Rows.Count > 0)
            {
                dgv.ClearSelection();
                dgv.Rows[0].Selected = true;
                dgv.CurrentCell = dgv.Rows[0].Cells[0];
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible cargar los productos. Detalle: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // Helper to call repository with clearer name (avoids long var name in constructor)
    private IReadOnlyDictionary<int, string> _categoria_repository_obtener()
    {
        return _categoriaRepository.ObtenerCategorias();
    }

    private void LimpiarFormulario()
    {
        txtCodigo.Clear();
        txtProducto.Clear();
        txtStock.Clear();
        txtPrecio.Clear();
        txtDescripcion.Clear();

        cboEstado.Items.Clear();
        cboEstado.Items.AddRange(new object[] { "Activo", "Inactivo" });
        cboEstado.SelectedIndex = 0;

        if (cboCategoria.DataSource is System.Collections.IList list && list.Count > 0)
        {
            cboCategoria.SelectedIndex = 0;
        }

        txtCodigo.ReadOnly = false;
        _esEdicion = false;
        btnEditar.Enabled = false;

        // Hide and reset add-stock controls when creating a new product
        if (chkAgregarStock != null)
        {
            chkAgregarStock.Checked = false;
            chkAgregarStock.Visible = false;
        }

        if (nudAgregarStock != null)
        {
            nudAgregarStock.Value = 0;
            nudAgregarStock.Enabled = false;
            nudAgregarStock.Visible = false;
        }
    }

    private void CargarSeleccionEnFormulario()
    {
        if (dgv.CurrentRow?.DataBoundItem is not ProductoViewModel p)
        {
            return;
        }

        txtCodigo.Text = p.Codigo.ToString(CultureInfo.InvariantCulture);
        txtProducto.Text = p.Nombre;
        txtStock.Text = p.Stock.ToString(CultureInfo.InvariantCulture);
        txtPrecio.Text = p.Precio.ToString(CultureInfo.CurrentCulture);
        txtDescripcion.Text = p.Descripcion ?? string.Empty;

        // set category selection
        if (cboCategoria.DataSource is System.Collections.IList)
        {
            cboCategoria.SelectedValue = p.IdCategoria;
        }

        // ensure items exist before selecting
        if (cboEstado.Items.Count == 0)
        {
            cboEstado.Items.AddRange(new object[] { "Activo", "Inactivo" });
        }

        cboEstado.SelectedItem = p.Estado ? "Activo" : "Inactivo";

        // editing mode
        txtCodigo.ReadOnly = true;
        _esEdicion = true;

        // Show add-stock controls when editing
        if (chkAgregarStock != null && nudAgregarStock != null)
        {
            chkAgregarStock.Visible = true;
            nudAgregarStock.Visible = true;
            // ensure numeric is disabled until user checks
            nudAgregarStock.Enabled = chkAgregarStock.Checked;
        }
    }

    private void GuardarCambios()
    {
        // Basic validation for numeric fields
        if (string.IsNullOrWhiteSpace(txtCodigo.Text) || !long.TryParse(txtCodigo.Text, out var codigo) || codigo <= 0)
        {
            MessageBox.Show("El código debe ser un número entero positivo y obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtCodigo.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(txtProducto.Text))
        {
            MessageBox.Show("El nombre del producto es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtProducto.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(txtStock.Text) || !short.TryParse(txtStock.Text, out var stock) || stock < 0)
        {
            MessageBox.Show("El stock debe ser un número entero no negativo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtStock.Focus();
            return;
        }

        // if adding stock, get add value
        short agregar = 0;
        if (chkAgregarStock.Checked)
        {
            agregar = (short)nudAgregarStock.Value;
            if (agregar < 0)
            {
                MessageBox.Show("La cantidad a agregar debe ser un número no negativo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudAgregarStock.Focus();
                return;
            }

            if (agregar == 0)
            {
                // nothing to add
                agregar = 0;
            }
        }

        var priceText = txtPrecio.Text.Trim();
        if (string.IsNullOrWhiteSpace(priceText) || !decimal.TryParse(priceText, NumberStyles.Number, CultureInfo.CurrentCulture, out var price) || price < 0)
        {
            MessageBox.Show("El precio debe ser un número positivo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtPrecio.Focus();
            return;
        }

        if (!(cboCategoria.SelectedValue is int idCategoria))
        {
            MessageBox.Show("Debe seleccionar una categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cboCategoria.Focus();
            return;
        }

        var estado = string.Equals(cboEstado.SelectedItem?.ToString(), "Activo", StringComparison.OrdinalIgnoreCase);

        var productoDto = new ProductoDto
        {
           CodigoProducto = codigo,
            Nombre = txtProducto.Text.Trim(),
            Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text) ? null : txtDescripcion.Text.Trim(),
            Precio = price,
            Stock = stock,
            IdCategoria = idCategoria,
            Estado = estado
        };

        try
        {
            if (_esEdicion)
            {
                if (chkAgregarStock.Checked && agregar > 0)
                {
                    // apply increment in DB atomically with a max cap
                    var nuevaCantidad = _productoRepository.IncrementarStock(codigo, agregar, MaxStockAllowed);
                    productoDto.Stock = nuevaCantidad;
                }

                _productoRepository.ActualizarProducto(productoDto);
                MessageBox.Show("Producto actualizado correctamente.", "Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (chkAgregarStock.Checked && agregar > 0)
                {
                    // when creating, just add to initial stock but cap
                    var initial = Math.Min(MaxStockAllowed, (int)stock + agregar);
                    productoDto.Stock = (short)initial;
                }

                _productoRepository.CrearProducto(productoDto);
                MessageBox.Show("Producto creado correctamente.", "Inventario", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CargarDatos();
            SeleccionarFilaPorCodigo(codigo);
            LimpiarFormulario();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible guardar el producto. Detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void FiltrarProductos()
    {
        var texto = txtBuscar.Text.Trim();
        if (string.IsNullOrWhiteSpace(texto))
        {
            CargarDatos();
            return;
        }

        // si es solo dígitos, buscar por código exacto primero
        if (texto.All(char.IsDigit) && long.TryParse(texto, out var codigo))
        {
            // intentar obtener producto exacto
            var productos = _productoRepository.GetProductosActivos();
            var encontrados = productos.Where(p => p.CodigoProducto == codigo).ToList();
            if (encontrados.Count == 1)
            {
                MostrarProductos(encontrados);
                SeleccionarFilaPorCodigo(codigo);
                return;
            }

            // si no hay exacto, continuar con búsqueda por nombre
        }

        // búsqueda por nombre que contenga el texto (case-insensitive)
        var productosPorNombre = _productoRepository.GetProductosActivos()
            .Where(p => p.Nombre.IndexOf(texto, StringComparison.CurrentCultureIgnoreCase) >= 0)
            .ToList();

        MostrarProductos(productosPorNombre);
    }

    private void MostrarProductos(IReadOnlyList<ProductoDto> productos)
    {
        var categorias = _categoria_repository_obtener();

        _productos.Clear();
        foreach (var p in productos)
        {
            _productos.Add(new ProductoViewModel
            {
                Codigo = p.CodigoProducto,
                Nombre = p.Nombre,
                Stock = p.Stock,
                Precio = p.Precio,
                Descripcion = p.Descripcion ?? string.Empty,
                IdCategoria = p.IdCategoria,
                CategoriaNombre = categorias.TryGetValue(p.IdCategoria, out var nom) ? nom : "(Sin categoría)",
                Estado = p.Estado
            });
        }

        bindingSourceProductos.ResetBindings(false);

        if (dgv.Rows.Count > 0)
        {
            dgv.ClearSelection();
            dgv.Rows[0].Selected = true;
            dgv.CurrentCell = dgv.Rows[0].Cells[0];
        }
    }

    private void SeleccionarFilaPorCodigo(long codigoProducto)
    {
        foreach (DataGridViewRow row in dgv.Rows)
        {
            if (row.DataBoundItem is not ProductoViewModel p)
                continue;

            if (p.Codigo == codigoProducto)
            {
                row.Selected = true;
                if (row.Cells.Count > 0)
                {
                    dgv.CurrentCell = row.Cells[0];
                }
                break;
            }
        }
    }

    private sealed class ProductoViewModel
    {
        public long Codigo { get; init; }

        public string Nombre { get; init; } = string.Empty;

        public short Stock { get; init; }

        public decimal Precio { get; init; }

        public string Descripcion { get; init; } = string.Empty;

        public int IdCategoria { get; init; }

        public string CategoriaNombre { get; init; } = string.Empty;

        public bool Estado { get; init; }

        public string EstadoTexto => Estado ? "Activo" : "Inactivo";
    }
}
