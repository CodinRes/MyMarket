using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Infraestructura;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;

namespace MyMarket.Formularios.Recibos;

/// <summary>
///     Pantalla que lista recibos emitidos recientemente.
///     Usa data binding y solicita datos al repositorio.
/// </summary>
public partial class FrmRecibosEmitidos : Form
{
    private readonly SqlConnectionFactory? _connectionFactory;
    private readonly FacturaRepository? _facturaRepository;
    private readonly BindingSource _bindingSource = new();
    private readonly Func<EmpleadoDto?>? _empleadoActualProvider;

    // Modelo simple para enlazar a la grilla
    private sealed class ReciboViewModel
    {
        public long Codigo { get; init; }
        public DateTime Fecha { get; init; }
        public string Cliente { get; init; } = string.Empty;
        public decimal Total { get; init; }
        public string TotalFormato => Total.ToString("C2", CultureInfo.CurrentCulture);
    }

    // Constructor runtime que carga desde BD y necesita el empleado actual para aplicar filtros por rol
    public FrmRecibosEmitidos(SqlConnectionFactory connectionFactory, Func<EmpleadoDto?> empleadoActualProvider)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        _facturaRepository = new FacturaRepository(connectionFactory);
        _empleadoActualProvider = empleadoActualProvider ?? throw new ArgumentNullException(nameof(empleadoActualProvider));

        InitializeComponent();
        BtnSetup();
    }

    // Constructor para diseñador (no carga datos)
    public FrmRecibosEmitidos()
    {
        InitializeComponent();
        BtnSetup();
    }

    private void BtnSetup()
    {
        btnVerDetalle.Click += BtnVerDetalle_Click;
        Load += FrmRecibosEmitidos_Load;
        ConfigureGridBinding();
    }

    private void ConfigureGridBinding()
    {
        dgv.AutoGenerateColumns = false;
        dgv.Columns.Clear();

        // Evitar que el usuario cambie el alto de las filas
        dgv.AllowUserToResizeRows = false;
        dgv.RowHeadersVisible = false;
        dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        dgv.RowTemplate.Height = 24;

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Codigo",
            HeaderText = "Código",
            DataPropertyName = nameof(ReciboViewModel.Codigo),
            ReadOnly = true
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Fecha",
            HeaderText = "Fecha",
            DataPropertyName = nameof(ReciboViewModel.Fecha),
            ReadOnly = true,
            DefaultCellStyle = { Format = "g" }
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Cliente",
            HeaderText = "Cliente / DNI",
            DataPropertyName = nameof(ReciboViewModel.Cliente),
            ReadOnly = true
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Total",
            HeaderText = "Total",
            DataPropertyName = nameof(ReciboViewModel.Total),
            ReadOnly = true,
            DefaultCellStyle = { Format = "C2" }
        });

        dgv.DataSource = _bindingSource;
    }

    private void FrmRecibosEmitidos_Load(object? sender, EventArgs e)
    {
        if (_facturaRepository is null || _empleadoActualProvider is null)
        {
            MessageBox.Show("No hay conexión a la base de datos o no hay sesión. La lista de recibos no estará disponible.", "Recibos emitidos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnVerDetalle.Enabled = false;
            return;
        }

        CargarRecibosDesdeRepositorio();
    }

    private void CargarRecibosDesdeRepositorio()
    {
        try
        {
            var empleado = _empleadoActualProvider?.Invoke();
            if (empleado is null)
            {
                MessageBox.Show("Debe iniciar sesión para ver los recibos.", "Recibos emitidos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _bindingSource.DataSource = new List<ReciboViewModel>();
                btnVerDetalle.Enabled = false;
                return;
            }

            var rolNormalizado = (empleado.RolDescripcion ?? string.Empty).Trim().ToLowerInvariant();

            // Gerentes y administradores ven todas las facturas; los demás (ej. cajero) sólo las suyas.
            int? filtroEmpleado = null;
            if (!(rolNormalizado.Contains("gerente") || rolNormalizado.Contains("admin")))
            {
                // No es gerente/admin -> mostrar solo facturas del empleado logueado (cajero/vendedor/etc.)
                filtroEmpleado = empleado.IdEmpleado;
            }

            var facturas = _facturaRepository!.ObtenerFacturasRecientes(100, filtroEmpleado).ToList();
            var modelos = facturas.Select(f =>
            {
                var impuestos = Math.Round(f.Subtotal * f.PorcentajeImpuestos / 100m, 2, MidpointRounding.AwayFromZero);
                var total = f.Subtotal + impuestos;
                var cliente = string.IsNullOrWhiteSpace(f.DniCliente) ? "(ocasional)" : f.DniCliente!;
                return new ReciboViewModel
                {
                    Codigo = f.CodigoFactura,
                    Fecha = f.FechaEmision,
                    Cliente = cliente,
                    Total = total
                };
            }).ToList();

            _bindingSource.DataSource = modelos;
            btnVerDetalle.Enabled = modelos.Count > 0;
            if (modelos.Count == 0)
            {
                MessageBox.Show("No hay recibos recientes para mostrar.", "Recibos emitidos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show($"No fue posible obtener los recibos desde la base. Detalle: {ex.Message}", "Recibos emitidos",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnVerDetalle.Enabled = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error cargando recibos: {ex.Message}", "Recibos emitidos",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnVerDetalle.Enabled = false;
        }
    }

    private void BtnVerDetalle_Click(object? sender, EventArgs e)
    {
        if (dgv.CurrentRow is null)
        {
            MessageBox.Show("Seleccione un recibo para ver el detalle.", "Detalle de recibos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var valor = dgv.CurrentRow.Cells["Codigo"].Value;
        if (valor is null || !long.TryParse(valor.ToString(), out var codigoFactura))
        {
            MessageBox.Show("Número de recibo inválido.", "Detalle de recibos",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_facturaRepository is null)
        {
            MessageBox.Show($"Detalle del recibo {codigoFactura} (prototipo: no hay conexión a base de datos).", "Detalle de recibos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            var factura = _facturaRepository.ObtenerFacturaPorCodigo(codigoFactura);
            if (factura is null)
            {
                MessageBox.Show($"No se encontró la factura {codigoFactura}.", "Detalle de recibos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var subtotal = factura.Subtotal;
            var impuestos = Math.Round(subtotal * factura.PorcentajeImpuestos / 100m, 2, MidpointRounding.AwayFromZero);
            var total = subtotal + impuestos;

            var detallesTexto = factura.Detalles.Any()
                ? string.Join(Environment.NewLine, factura.Detalles.Select(d => $"- Producto {d.CodigoProducto}: {d.CantidadProducto} u."))
                : "(sin renglones)";

            var cliente = string.IsNullOrWhiteSpace(factura.DniCliente) ? "(ocasional)" : factura.DniCliente;

            var mensaje = $"Factura {factura.CodigoFactura}\nFecha: {factura.FechaEmision:g}\nCliente/DNI: {cliente}\n\nDetalles:\n{detallesTexto}\n\nSubtotal: {subtotal.ToString("C2", CultureInfo.CurrentCulture)}\nImpuestos ({factura.PorcentajeImpuestos}%): {impuestos.ToString("C2", CultureInfo.CurrentCulture)}\nTotal: {total.ToString("C2", CultureInfo.CurrentCulture)}";

            MessageBox.Show(mensaje, "Detalle de recibos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (SqlException ex)
        {
            MessageBox.Show($"No fue posible obtener el detalle. Detalle: {ex.Message}", "Detalle de recibos",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al obtener detalle: {ex.Message}", "Detalle de recibos",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
