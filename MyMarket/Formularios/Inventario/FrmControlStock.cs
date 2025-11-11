using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MyMarket.Datos.Infraestructura;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;
using MyMarket.Servicios;

namespace MyMarket.Formularios.Inventario;

/// <summary>
///     Pantalla para modificar el stock de productos de manera manual y gestionar categorías.
/// </summary>
public partial class FrmControlStock : Form
{
    private readonly ProductoRepository _productoRepository;
    private readonly CategoriaRepository _categoriaRepository;
    private readonly SortableBindingList<ProductoConCategoriaDto> _todosProductos = new();
    private readonly SortableBindingList<ProductoConCategoriaDto> _productosFiltrados = new();
    private readonly BindingSource _bindingSourceProductos;
    private readonly SortableBindingList<CategoriaDto> _todasCategorias = new();
    private readonly SortableBindingList<CategoriaDto> _categoriasFiltradas = new();
    private readonly BindingSource _bindingSourceCategorias;

    public FrmControlStock()
    {
        var connectionFactory = new SqlConnectionFactory();
        _productoRepository = new ProductoRepository(connectionFactory);
        _categoriaRepository = new CategoriaRepository(connectionFactory);
        _bindingSourceProductos = new BindingSource();
        _bindingSourceCategorias = new BindingSource();

        InitializeComponent();

        // Configurar DataGridView de productos
        dgvProductos.DataSource = _bindingSourceProductos;
        _bindingSourceProductos.DataSource = _productosFiltrados;
        dgvProductos.AutoGenerateColumns = false;
        dgvProductos.Columns.Clear();
        dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ProductoConCategoriaDto.CodigoProducto),
            HeaderText = "Código",
            Name = "Codigo",
            Width = 100
        });
        dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ProductoConCategoriaDto.Nombre),
            HeaderText = "Producto",
            Name = "Nombre",
            Width = 200
        });
        dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ProductoConCategoriaDto.Descripcion),
            HeaderText = "Descripción",
            Name = "Descripcion",
            Width = 200
        });
        dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ProductoConCategoriaDto.Precio),
            HeaderText = "Precio",
            Name = "Precio",
            Width = 100,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
        });
        dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ProductoConCategoriaDto.Stock),
            HeaderText = "Stock",
            Name = "Stock",
            Width = 80
        });
        dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(ProductoConCategoriaDto.NombreCategoria),
            HeaderText = "Categoría",
            Name = "NombreCategoria",
            Width = 150
        });
        dgvProductos.Columns.Add(new DataGridViewCheckBoxColumn
        {
            DataPropertyName = nameof(ProductoConCategoriaDto.Activo),
            HeaderText = "Activo",
            Name = "Activo",
            Width = 60,
            SortMode = DataGridViewColumnSortMode.Automatic
        });

        // Habilitar ordenamiento
        DataGridViewHelper.HabilitarOrdenamientoPorColumna(dgvProductos);

        // Configurar DataGridView de categorías
        dgvCategorias.DataSource = _bindingSourceCategorias;
        _bindingSourceCategorias.DataSource = _categoriasFiltradas;
        dgvCategorias.AutoGenerateColumns = false;
        dgvCategorias.Columns.Clear();
        dgvCategorias.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(CategoriaDto.IdCategoria),
            HeaderText = "ID",
            Name = "IdCategoria",
            Width = 80
        });
        dgvCategorias.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(CategoriaDto.NombreCategoria),
            HeaderText = "Nombre de Categoría",
            Name = "NombreCategoria",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });
        dgvCategorias.Columns.Add(new DataGridViewCheckBoxColumn
        {
            DataPropertyName = nameof(CategoriaDto.Activo),
            HeaderText = "Activo",
            Name = "Activo",
            Width = 60,
            SortMode = DataGridViewColumnSortMode.Automatic
        });

        // Habilitar ordenamiento
        DataGridViewHelper.HabilitarOrdenamientoPorColumna(dgvCategorias);

        // Eventos de botones
        btnAgregarProducto.Click += BtnAgregarProducto_Click;
        btnEditarProducto.Click += BtnEditarProducto_Click;
        btnActualizarStock.Click += BtnActualizarStock_Click;
        btnAgregarCategoria.Click += BtnAgregarCategoria_Click;
        // Nuevos eventos
        btnToggleEstadoProducto.Click += BtnToggleEstadoProducto_Click;
        btnToggleEstadoCategoria.Click += BtnToggleEstadoCategoria_Click;

        Load += FrmControlStock_Load;
    }

    private void FrmControlStock_Load(object? sender, EventArgs e)
    {
        ConfigurarEventosBusqueda();
        CargarProductos();
        CargarCategorias();
    }

    private void ConfigurarEventosBusqueda()
    {
        // Configurar eventos para los controles de búsqueda de productos
        txtBuscarProducto.TextChanged += (s, e) => FiltrarProductos();
        btnBuscarProducto.Click += (s, e) => FiltrarProductos();
        btnLimpiarProducto.Click += (s, e) =>
        {
            txtBuscarProducto.Clear();
            FiltrarProductos();
        };
        // Mostrar inactivos productos
        chkMostrarInactivosProductos.CheckedChanged += (s, e) => FiltrarProductos();

        // Configurar eventos para los controles de búsqueda de categorías
        txtBuscarCategoria.TextChanged += (s, e) => FiltrarCategorias();
        btnBuscarCategoria.Click += (s, e) => FiltrarCategorias();
        btnLimpiarCategoria.Click += (s, e) =>
        {
            txtBuscarCategoria.Clear();
            FiltrarCategorias();
        };
        // Mostrar inactivas categorías
        chkMostrarInactivasCategorias.CheckedChanged += (s, e) => FiltrarCategorias();
    }

    private void FiltrarProductos()
    {
        var textoBusqueda = txtBuscarProducto?.Text.Trim().ToLowerInvariant() ?? string.Empty;
        var incluirInactivos = chkMostrarInactivosProductos?.Checked == true;

        _productosFiltrados.RaiseListChangedEvents = false;
        _productosFiltrados.Clear();

        var productosFiltrados = _todosProductos.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(textoBusqueda))
        {
            productosFiltrados = productosFiltrados.Where(p =>
                p.Nombre.ToLowerInvariant().Contains(textoBusqueda) ||
                p.CodigoProducto.ToString().Contains(textoBusqueda) ||
                (p.Descripcion != null && p.Descripcion.ToLowerInvariant().Contains(textoBusqueda)));
        }

        // Filtrar por estado según el checkbox
        if (!incluirInactivos)
        {
            productosFiltrados = productosFiltrados.Where(p => p.Activo);
        }

        foreach (var producto in productosFiltrados)
        {
            _productosFiltrados.Add(producto);
        }

        _productosFiltrados.RaiseListChangedEvents = true;
        _bindingSourceProductos.ResetBindings(false);
    }

    private void FiltrarCategorias()
    {
        var textoBusqueda = txtBuscarCategoria?.Text.Trim().ToLowerInvariant() ?? string.Empty;
        var incluirInactivas = chkMostrarInactivasCategorias?.Checked == true;

        _categoriasFiltradas.RaiseListChangedEvents = false;
        _categoriasFiltradas.Clear();

        var categoriasFiltradas = _todasCategorias.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(textoBusqueda))
        {
            categoriasFiltradas = categoriasFiltradas.Where(c =>
                c.NombreCategoria.ToLowerInvariant().Contains(textoBusqueda));
        }

        // Filtrar por estado según el checkbox
        if (!incluirInactivas)
        {
            categoriasFiltradas = categoriasFiltradas.Where(c => c.Activo);
        }

        foreach (var categoria in categoriasFiltradas)
        {
            _categoriasFiltradas.Add(categoria);
        }

        _categoriasFiltradas.RaiseListChangedEvents = true;
        _bindingSourceCategorias.ResetBindings(false);
    }

    private void CargarProductos()
    {
        try
        {
            var productos = _productoRepository.GetProductosConCategoria();
            
            _todosProductos.RaiseListChangedEvents = false;
            _todosProductos.Clear();
            foreach (var producto in productos)
            {
                _todosProductos.Add(producto);
            }
            _todosProductos.RaiseListChangedEvents = true;
            
            FiltrarProductos();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CargarCategorias()
    {
        try
        {
            var categorias = _categoriaRepository.ObtenerCategorias(false); // Obtener todas incluyendo inactivas
            
            _todasCategorias.RaiseListChangedEvents = false;
            _todasCategorias.Clear();
            foreach (var categoria in categorias)
            {
                _todasCategorias.Add(categoria);
            }
            _todasCategorias.RaiseListChangedEvents = true;
            
            FiltrarCategorias();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar categorías: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnAgregarProducto_Click(object? sender, EventArgs e)
    {
        try
        {
            // Solo obtener categorías activas para crear productos
            var categorias = _categoriaRepository.ObtenerCategorias(true);
            if (categorias.Count == 0)
            {
                MessageBox.Show("Debe crear al menos una categoría activa antes de agregar productos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialogo = new ProductoDialog(categorias);
            if (dialogo.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var producto = dialogo.Producto;
            if (producto == null)
            {
                return;
            }

            if (_productoRepository.CrearProducto(producto))
            {
                MessageBox.Show("Producto agregado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProductos();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el producto.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al agregar producto: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnEditarProducto_Click(object? sender, EventArgs e)
    {
        // La grilla está enlazada a ProductoConCategoriaDto, no a ProductoDto
        if (dgvProductos.CurrentRow?.DataBoundItem is not ProductoConCategoriaDto item)
        {
            MessageBox.Show("Debe seleccionar un producto.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var producto = new ProductoDto
        {
            CodigoProducto = item.CodigoProducto,
            Nombre = item.Nombre,
            Descripcion = item.Descripcion,
            Precio = item.Precio,
            Stock = item.Stock,
            IdCategoria = item.IdCategoria,
            Estado = item.Activo
        };

        try
        {
            // Solo obtener categorías activas para editar productos
            var categorias = _categoriaRepository.ObtenerCategorias(true);
            using var dialogo = new ProductoDialog(categorias, producto);
            if (dialogo.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            var productoActualizado = dialogo.Producto;
            if (productoActualizado == null)
            {
                return;
            }

            if (_productoRepository.ActualizarProducto(productoActualizado))
            {
                MessageBox.Show("Producto actualizado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProductos();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el producto.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al editar producto: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnActualizarStock_Click(object? sender, EventArgs e)
    {
        if (dgvProductos.CurrentRow?.DataBoundItem is not ProductoConCategoriaDto item)
        {
            MessageBox.Show("Debe seleccionar un producto.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var dialogo = new StockDialog(item.Nombre, item.Stock);
        if (dialogo.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var nuevoStock = dialogo.NuevoStock;

        try
        {
            if (_productoRepository.ActualizarStock(item.CodigoProducto, nuevoStock))
            {
                MessageBox.Show("Stock actualizado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProductos();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el stock.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al actualizar stock: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnAgregarCategoria_Click(object? sender, EventArgs e)
    {
        using var dialogo = new CategoriaDialog();
        if (dialogo.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var nombreCategoria = dialogo.NombreCategoria;

        try
        {
            if (_categoriaRepository.CrearCategoria(nombreCategoria))
            {
                MessageBox.Show("Categoría agregada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCategorias();
            }
            else
            {
                MessageBox.Show("No se pudo agregar la categoría.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al agregar categoría: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnToggleEstadoProducto_Click(object? sender, EventArgs e)
    {
        if (dgvProductos.CurrentRow?.DataBoundItem is not ProductoConCategoriaDto item)
        {
            MessageBox.Show("Debe seleccionar un producto.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var nuevoEstado = !item.Activo;
        var accion = nuevoEstado ? "activar" : "desactivar";
        var confirmacion = MessageBox.Show(
            $"¿Está seguro de {accion} el producto '{item.Nombre}'?",
            "Confirmar",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
        if (confirmacion != DialogResult.Yes)
        {
            return;
        }

        try
        {
            if (_productoRepository.ActualizarEstadoProducto(item.CodigoProducto, nuevoEstado))
            {
                MessageBox.Show($"Producto {(nuevoEstado ? "activado" : "desactivado")} correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProductos();
            }
            else
            {
                MessageBox.Show("No se pudo cambiar el estado del producto.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cambiar estado del producto: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnToggleEstadoCategoria_Click(object? sender, EventArgs e)
    {
        if (dgvCategorias.CurrentRow?.DataBoundItem is not CategoriaDto categoria)
        {
            MessageBox.Show("Debe seleccionar una categoría.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var activar = !categoria.Activo;
        var accion = activar ? "activar" : "desactivar";
        var confirmacion = MessageBox.Show(
            $"¿Está seguro de {accion} la categoría '{categoria.NombreCategoria}'?",
            "Confirmar",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
        if (confirmacion != DialogResult.Yes)
        {
            return;
        }

        try
        {
            var ok = activar
                ? _categoriaRepository.ActivarCategoria(categoria.IdCategoria)
                : _categoriaRepository.DesactivarCategoria(categoria.IdCategoria);

            if (ok)
            {
                MessageBox.Show($"Categoría {(activar ? "activada" : "desactivada")} correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCategorias();
            }
            else
            {
                MessageBox.Show("No se pudo cambiar el estado de la categoría.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cambiar estado de la categoría: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    ///     Diálogo para agregar o editar productos.
    /// </summary>
    private sealed class ProductoDialog : Form
    {
        private readonly TextBox _txtCodigo;
        private readonly TextBox _txtNombre;
        private readonly TextBox _txtDescripcion;
        private readonly TextBox _txtPrecio;
        private readonly TextBox _txtStock;
        private readonly ComboBox _cboCategoria;
        private readonly CheckBox _chkEstado;
        private readonly bool _esEdicion;

        public ProductoDto? Producto { get; private set; }

        public ProductoDialog(IReadOnlyList<CategoriaDto> categorias, ProductoDto? productoExistente = null)
        {
            _esEdicion = productoExistente != null;

            Text = _esEdicion ? "Editar Producto" : "Agregar Producto";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(12);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            for (int i = 0; i < 8; i++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            var lblCodigo = new Label { Text = "Código (10 dígitos):", Anchor = AnchorStyles.Left, AutoSize = true };
            _txtCodigo = new TextBox { Width = 250, MaxLength = 10, Enabled = !_esEdicion };
            _txtCodigo.KeyPress += TxtCodigo_KeyPress;

            var lblNombre = new Label { Text = "Nombre:", Anchor = AnchorStyles.Left, AutoSize = true };
            _txtNombre = new TextBox { Width = 250, MaxLength = 100 };

            var lblDescripcion = new Label { Text = "Descripción:", Anchor = AnchorStyles.Left, AutoSize = true };
            _txtDescripcion = new TextBox { Width = 250, MaxLength = 200 };

            var lblPrecio = new Label { Text = "Precio:", Anchor = AnchorStyles.Left, AutoSize = true };
            _txtPrecio = new TextBox { Width = 250 };
            _txtPrecio.KeyPress += TxtPrecio_KeyPress;

            var lblStock = new Label { Text = "Stock:", Anchor = AnchorStyles.Left, AutoSize = true };
            _txtStock = new TextBox { Width = 250 };
            _txtStock.KeyPress += TxtStock_KeyPress;

            var lblCategoria = new Label { Text = "Categoría:", Anchor = AnchorStyles.Left, AutoSize = true };
            _cboCategoria = new ComboBox
            {
                Width = 250,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = categorias.ToList(),
                DisplayMember = nameof(CategoriaDto.NombreCategoria),
                ValueMember = nameof(CategoriaDto.IdCategoria)
            };

            _chkEstado = new CheckBox { Text = "Activo", Checked = true, AutoSize = true };

            if (productoExistente != null)
            {
                _txtCodigo.Text = productoExistente.CodigoProducto.ToString();
                _txtNombre.Text = productoExistente.Nombre;
                _txtDescripcion.Text = productoExistente.Descripcion ?? string.Empty;
                _txtPrecio.Text = productoExistente.Precio.ToString("F2");
                _txtStock.Text = productoExistente.Stock.ToString();
                _chkEstado.Checked = productoExistente.Estado;

                var categoriaSeleccionada = categorias.FirstOrDefault(c => c.IdCategoria == productoExistente.IdCategoria);
                if (categoriaSeleccionada != null)
                {
                    _cboCategoria.SelectedItem = categoriaSeleccionada;
                }
            }

            layout.Controls.Add(lblCodigo, 0, 0);
            layout.Controls.Add(_txtCodigo, 1, 0);
            layout.Controls.Add(lblNombre, 0, 1);
            layout.Controls.Add(_txtNombre, 1, 1);
            layout.Controls.Add(lblDescripcion, 0, 2);
            layout.Controls.Add(_txtDescripcion, 1, 2);
            layout.Controls.Add(lblPrecio, 0, 3);
            layout.Controls.Add(_txtPrecio, 1, 3);
            layout.Controls.Add(lblStock, 0, 4);
            layout.Controls.Add(_txtStock, 1, 4);
            layout.Controls.Add(lblCategoria, 0, 5);
            layout.Controls.Add(_cboCategoria, 1, 5);
            layout.Controls.Add(_chkEstado, 1, 6);

            var panelBotones = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 12, 0, 0)
            };

            var btnCancelar = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                AutoSize = true
            };

            var btnGuardar = new Button
            {
                Text = "Guardar",
                AutoSize = true,
                BackColor = Color.FromArgb(55, 130, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += (_, _) => GuardarProducto();

            panelBotones.Controls.Add(btnCancelar);
            panelBotones.Controls.Add(btnGuardar);

            layout.Controls.Add(panelBotones, 0, 7);
            layout.SetColumnSpan(panelBotones, 2);

            Controls.Add(layout);

            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
        }

        private static void TxtCodigo_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Solo permitir dígitos y teclas de control
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private static void TxtPrecio_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Permitir dígitos, coma decimal y teclas de control
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            // Solo permitir una coma
            if (e.KeyChar == ',' && sender is TextBox txt && txt.Text.Contains(','))
            {
                e.Handled = true;
            }
        }

        private static void TxtStock_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Solo permitir dígitos y teclas de control
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void GuardarProducto()
        {
            if (string.IsNullOrWhiteSpace(_txtCodigo.Text))
            {
                MessageBox.Show("Debe ingresar el código del producto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtCodigo.Focus();
                return;
            }

            var codigoText = _txtCodigo.Text.Trim();

            // Validar que tenga exactamente 10 dígitos
            if (codigoText.Length != 10)
            {
                MessageBox.Show("El código del producto debe tener exactamente 10 dígitos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtCodigo.Focus();
                return;
            }

            if (!long.TryParse(codigoText, out var codigo) || codigo <= 0)
            {
                MessageBox.Show("El código debe ser un número positivo.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtCodigo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(_txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar el nombre del producto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtNombre.Focus();
                return;
            }

            if (!decimal.TryParse(_txtPrecio.Text, out var precio) || precio < 0)
            {
                MessageBox.Show("Debe ingresar un precio válido (mayor o igual a 0).", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtPrecio.Focus();
                return;
            }

            if (!short.TryParse(_txtStock.Text, out var stock) || stock < 0)
            {
                MessageBox.Show("Debe ingresar un stock válido (mayor o igual a 0, máximo 32767).", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtStock.Focus();
                return;
            }

            if (_cboCategoria.SelectedItem is not CategoriaDto categoria)
            {
                MessageBox.Show("Debe seleccionar una categoría.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _cboCategoria.Focus();
                return;
            }

            Producto = new ProductoDto
            {
                CodigoProducto = codigo,
                Nombre = _txtNombre.Text.Trim(),
                Descripcion = string.IsNullOrWhiteSpace(_txtDescripcion.Text) ? null : _txtDescripcion.Text.Trim(),
                Precio = precio,
                Stock = stock,
                IdCategoria = categoria.IdCategoria,
                Estado = _chkEstado.Checked
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }

    /// <summary>
    ///     Diálogo para actualizar el stock de un producto.
    /// </summary>
    private sealed class StockDialog : Form
    {
        private readonly TextBox _txtStock;

        public short NuevoStock { get; private set; }

        public StockDialog(string nombreProducto, short stockActual)
        {
            Text = "Actualizar Stock";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(12);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            var lblMensaje = new Label
            {
                Text = $"Ingrese el nuevo stock para '{nombreProducto}':\n(Stock actual: {stockActual})",
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 8)
            };

            _txtStock = new TextBox
            {
                Width = 250,
                Text = stockActual.ToString()
            };
            _txtStock.KeyPress += TxtStock_KeyPress;

            var panelBotones = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 12, 0, 0)
            };

            var btnCancelar = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                AutoSize = true
            };

            var btnAceptar = new Button
            {
                Text = "Aceptar",
                AutoSize = true,
                BackColor = Color.FromArgb(55, 130, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAceptar.FlatAppearance.BorderSize = 0;
            btnAceptar.Click += (_, _) => Guardar();

            panelBotones.Controls.Add(btnCancelar);
            panelBotones.Controls.Add(btnAceptar);

            layout.Controls.Add(lblMensaje, 0, 0);
            layout.Controls.Add(_txtStock, 0, 1);
            layout.Controls.Add(panelBotones, 0, 2);

            Controls.Add(layout);

            AcceptButton = btnAceptar;
            CancelButton = btnCancelar;
        }

        private static void TxtStock_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Solo permitir dígitos y teclas de control
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Guardar()
        {
            if (!short.TryParse(_txtStock.Text, out var stock) || stock < 0)
            {
                MessageBox.Show("Debe ingresar un número válido entre 0 y 32767.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtStock.Focus();
                return;
            }

            NuevoStock = stock;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    /// <summary>
    ///     Diálogo para agregar una nueva categoría.
    /// </summary>
    private sealed class CategoriaDialog : Form
    {
        private readonly TextBox _txtNombre;

        public string NombreCategoria { get; private set; } = string.Empty;

        public CategoriaDialog()
        {
            Text = "Agregar Categoría";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(12);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            var lblMensaje = new Label
            {
                Text = "Ingrese el nombre de la nueva categoría:",
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 8)
            };

            _txtNombre = new TextBox
            {
                Width = 250,
                MaxLength = 100
            };

            var panelBotones = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 12, 0, 0)
            };

            var btnCancelar = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                AutoSize = true
            };

            var btnAceptar = new Button
            {
                Text = "Aceptar",
                AutoSize = true,
                BackColor = Color.FromArgb(55, 130, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAceptar.FlatAppearance.BorderSize = 0;
            btnAceptar.Click += (_, _) => Guardar();

            panelBotones.Controls.Add(btnCancelar);
            panelBotones.Controls.Add(btnAceptar);

            layout.Controls.Add(lblMensaje, 0, 0);
            layout.Controls.Add(_txtNombre, 0, 1);
            layout.Controls.Add(panelBotones, 0, 2);

            Controls.Add(layout);

            AcceptButton = btnAceptar;
            CancelButton = btnCancelar;
        }

        private void Guardar()
        {
            var nombre = _txtNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Debe ingresar el nombre de la categoría.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtNombre.Focus();
                return;
            }

            NombreCategoria = nombre;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
