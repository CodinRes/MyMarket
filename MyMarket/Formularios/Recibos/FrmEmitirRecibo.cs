using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Infraestructura;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;
using MyMarket.Servicios.Estado;

namespace MyMarket.Formularios.Recibos;

/// <summary>
///     Pantalla encargada de registrar una nueva venta y emitir el recibo correspondiente.
/// </summary>
public partial class FrmEmitirRecibo : Form
{
    private readonly ClienteRepository _clienteRepository;
    private readonly BindingSource _detalleBindingSource = new();
    private readonly BindingList<DetalleReciboViewModel> _detalleRecibo = new();
    private readonly Func<EmpleadoDto?> _empleadoActualProvider;
    private readonly FacturaRepository _facturaRepository;
    private readonly MetodoPagoRepository _metodoPagoRepository;
    private readonly ProductoRepository _productoRepository;
    private readonly SqlConnectionFactory _connectionFactory;
    private readonly AlmacenConfiguracion _almacenConfiguracion;

    private IReadOnlyList<ClienteDto> _clientes = Array.Empty<ClienteDto>();
    private IReadOnlyList<MetodoPagoDto> _metodosPago = Array.Empty<MetodoPagoDto>();
    private List<ProductoDto> _productosDisponibles = new();
    private ConfiguracionDto? _configuracionActual;

    /// <summary>
    ///     Inicializa el formulario indicando cómo acceder a la base de datos y a la sesión activa.
    /// </summary>
    public FrmEmitirRecibo(SqlConnectionFactory connectionFactory, Func<EmpleadoDto?> empleadoActualProvider)
    {
        if (connectionFactory is null)
        {
            throw new ArgumentNullException(nameof(connectionFactory));
        }

        _empleadoActualProvider = empleadoActualProvider
                                   ?? throw new ArgumentNullException(nameof(empleadoActualProvider));

        _connectionFactory = connectionFactory;
        _productoRepository = new ProductoRepository(connectionFactory);
        _clienteRepository = new ClienteRepository(connectionFactory);
        _facturaRepository = new FacturaRepository(connectionFactory);
        _metodoPagoRepository = new MetodoPagoRepository(connectionFactory);
        _almacenConfiguracion = new AlmacenConfiguracion();

        InitializeComponent();
        ConfigurarCombos();
        ConfigurarDataGridView();
        InicializarTotales();

        Load += FrmEmitirRecibo_Load;
        btnNuevoItem.Click += BtnNuevoItem_Click;
        btnQuitarItem.Click += BtnQuitarItem_Click;
        btnEmitirRecibo.Click += BtnEmitirRecibo_Click;
        btnGestionarMetodosPago.Click += BtnGestionarMetodosPago_Click;
        chkPagoEfectivo.CheckedChanged += ChkPagoEfectivo_CheckedChanged;
        cmbMetodoPago.SelectedIndexChanged += CmbMetodoPago_SelectedIndexChanged;
        cmbCliente.SelectedIndexChanged += CmbCliente_SelectedIndexChanged;
    }

    private void FrmEmitirRecibo_Load(object? sender, EventArgs e)
    {
        _configuracionActual = _almacenConfiguracion.Cargar();
        CargarClientes();
        CargarMetodosPago();
        CargarProductosDisponibles();
        ActualizarEstadoBotones();
    }

    /// <summary>
    ///     Configura los combos para utilizar objetos ricos en lugar de simples cadenas.
    /// </summary>
    private void ConfigurarCombos()
    {
        cmbCliente.DisplayMember = nameof(ComboOpcion<ClienteDto>.Descripcion);
        cmbMetodoPago.DisplayMember = nameof(ComboOpcion<MetodoPagoDto>.Descripcion);
    }

