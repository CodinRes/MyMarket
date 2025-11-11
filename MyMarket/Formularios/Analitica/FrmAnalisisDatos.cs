using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MyMarket.Datos.Modelos.AnaliticaDTOs;
using MyMarket.Datos.Repositorios;
using MyMarket.Datos.Modelos;
using MyMarket.Servicios.Estado;
using MyMarket.Servicios.Estado.Modelos;
using MyMarket.Servicios;

namespace MyMarket.Formularios.Analitica
{
    public partial class FrmAnalisisDatos : Form
    {
        private readonly AnaliticaRepository _repo;
        private enum Vista { Vendedores, Productos }
        private Vista _vistaActual = Vista.Vendedores;

        private EstadoEmpleado? _sesionActiva;
        private bool _esGerente;

        // Labels detectados dinámicamente para evitar dependencia de nombres del diseñador
        private Label? _lblPeriodoTitle;
        private Label? _lblCategoriasTitle;

        public FrmAnalisisDatos()
        {
            InitializeComponent();

            // localizar labels asociados a los combos (sin depender de label1/label2)
            _lblPeriodoTitle = TryFindClosestLabelAbove(panelTabs, cmbPeriodo, fallbackText: "Período");
            _lblCategoriasTitle = TryFindClosestLabelAbove(panelTabs, cmbCategoria, fallbackText: "Categorías");

            // load persisted session to detect role of current user
            try
            {
                var almacen = new AlmacenEstadoAplicacion();
                var estado = almacen.Cargar();
                _sesionActiva = estado?.SesionActiva;
                _esGerente = _sesionActiva != null && !string.IsNullOrWhiteSpace(_sesionActiva.RolDescripcion) && _sesionActiva.RolDescripcion.Trim().ToLowerInvariant().Contains("gerente");
            }
            catch
            {
                _sesionActiva = null;
                _esGerente = false;
            }

            // Inicializa el repositorio usando la cadena configurada en App.config ('TiendaDb').
            _repo = new AnaliticaRepository();

            // Default period a "Año" si existe
            try
            {
                if (cmbPeriodo != null && cmbPeriodo.Items.Count >= 3)
                {
                    cmbPeriodo.SelectedIndex = 2; // Año
                }
            }
            catch
            {
                // ignorar si el diseñador no inicializó cmbPeriodo aún
            }

            // If current user is not gerente, restrict UI: hide vendedores tab and bottom panels
            if (!_esGerente)
            {
                btnVendedores.Visible = false;
                // force products view and hide bottom panels (charts)
                panelBottom.Visible = false;
                _vistaActual = Vista.Productos;
            }

            // Eventos
            Load += FrmAnalisisDatos_Load;
            Resize += (_, _) => ReposicionarPeriodo();

            // Comportamiento visual tipo "pestañas"
            btnVendedores.Click += (_, _) => { if (_esGerente) CambiarVista(Vista.Vendedores); };
            btnProductos.Click += (_, _) => CambiarVista(Vista.Productos);

            // Hover visual
            btnVendedores.MouseEnter += (_, _) => OnTabHover(btnVendedores, true);
            btnVendedores.MouseLeave += (_, _) => OnTabHover(btnVendedores, false);
            btnProductos.MouseEnter += (_, _) => OnTabHover(btnProductos, true);
            btnProductos.MouseLeave += (_, _) => OnTabHover(btnProductos, false);

            // Cambios de periodo recargan automáticamente la vista actual
            if (cmbPeriodo != null)
            {
                cmbPeriodo.SelectedIndexChanged += (_, _) => RefrescarVistaActual();
            }

            // category filter
            if (cmbCategoria != null)
            {
                cmbCategoria.SelectedIndexChanged += (_, _) => RefrescarVistaActual();
                cmbCategoria.Items.Add("Todos");
                cmbCategoria.SelectedIndex = 0;
            }

            // Habilitar ordenamiento por columnas en las grillas de reportes
            try
            {
                if (dgvResultados != null)
                {
                    DataGridViewHelper.HabilitarOrdenamientoPorColumna(dgvResultados);
                }
                if (dgvLowStock != null)
                {
                    DataGridViewHelper.HabilitarOrdenamientoPorColumna(dgvLowStock);
                }
            }
            catch { /* ignore wiring issues at design-time */ }

            // Inicializar estado visual: seleccionar una pestaña por defecto
            CambiarVista(_vistaActual);
        }

        private void FrmAnalisisDatos_Load(object? sender, EventArgs e)
        {
            ReposicionarPeriodo();
            // cargar categorias si existe el combo
            if (cmbCategoria != null)
            {
                try
                {
                    var categorias = _repo.GetCategorias();
                    cmbCategoria.Items.Clear();
                    cmbCategoria.Items.Add("Todos");
                    foreach (var c in categorias)
                    {
                        cmbCategoria.Items.Add(new ComboBoxItem<int>(c.NombreCategoria, c.IdCategoria));
                    }
                    cmbCategoria.SelectedIndex = 0;
                }
                catch
                {
                    // ignorar problemas de carga
                }
            }
            // Inicializar gráfico y grilla
            InicializarChartCategorias();
            InicializarChartVendedores();
            InicializarLowStockGrid();
            // configurar selector de dia
            if (dtpDia != null)
            {
                dtpDia.Value = DateTime.Today;
                dtpDia.ValueChanged += (_, _) => ActualizarTotalDia();
                ActualizarTotalDia();
            }
        }

        private void ActualizarTotalDia()
        {
            if (lblTotalDia == null || dtpDia == null) return;
            var inicio = dtpDia.Value.Date;
            var ventas = _repo.GetVentasPorCategoria(inicio);
            var total = ventas.Sum(v => v.TotalGenerado);
            lblTotalDia.Text = $"Total del día:\n{total:C2}";
        }

        private void InicializarChartCategorias()
        {
            if (chartCategorias == null) return;
            chartCategorias.Series.Clear();
            chartCategorias.Legends.Clear();
            chartCategorias.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.BrightPastel;
            chartCategorias.Legends.Add(new Legend("CategoriasLegend")
            {
                Docking = Docking.Right,
                Alignment = StringAlignment.Center,
                LegendStyle = LegendStyle.Column
            });
            chartCategorias.ChartAreas.Clear();
            chartCategorias.ChartAreas.Add(new ChartArea("MainArea"));
        }

        private void InicializarChartVendedores()
        {
            if (chartVendedores == null) return;
            chartVendedores.Series.Clear();
            chartVendedores.Legends.Clear();
            chartVendedores.ChartAreas.Clear();
            var area = new ChartArea("VendedoresArea");
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Angle = -45;
            area.AxisY.LabelStyle.Format = "N0";
            chartVendedores.ChartAreas.Add(area);
            chartVendedores.Legends.Add(new Legend("VendedoresLegend") { Docking = Docking.Bottom });
            chartVendedores.Palette = ChartColorPalette.Pastel;
        }

        private void InicializarLowStockGrid()
        {
            if (dgvLowStock == null) return;
            dgvLowStock.AutoGenerateColumns = false;
            dgvLowStock.Columns.Clear();
            dgvLowStock.DataSource = null;
            dgvLowStock.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodigoProducto",
                HeaderText = "Código",
                DataPropertyName = "CodigoProducto",
                Width = 80,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvLowStock.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreProducto",
                HeaderText = "Producto",
                DataPropertyName = "NombreProducto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvLowStock.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock",
                DataPropertyName = "Stock",
                Width = 60,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgvLowStock.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreCategoria",
                HeaderText = "Categoría",
                DataPropertyName = "NombreCategoria",
                Width = 120,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            // attach formatting - corregir firma del evento
            dgvLowStock.CellFormatting -= dgvLowStock_CellFormatting;
            dgvLowStock.CellFormatting += dgvLowStock_CellFormatting;
        }