    /// <summary>
    ///     Prepara el <see cref="DataGridView"/> para mostrar el detalle de la venta.
    /// </summary>
    private void ConfigurarDataGridView()
    {
        dgvDetalle.AutoGenerateColumns = false;
        dgvDetalle.Columns.Clear();

        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colDescripcion",
            HeaderText = "Descripción",
            DataPropertyName = nameof(DetalleReciboViewModel.Descripcion),
            FillWeight = 40,
            ReadOnly = true
        });

        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colCantidad",
            HeaderText = "Cantidad",
            DataPropertyName = nameof(DetalleReciboViewModel.Cantidad),
            FillWeight = 15,
            ReadOnly = true
        });

        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colPrecioUnitario",
            HeaderText = "Precio unitario",
            DataPropertyName = nameof(DetalleReciboViewModel.PrecioUnitario),
            DefaultCellStyle = { Format = "C2" },
            FillWeight = 20,
            ReadOnly = true
        });

        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colSubtotal",
            HeaderText = "Subtotal",
            DataPropertyName = nameof(DetalleReciboViewModel.Subtotal),
            DefaultCellStyle = { Format = "C2" },
            FillWeight = 25,
            ReadOnly = true
        });

        _detalleBindingSource.DataSource = _detalleRecibo;
        dgvDetalle.DataSource = _detalleBindingSource;
    }

    /// <summary>
    ///     Resetea los textos de totales mostrados en pantalla.
    /// </summary>
    private void InicializarTotales()
    {
        txtSubtotal.Text = "$ 0,00";
        txtImpuestos.Text = "$ 0,00";
        txtComision.Text = "$ 0,00";
        txtDescuento.Text = "$ 0,00";
        txtTotal.Text = "$ 0,00";
    }

    private void CargarClientes()
    {
        try
        {
            _clientes = _clienteRepository.ObtenerClientes()
                                          .Where(c => c.Activo)
                                          .ToList();
        }
        catch (SqlException ex)
        {
            MostrarError("clientes suscriptos", ex.Message);
            _clientes = Array.Empty<ClienteDto>();
        }
        catch (Exception ex)
        {
            MostrarError("clientes suscriptos", ex.Message);
            _clientes = Array.Empty<ClienteDto>();
        }

        var opciones = new List<ComboOpcion<ClienteDto>>
        {
            new("Seleccione un cliente...", null, true),
            new("Cliente ocasional (sin registrar)", null)
        };

        opciones.AddRange(_clientes.Select(c =>
            new ComboOpcion<ClienteDto>($"{c.Apellido}, {c.Nombre} - DNI {c.Dni}", c)));

        cmbCliente.DataSource = opciones;
        cmbCliente.SelectedIndex = opciones.Count > 0 ? 0 : -1;
    }

    private void CargarMetodosPago()
    {
        try
        {
            _metodosPago = _metodoPagoRepository.ObtenerMetodosActivos();
        }
        catch (SqlException ex)
        {
            MostrarError("métodos de pago", ex.Message);
            _metodosPago = Array.Empty<MetodoPagoDto>();
        }
        catch (Exception ex)
        {
            MostrarError("métodos de pago", ex.Message);
            _metodosPago = Array.Empty<MetodoPagoDto>();
        }

        var opciones = new List<ComboOpcion<MetodoPagoDto>>
        {
            new("Seleccione un método de pago...", null, true)
        };

        opciones.AddRange(_metodosPago.Select(m =>
            new ComboOpcion<MetodoPagoDto>($"{m.ProveedorPago} (ID {m.IdentificacionPago})", m)));

        cmbMetodoPago.DataSource = opciones;
        cmbMetodoPago.SelectedIndex = opciones.Count > 0 ? 0 : -1;
    }

    private void CargarProductosDisponibles()
    {
        try
        {
            _productosDisponibles = _productoRepository.GetProductosDisponiblesParaVenta().ToList();
        }
        catch (SqlException ex)
        {
            MostrarError("productos", ex.Message);
            _productosDisponibles = new List<ProductoDto>();
        }
        catch (Exception ex)
        {
            MostrarError("productos", ex.Message);
            _productosDisponibles = new List<ProductoDto>();
        }

        ActualizarEstadoBotones();
    }

    private void BtnNuevoItem_Click(object? sender, EventArgs e)
    {
        var disponibles = ObtenerProductosConStockDisponible();
        if (disponibles.Count == 0)
        {
            MessageBox.Show("No hay productos con stock disponible para agregar.", "Emitir recibo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var selector = new FrmSeleccionarProducto(disponibles);
        if (selector.ShowDialog(this) != DialogResult.OK || selector.ProductoSeleccionado is null)
        {
            return;
        }

        AgregarOActualizarDetalle(selector.ProductoSeleccionado, selector.CantidadSeleccionada);
        ActualizarTotales();
        ActualizarEstadoBotones();
    }

    private void BtnQuitarItem_Click(object? sender, EventArgs e)
    {
        if (dgvDetalle.CurrentRow?.DataBoundItem is DetalleReciboViewModel detalle)
        {
            _detalleRecibo.Remove(detalle);
            ActualizarTotales();
            ActualizarEstadoBotones();
        }
    }

    private void BtnEmitirRecibo_Click(object? sender, EventArgs e)
    {
        if (_detalleRecibo.Count == 0)
        {
            MessageBox.Show("Debe agregar al menos un producto antes de emitir el recibo.", "Emitir recibo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // Validar método de pago solo si no es pago en efectivo
        if (!chkPagoEfectivo.Checked)
        {
            var metodoSeleccionado = ObtenerOpcionSeleccionada<MetodoPagoDto>(cmbMetodoPago);
            if (metodoSeleccionado is null || metodoSeleccionado.EsPlaceholder || metodoSeleccionado.Valor is null)
            {
                MessageBox.Show("Seleccione un método de pago válido o marque 'Pago en efectivo'.", "Emitir recibo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        var clienteSeleccionado = ObtenerOpcionSeleccionada<ClienteDto>(cmbCliente);
        if (clienteSeleccionado is null || clienteSeleccionado.EsPlaceholder)
        {
            MessageBox.Show("Seleccione el cliente que recibirá la factura o elija \"Cliente ocasional\".", "Emitir recibo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var empleado = _empleadoActualProvider();
        if (empleado is null)
        {
            MessageBox.Show("Debe iniciar sesión para registrar la venta.", "Emitir recibo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // Use cached configuration or load if not available
        var configuracion = _configuracionActual ?? _almacenConfiguracion.Cargar();
        var subtotal = _detalleRecibo.Sum(d => d.Subtotal);
        
        // Calcular impuestos sobre el subtotal original (sin descuento)
        var impuestos = Math.Round(subtotal * configuracion.PorcentajeImpuestos / 100m, 2, MidpointRounding.AwayFromZero);
        
        // Calcular comisión basada en el método de pago seleccionado
        decimal comision = 0m;
        if (!chkPagoEfectivo.Checked)
        {
            var metodoSeleccionado = ObtenerOpcionSeleccionada<MetodoPagoDto>(cmbMetodoPago);
            if (metodoSeleccionado?.Valor is not null)
            {
                var subtotalConImpuestos = subtotal + impuestos;
                comision = Math.Round(subtotalConImpuestos * metodoSeleccionado.Valor.ComisionProveedor / 100m, 2, MidpointRounding.AwayFromZero);
            }
        }
        
        // Calcular descuento por antigüedad del cliente
        decimal descuento = 0m;
        if (clienteSeleccionado.Valor is not null)
        {
            var diasAntiguedad = (DateTime.Now - clienteSeleccionado.Valor.FechaRegistro).Days;
            if (diasAntiguedad >= configuracion.DiasAntiguedadMinima)
            {
                descuento = Math.Round(subtotal * configuracion.PorcentajeDescuentoAntiguedad / 100m, 2, MidpointRounding.AwayFromZero);
            }
        }
        
        // El total es subtotal + impuestos + comision - descuento
        var total = subtotal + impuestos + comision - descuento;

        // Determinar el id del método de pago: null si es efectivo, o el valor del combo si no
        long? idMetodoPago = null;
        if (!chkPagoEfectivo.Checked)
        {
            var metodoSeleccionado = ObtenerOpcionSeleccionada<MetodoPagoDto>(cmbMetodoPago);
            idMetodoPago = metodoSeleccionado?.Valor?.IdMetodoPago;
        }

        var cabecera = new FacturaCabecera
        {
            FechaEmision = DateTime.Now,
            Descuento = descuento,
            Subtotal = subtotal,
            IdEmpleado = empleado.IdEmpleado,
            IdCliente = clienteSeleccionado.Valor?.IdCliente,
            IdMetodoPago = idMetodoPago,
            EstadoVenta = "pagada",
            PorcentajeImpuestos = configuracion.PorcentajeImpuestos
        };

        var detalles = _detalleRecibo
            .Select(d => new FacturaDetalle
            {
                CodigoProducto = d.CodigoProducto,
                CantidadProducto = d.Cantidad
            })
            .ToList();

        try
        {
            var idFactura = _facturaRepository.CrearFactura(cabecera, detalles);

            foreach (var detalle in detalles)
            {
                var producto = _productosDisponibles.FirstOrDefault(p => p.CodigoProducto == detalle.CodigoProducto);
                if (producto is null)
                {
                    continue;
                }

                var stockRestante = Math.Max(0, (int)producto.Stock - detalle.CantidadProducto);
                var nuevoStock = (short)stockRestante;
                if (nuevoStock != producto.Stock)
                {
                    _productoRepository.ActualizarStock(producto.CodigoProducto, nuevoStock);
                    producto.Stock = nuevoStock;
                }
            }

            // En lugar de un mensaje simple, abrir el detalle de la factura recién emitida
            try
            {
                var factura = _facturaRepository.ObtenerFacturaPorId(idFactura);
                if (factura is not null)
                {
                    using var detalleForm = new FrmDetalleFactura(factura);
                    detalleForm.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show(
                        $"Recibo emitido correctamente. ID: {idFactura}.\nTotal cobrado: {total.ToString("C2", CultureInfo.CurrentCulture)}",
                        "Recibo emitido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"El recibo se emitió pero no fue posible abrir el detalle. Detalle: {ex.Message}",
                    "Recibo emitido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"El recibo se emitió pero ocurrió un error al abrir el detalle. Detalle: {ex.Message}",
                    "Recibo emitido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            LimpiarFormulario();
            CargarProductosDisponibles();
        }
        catch (SqlException ex)
        {
            MostrarError("emitir el recibo", ex.Message);
        }
        catch (Exception ex)
        {
            MostrarError("emitir el recibo", ex.Message);
        }
    }

    private void AgregarOActualizarDetalle(ProductoDto producto, short cantidadSeleccionada)
    {
        var existente = _detalleRecibo.FirstOrDefault(d => d.CodigoProducto == producto.CodigoProducto);
        if (existente is not null)
        {
            var nuevaCantidad = (short)(existente.Cantidad + cantidadSeleccionada);
            if (nuevaCantidad > producto.Stock)
            {
                MessageBox.Show("La cantidad seleccionada supera el stock disponible.", "Emitir recibo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            existente.Cantidad = nuevaCantidad;
            _detalleBindingSource.ResetBindings(false);
            return;
        }

        _detalleRecibo.Add(new DetalleReciboViewModel
        {
            CodigoProducto = producto.CodigoProducto,
            Descripcion = producto.Nombre,
            Cantidad = cantidadSeleccionada,
            PrecioUnitario = producto.Precio
        });
    }

    private void LimpiarFormulario()
    {
        _detalleRecibo.Clear();
        _detalleBindingSource.ResetBindings(false);

        if (cmbCliente.Items.Count > 0)
        {
            cmbCliente.SelectedIndex = 0;
        }

        if (cmbMetodoPago.Items.Count > 0)
        {
            cmbMetodoPago.SelectedIndex = 0;
        }

        chkPagoEfectivo.Checked = false;

        InicializarTotales();
        ActualizarEstadoBotones();
    }

    private void ActualizarTotales()
    {
        // Use cached configuration or load if not available
        var configuracion = _configuracionActual ?? _almacenConfiguracion.Cargar();
        var subtotal = _detalleRecibo.Sum(d => d.Subtotal);
        
        // Calcular impuestos sobre el subtotal original (sin descuento)
        var impuestos = Math.Round(subtotal * configuracion.PorcentajeImpuestos / 100m, 2, MidpointRounding.AwayFromZero);
        
        // Calcular comisión basada en el método de pago seleccionado
        decimal comision = 0m;
        if (!chkPagoEfectivo.Checked)
        {
            var metodoSeleccionado = ObtenerOpcionSeleccionada<MetodoPagoDto>(cmbMetodoPago);
            if (metodoSeleccionado?.Valor is not null)
            {
                var subtotalConImpuestos = subtotal + impuestos;
                comision = Math.Round(subtotalConImpuestos * metodoSeleccionado.Valor.ComisionProveedor / 100m, 2, MidpointRounding.AwayFromZero);
            }
        }
        
        // Calcular descuento por antigüedad del cliente
        decimal descuento = 0m;
        var clienteSeleccionado = ObtenerOpcionSeleccionada<ClienteDto>(cmbCliente);
        if (clienteSeleccionado?.Valor is not null)
        {
            var diasAntiguedad = (DateTime.Now - clienteSeleccionado.Valor.FechaRegistro).Days;
            if (diasAntiguedad >= configuracion.DiasAntiguedadMinima)
            {
                descuento = Math.Round(subtotal * configuracion.PorcentajeDescuentoAntiguedad / 100m, 2, MidpointRounding.AwayFromZero);
            }
        }
        
        // El total es subtotal + impuestos + comision - descuento
        var total = subtotal + impuestos + comision - descuento;

        txtSubtotal.Text = subtotal.ToString("C2", CultureInfo.CurrentCulture);
        txtImpuestos.Text = impuestos.ToString("C2", CultureInfo.CurrentCulture);
        txtComision.Text = comision.ToString("C2", CultureInfo.CurrentCulture);
        txtDescuento.Text = descuento.ToString("C2", CultureInfo.CurrentCulture);
        txtTotal.Text = total.ToString("C2", CultureInfo.CurrentCulture);
    }

    private void ActualizarEstadoBotones()
    {
        btnQuitarItem.Enabled = _detalleRecibo.Count > 0;
        btnEmitirRecibo.Enabled = _detalleRecibo.Count > 0;

        var hayStock = _productosDisponibles.Any(p =>
        {
            var existente = _detalleRecibo.FirstOrDefault(d => d.CodigoProducto == p.CodigoProducto);
            var reservado = existente?.Cantidad ?? 0;
            return p.Stock - reservado > 0;
        });

        btnNuevoItem.Enabled = hayStock;
    }

    private List<ProductoVentaDisponible> ObtenerProductosConStockDisponible()
    {
        return _productosDisponibles
            .Select(producto =>
            {
                var existente = _detalleRecibo.FirstOrDefault(d => d.CodigoProducto == producto.CodigoProducto);
                var reservado = existente?.Cantidad ?? 0;
                var stockDisponibleCalculado = Math.Max(0, (int)producto.Stock - reservado);
                var stockDisponible = (short)stockDisponibleCalculado;
                return new ProductoVentaDisponible(producto, stockDisponible);
            })
            .Where(p => p.StockDisponible > 0)
            .ToList();
    }

    private static ComboOpcion<T>? ObtenerOpcionSeleccionada<T>(ComboBox combo)
    {
        return combo.SelectedItem as ComboOpcion<T>;
    }

    private static void MostrarError(string contexto, string mensaje)
    {
        MessageBox.Show($"No fue posible cargar {contexto}. Detalle: {mensaje}", "Emitir recibo",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void BtnGestionarMetodosPago_Click(object? sender, EventArgs e)
    {
        using var form = new FrmGestionMetodosPago(_connectionFactory);
        form.ShowDialog(this);
        
        // Recargar los métodos de pago después de cerrar el formulario de gestión
        CargarMetodosPago();
    }

    private void ChkPagoEfectivo_CheckedChanged(object? sender, EventArgs e)
    {
        // Deshabilitar el combo de método de pago y el botón de gestión cuando se selecciona efectivo
        cmbMetodoPago.Enabled = !chkPagoEfectivo.Checked;
        btnGestionarMetodosPago.Enabled = !chkPagoEfectivo.Checked;
        lblMetodoPago.Enabled = !chkPagoEfectivo.Checked;
        
        // Limpiar la selección del combo cuando se marca efectivo
        if (chkPagoEfectivo.Checked && cmbMetodoPago.Items.Count > 0)
        {
            cmbMetodoPago.SelectedIndex = 0;
        }
        
        // Actualizar totales cuando cambia el método de pago
        ActualizarTotales();
    }

    private void CmbMetodoPago_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // Actualizar totales cuando cambia el método de pago seleccionado
        ActualizarTotales();
    }

    private void CmbCliente_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // Actualizar totales cuando cambia el cliente seleccionado (para aplicar descuento por antigüedad)
        ActualizarTotales();
    }

    private sealed class DetalleReciboViewModel
    {
        public long CodigoProducto { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public short Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal => Math.Round(PrecioUnitario * Cantidad, 2, MidpointRounding.AwayFromZero);
    }

    private sealed class ComboOpcion<T>
    {
        public ComboOpcion(string descripcion, T? valor, bool esPlaceholder = false)
        {
            Descripcion = descripcion;
            Valor = valor;
            EsPlaceholder = esPlaceholder;
        }

        public string Descripcion { get; }
        public T? Valor { get; }
        public bool EsPlaceholder { get; }

        public override string ToString() => Descripcion;
    }
}