        private void ActualizarLowStockGrid()
        {
            if (dgvLowStock == null) return;
            dgvLowStock.DataSource = null;
            var bajo = _repo.GetProductosBajoStock(5);
            if (bajo == null || bajo.Count == 0)
            {
                dgvLowStock.Rows.Clear();
                dgvLowStock.Columns.Clear();
                dgvLowStock.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Mensaje",
                    HeaderText = "Información",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });
                dgvLowStock.Rows.Add("No hay productos con bajo stock");
                dgvLowStock.ClearSelection();
                return;
            }

            InicializarLowStockGrid();
            // Bind as sortable list through BindingSource
            try
            {
                var binding = new BindingSource { DataSource = new SortableBindingList<ProductoStockDTO>(bajo) };
                dgvLowStock.DataSource = binding;
            }
            catch
            {
                dgvLowStock.DataSource = bajo;
            }
        }

        private void dgvLowStock_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null) return;
            if (grid.Columns[e.ColumnIndex].Name == "Stock" && e.Value != null && e.CellStyle != null)
            {
                e.CellStyle.ForeColor = Color.Red;
                if (grid.DefaultCellStyle.Font != null)
                {
                    e.CellStyle.Font = new Font(grid.DefaultCellStyle.Font, FontStyle.Bold);
                }
            }
        }

        private void ReposicionarPeriodo()
        {
            if (cmbPeriodo == null || panelTabs == null) return;
            // Colocar cmbPeriodo alineado a la derecha dentro de panelTabs con un padding de 12px
            var padding = 12;
            var right = panelTabs.ClientSize.Width - padding - cmbPeriodo.Width;
            var btnProductosRight = btnProductos?.Right ?? 0;
            cmbPeriodo.Location = new Point(Math.Max(right, btnProductosRight + 12), cmbPeriodo.Location.Y);
            // posicionar cmbCategoria antes del periodo
            if (cmbCategoria != null)
            {
                cmbCategoria.Location = new Point(Math.Max(btnProductosRight + 12, right - cmbCategoria.Width - 8), cmbCategoria.Location.Y);
            }

            // Reposicionar títulos encima de sus combos si existen
            if (_lblPeriodoTitle != null)
            {
                var xPeriodo = cmbPeriodo.Left;
                var yPeriodo = Math.Max(panelTabs.Padding.Top / 2, cmbPeriodo.Top - _lblPeriodoTitle.Height - 2);
                _lblPeriodoTitle.Location = new Point(xPeriodo, yPeriodo);
            }
            if (cmbCategoria != null && _lblCategoriasTitle != null)
            {
                var xCat = cmbCategoria.Left;
                var yCat = Math.Max(panelTabs.Padding.Top / 2, cmbCategoria.Top - _lblCategoriasTitle.Height - 2);
                _lblCategoriasTitle.Location = new Point(xCat, yCat);
                _lblCategoriasTitle.Visible = cmbCategoria.Visible;
            }
        }

        private void OnTabHover(Button btn, bool hover)
        {
            if (btn == null) return;
            if (hover)
            {
                if ((_vistaActual == Vista.Vendedores && btn == btnVendedores) || (_vistaActual == Vista.Productos && btn == btnProductos))
                    return; // mantener estilo seleccionado
                btn.BackColor = SystemColors.ControlLight;
            }
            else
            {
                if ((_vistaActual == Vista.Vendedores && btn == btnVendedores) || (_vistaActual == Vista.Productos && btn == btnProductos))
                    return; // mantener estilo seleccionado
                btn.BackColor = SystemColors.Control;
            }
        }

        private void CambiarVista(Vista vista)
        {
            _vistaActual = vista;

            if (vista == Vista.Vendedores)
            {
                SetSelectedTabStyle(btnVendedores);
                SetUnselectedTabStyle(btnProductos);
                if (cmbCategoria != null) cmbCategoria.Visible = false;
                if (panelBottom != null) panelBottom.Visible = true;

                if (chartVendedores != null) chartVendedores.Visible = true;
                if (chartCategorias != null) chartCategorias.Visible = false;
                if (panelDaily != null) panelDaily.Visible = true;
                if (dgvLowStock != null) dgvLowStock.Visible = false;

                // Ocultar etiqueta Categorías cuando no corresponde
                if (_lblCategoriasTitle != null) _lblCategoriasTitle.Visible = false;
            }
            else // Vista.Productos
            {
                SetSelectedTabStyle(btnProductos);
                SetUnselectedTabStyle(btnVendedores);
                if (cmbCategoria != null) cmbCategoria.Visible = true;
                if (panelBottom != null) panelBottom.Visible = true;

                if (chartVendedores != null) chartVendedores.Visible = false;
                if (chartCategorias != null) chartCategorias.Visible = true;
                if (panelDaily != null) panelDaily.Visible = false;
                if (dgvLowStock != null) dgvLowStock.Visible = true;

                // Mostrar etiqueta Categorías sólo si el combo está visible
                if (_lblCategoriasTitle != null) _lblCategoriasTitle.Visible = (cmbCategoria?.Visible == true);
            }

            // Asegurar sincronización si cambia luego por alguna lógica extra
            if (_lblCategoriasTitle != null && cmbCategoria != null && _lblCategoriasTitle.Visible != cmbCategoria.Visible)
            {
                _lblCategoriasTitle.Visible = cmbCategoria.Visible;
            }

            RefrescarVistaActual();
        }

        private void SetSelectedTabStyle(Button btn)
        {
            btn.BackColor = Color.White;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 180);
            btn.ForeColor = SystemColors.ControlText;
        }

        private void SetUnselectedTabStyle(Button btn)
        {
            btn.BackColor = SystemColors.Control;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = SystemColors.ControlDarkDark;
        }

        private void RefrescarVistaActual()
        {
            DateTime? desde = ObtenerFechaDesde();
            switch (_vistaActual)
            {
                case Vista.Vendedores:
                    CargarReporteVendedores(desde);
                    ActualizarChartVendedores(desde);
                    break;
                case Vista.Productos:
                    CargarReporteProductos(desde);
                    ActualizarChartCategorias(desde);
                    break;
            }
        }

        private DateTime? ObtenerFechaDesde()
        {
            if (cmbPeriodo == null) return null;
            var seleccionado = cmbPeriodo.SelectedItem?.ToString() ?? "Día";
            switch (seleccionado)
            {
                case "Día":
                    return DateTime.Today;
                case "Mes":
                    return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                case "Año":
                    return new DateTime(DateTime.Today.Year, 1, 1);
                default:
                    return null;
            }
        }

        private void CargarReporteVendedores(DateTime? fechaDesde)
        {
            var datos = _repo.GetReporteVendedores(fechaDesde);
            if (datos == null) return;

            // Bind as sortable list
            try
            {
                var binding = new BindingSource { DataSource = new SortableBindingList<ReporteVendedorDTO>(datos) };
                dgvResultados.DataSource = binding;
            }
            catch
            {
                dgvResultados.DataSource = datos;
            }

            // Remove 'btnVer' column if present (only used for Productos view)
            if (dgvResultados.Columns.Contains("btnVer"))
            {
                dgvResultados.Columns.Remove("btnVer");
            }

            // Formateos
            if (dgvResultados.Columns.Contains("CantidadVentas"))
                dgvResultados.Columns["CantidadVentas"].DefaultCellStyle.Format = "N0";
            if (dgvResultados.Columns.Contains("TotalVendido"))
                dgvResultados.Columns["TotalVendido"].DefaultCellStyle.Format = "C2";

            // Habilitar sort en columnas
            foreach (DataGridViewColumn col in dgvResultados.Columns)
            {
                if (col is DataGridViewButtonColumn) continue;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            // Asegurar columna btnVer posicionionada (no aplicará si fue removida)
            if (dgvResultados.Columns.Contains("btnVer"))
            {
                var btn = dgvResultados.Columns["btnVer"];
                btn.DisplayIndex = dgvResultados.Columns.Count - 1;
                btn.Width = 40;
                btn.MinimumWidth = 40;
            }
            // panelBottom should remain visible in vendedores view so the chart can show
        }

        private void CargarReporteProductos(DateTime? fechaDesde)
        {
            var datos = _repo.GetReporteProductos(fechaDesde);
            if (datos == null) return;

            // aplicar filtro de categoria si corresponde
            if (cmbCategoria != null && cmbCategoria.SelectedItem is ComboBoxItem<int> sel && sel.Value > 0)
            {
                datos = datos.Where(d => d.IdCategoria == sel.Value).ToList();
            }

            // Si no hay datos, NO agregar columna de mensaje; dejar la grilla vacía
            if (datos.Count == 0)
            {
                // quitar columna mensaje si quedó de un estado previo
                if (dgvResultados.Columns.Contains("Mensaje"))
                {
                    dgvResultados.Columns.Remove("Mensaje");
                }

                // bind lista vacía para mantener estructura ordenable sin filas
                try
                {
                    var bindingVacio = new BindingSource { DataSource = new SortableBindingList<ReporteProductoDTO>(datos) }; // datos está vacío
                    dgvResultados.DataSource = bindingVacio;
                }
                catch
                {
                    dgvResultados.DataSource = datos; // vacío
                }

                // asegurar que cualquier columna de botón previa se remueva (no aplica sobre lista vacía aún)
                if (dgvResultados.Columns.Contains("btnVer"))
                {
                    dgvResultados.Columns.Remove("btnVer");
                }

                // mostrar panelBottom (chart y low-stock) aunque la tabla esté vacía
                if (panelBottom != null) panelBottom.Visible = true;
                ActualizarChartCategorias(fechaDesde);
                ActualizarLowStockGrid();
                return;
            }

            // Añadir columna de botón "Ver" si no existe (se hace antes de binding para mantener posición final)
            if (!dgvResultados.Columns.Contains("btnVer"))
            {
                var btnCol = new DataGridViewButtonColumn
                {
                    Name = "btnVer",
                    HeaderText = "",
                    Text = "Ver",
                    UseColumnTextForButtonValue = true,
                    Width = 40,
                    MinimumWidth = 40,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                };
                dgvResultados.Columns.Add(btnCol);
            }
            else
            {
                // asegurar que no sea ordenable
                dgvResultados.Columns["btnVer"].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // mostrar panelBottom en vista productos
            if (panelBottom != null) panelBottom.Visible = true;

            // actualizar chart y low-stock (chart recibirá fechaDesde para filtrar por periodo)
            ActualizarChartCategorias(fechaDesde);
            ActualizarLowStockGrid();

            // apply employee filter for non-gerente users
            if (!_esGerente && _sesionActiva != null)
            {
                try
                {
                    var idEmpleado = _sesionActiva.IdEmpleado;
                    var propCandidates = new[] { "IdEmpleado", "EmpleadoId", "IdVendedor", "VendedorId", "IdEmpleadoVendedor", "IdVendedorEmpleado", "IdUsuario" };
                    var itemType = datos.FirstOrDefault()?.GetType();
                    if (itemType != null)
                    {
                        System.Reflection.PropertyInfo? matchedProp = null;
                        foreach (var name in propCandidates)
                        {
                            var p = itemType.GetProperty(name);
                            if (p != null && (p.PropertyType == typeof(int) || p.PropertyType == typeof(long) || p.PropertyType == typeof(int?) || p.PropertyType == typeof(long?)))
                            {
                                matchedProp = p;
                                break;
                            }
                        }

                        if (matchedProp != null)
                        {
                            datos = datos.Where(d =>
                            {
                                var val = matchedProp.GetValue(d);
                                if (val == null) return false;
                                if (val is int i) return i == idEmpleado;
                                if (val is long l) return l == idEmpleado;
                                if (int.TryParse(val.ToString(), out var parsed)) return parsed == idEmpleado;
                                if (long.TryParse(val.ToString(), out var parsedL)) return parsedL == idEmpleado;
                                return false;
                            }).ToList();
                        }
                        else
                        {
                            var nameProp = itemType.GetProperty("NombreEmpleado") ?? itemType.GetProperty("Vendedor") ?? itemType.GetProperty("NombreVendedor");
                            if (nameProp != null && (_sesionActiva.Nombre != null || _sesionActiva.Apellido != null))
                            {
                                var expected = string.Join(" ", new[] { _sesionActiva.Nombre, _sesionActiva.Apellido }.Where(s => !string.IsNullOrWhiteSpace(s))).Trim();
                                if (!string.IsNullOrWhiteSpace(expected))
                                {
                                    datos = datos.Where(d =>
                                    {
                                        var v = nameProp.GetValue(d)?.ToString() ?? string.Empty;
                                        return v.IndexOf(expected, StringComparison.OrdinalIgnoreCase) >= 0;
                                    }).ToList();
                                }
                            }
                        }
                    }
                }
                catch { }
            }

            // Bind as sortable list (datos con posibles filtros aplicados)
            try
            {
                var binding = new BindingSource { DataSource = new SortableBindingList<ReporteProductoDTO>(datos) };
                dgvResultados.DataSource = binding;
            }
            catch
            {
                dgvResultados.DataSource = datos;
            }

            // remover cualquier columna de mensaje residual
            if (dgvResultados.Columns.Contains("Mensaje"))
                dgvResultados.Columns.Remove("Mensaje");

            // ocultar id de producto si existe
            if (dgvResultados.Columns.Contains("IdProducto"))
                dgvResultados.Columns["IdProducto"].Visible = false;

            // Formateos
            if (dgvResultados.Columns.Contains("TotalGenerado"))
                dgvResultados.Columns["TotalGenerado"].DefaultCellStyle.Format = "C2";
            if (dgvResultados.Columns.Contains("UnidadesVendidas"))
                dgvResultados.Columns["UnidadesVendidas"].DefaultCellStyle.Format = "N0";

            // Forzar el botón al final (derecha)
            if (dgvResultados.Columns.Contains("btnVer"))
            {
                var btn = dgvResultados.Columns["btnVer"];
                btn.DisplayIndex = dgvResultados.Columns.Count - 1;
                btn.Width = 40;
                btn.MinimumWidth = 40;
                btn.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Habilitar sort en columnas
            foreach (DataGridViewColumn col in dgvResultados.Columns)
            {
                if (col is DataGridViewButtonColumn) continue;
                if (col.Name == "Mensaje") continue;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void ActualizarChartCategorias(DateTime? fechaDesde)
        {
            if (chartCategorias == null) return;
            InicializarChartCategorias();
            var ventas = _repo.GetVentasPorCategoria(fechaDesde);
            chartCategorias.Series.Clear();
            var total = ventas.Sum(v => v.UnidadesVendidas);
            var s = new Series("Categorias") { ChartType = SeriesChartType.Pie };
            // Evitar mostrar etiquetas directamente sobre el gráfico
            s.IsValueShownAsLabel = false;
            chartCategorias.Series.Add(s);
            s.Legend = "CategoriasLegend";
            foreach (var v in ventas)
            {
                var idx = s.Points.AddY(v.UnidadesVendidas);
                s.Points[idx].LegendText = v.NombreCategoria;
                // ToolTip: mostrar nombre y porcentaje
                double porcentaje = total > 0 ? (double)v.UnidadesVendidas / total : 0;
                s.Points[idx].ToolTip = $"{v.NombreCategoria}: {porcentaje:P1}";
                // no label visible
                s.Points[idx].Label = string.Empty;
                // almacenar valor en LegendText y Tag si es necesario
                s.Points[idx].Tag = v;
            }
            chartCategorias.Invalidate();
        }

        private void ActualizarChartVendedores(DateTime? fechaDesde)
        {
            if (chartVendedores == null) return;
            InicializarChartVendedores();

            var datos = _repo.GetReporteVendedores(fechaDesde);
            if (datos == null || datos.Count == 0)
            {
                chartVendedores.Series.Clear();
                chartVendedores.Visible = false;
                return;
            }

            // seleccionar top 5 por cantidad de ventas
            var top5 = datos.OrderByDescending(d => d.CantidadVentas).Take(5).ToList();

            chartVendedores.Series.Clear();
            var s = new Series("Vendedores") { ChartType = SeriesChartType.Column, IsValueShownAsLabel = true };
            chartVendedores.Series.Add(s);

            foreach (var v in top5)
            {
                var idx = s.Points.AddY(v.CantidadVentas);
                s.Points[idx].AxisLabel = v.NombreEmpleado;
                s.Points[idx].ToolTip = $"{v.NombreEmpleado}: {v.CantidadVentas} ventas - {v.TotalVendido:C2}";
            }

            var area = chartVendedores.ChartAreas[0];
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Angle = -45;
            area.AxisY.LabelStyle.Format = "N0";

            chartVendedores.Visible = true;
            chartVendedores.BringToFront();
            chartVendedores.Invalidate();
        }

        private void dgvResultados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var grid = sender as DataGridView;
            if (grid == null) return;

            if (grid.Columns[e.ColumnIndex].Name == "btnVer")
            {
                // obtener id producto en la fila
                // primero intentar obtener columna 'IdProducto'
                if (grid.Columns.Contains("IdProducto"))
                {
                    var cell = grid.Rows[e.RowIndex].Cells["IdProducto"];
                    if (cell?.Value != null && int.TryParse(cell.Value.ToString(), out int idProducto))
                    {
                        using (var detalle = new FrmDetalleProducto(idProducto, _repo))
                        {
                            detalle.ShowDialog(this);
                        }
                        return;
                    }
                }

                // fallback: intentar obtener el Id desde el objeto enlazado (si existe la propiedad 'CodigoProducto' o 'IdProducto')
                var dataItem = grid.Rows[e.RowIndex].DataBoundItem;
                if (dataItem != null)
                {
                    // usar reflexion segura para intentar obtener una propiedad numérica
                    var prop = dataItem.GetType().GetProperty("IdProducto") ?? dataItem.GetType().GetProperty("CodigoProducto");
                    if (prop != null)
                    {
                        var val = prop.GetValue(dataItem);
                        if (val != null && int.TryParse(val.ToString(), out int idFromObj))
                        {
                            using (var detalle = new FrmDetalleProducto(idFromObj, _repo))
                            {
                                detalle.ShowDialog(this);
                            }
                        }
                    }
                }
            }
        }

        private void tableBottom_Paint(object sender, PaintEventArgs e)
        {

        }

        // helper combo box item
        private sealed class ComboBoxItem<T>
        {
            public ComboBoxItem(string text, T value)
            {
                Text = text;
                Value = value;
            }

            public string Text { get; }
            public T Value { get; }

            public override string ToString() => Text;
        }

        // Encuentra el Label más cercano por encima del control objetivo dentro de un contenedor
        private static Label? TryFindClosestLabelAbove(Control? container, Control? target, string? fallbackText = null)
        {
            if (container == null || target == null) return null;
            var labels = container.Controls.OfType<Label>().ToList();
            if (labels.Count == 0) return null;

            Label? closest = null;
            int bestDistance = int.MaxValue;
            foreach (var lbl in labels)
            {
                if (lbl.Bottom <= target.Top) // debe estar por encima
                {
                    // penalizar si no hay solapamiento horizontal
                    int overlap = Math.Min(lbl.Right, target.Right) - Math.Max(lbl.Left, target.Left);
                    if (overlap <= 0) continue;

                    int distance = target.Top - lbl.Bottom;
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        closest = lbl;
                    }
                }
            }

            // fallback por texto si no se encontró por geometría
            if (closest == null && !string.IsNullOrWhiteSpace(fallbackText))
            {
                closest = labels.FirstOrDefault(l => string.Equals(l.Text?.Trim(), fallbackText.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            return closest;
        }
    }
}
