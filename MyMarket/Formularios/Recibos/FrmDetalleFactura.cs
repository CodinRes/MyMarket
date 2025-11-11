using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MyMarket.Datos.Modelos;

namespace MyMarket.Formularios.Recibos;

/// <summary>
///     Presenta un comprobante de factura con un formato similar al de un recibo tradicional.
/// </summary>
public partial class FrmDetalleFactura : Form
{
    private readonly FacturaDto _factura;

    public FrmDetalleFactura(FacturaDto factura)
    {
        _factura = factura ?? throw new ArgumentNullException(nameof(factura));

        InitializeComponent();

        dgvDetalle.AutoGenerateColumns = false;
        dgvDetalle.DataSource = bindingSourceDetalle;

        ConfigurarGrilla();

        Load += FrmDetalleFactura_Load;
    }

    private void FrmDetalleFactura_Load(object? sender, EventArgs e)
    {
        CargarEncabezado();
        CargarDetalle();
        CargarTotales();
    }

    private void CargarEncabezado()
    {
        lblNumeroFactura.Text = $"Factura #{_factura.IdFactura:00000000}";
        lblFechaEmision.Text = _factura.FechaEmision.ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture);
        lblCliente.Text = string.IsNullOrWhiteSpace(_factura.ClienteNombreCompleto)
            ? "Cliente ocasional"
            : _factura.ClienteNombreCompleto;
        lblDni.Text = string.IsNullOrWhiteSpace(_factura.DniCliente) ? "No informado" : _factura.DniCliente;
        lblEmpleado.Text = string.IsNullOrWhiteSpace(_factura.EmpleadoNombreCompleto)
            ? _factura.IdEmpleado.ToString(CultureInfo.InvariantCulture)
            : _factura.EmpleadoNombreCompleto;

        var metodoPago = string.IsNullOrWhiteSpace(_factura.MetodoPagoDescripcion)
            ? _factura.IdentificacionPago.ToString(CultureInfo.InvariantCulture)
            : _factura.MetodoPagoDescripcion;
        lblMetodoPago.Text = metodoPago;
        lblEstado.Text = _factura.EstadoVenta;
    }

    private void CargarDetalle()
    {
        var detalles = _factura.Detalles
            .Select(d => new DetalleFacturaViewModel
            {
                Producto = string.IsNullOrWhiteSpace(d.NombreProducto)
                    ? $"Producto #{d.CodigoProducto}"
                    : d.NombreProducto,
                Cantidad = d.CantidadProducto,
                PrecioUnitario = d.PrecioUnitario
            })
            .ToList();

        bindingSourceDetalle.DataSource = new BindingList<DetalleFacturaViewModel>(detalles);
    }

    private void CargarTotales()
    {
        var subtotal = _factura.Subtotal;
        var descuento = _factura.Descuento;
        
        // Calcular impuestos sobre el subtotal original (sin descuento)
        var impuestos = Math.Round(subtotal * _factura.PorcentajeImpuestos / 100m, 2,
            MidpointRounding.AwayFromZero);
        var importeConImpuestos = subtotal + impuestos;
        
        // Calcular comisión sobre el subtotal con impuestos (sin descuento)
        var comision = Math.Round(importeConImpuestos * _factura.ComisionProveedor / 100m, 2,
            MidpointRounding.AwayFromZero);
        
        // El total es subtotal + impuestos + comision - descuento
        var total = subtotal + impuestos + comision - descuento;

        lblSubtotalValor.Text = subtotal.ToString("C2", CultureInfo.CurrentCulture);
        lblDescuentoValor.Text = descuento.ToString("C2", CultureInfo.CurrentCulture);
        lblComisionValor.Text = $"{comision.ToString("C2", CultureInfo.CurrentCulture)} ({_factura.ComisionProveedor}%)";
        lblImpuestosValor.Text = $"{impuestos.ToString("C2", CultureInfo.CurrentCulture)} ({_factura.PorcentajeImpuestos}%)";
        lblTotalValor.Text = total.ToString("C2", CultureInfo.CurrentCulture);
    }

    private void ConfigurarGrilla()
    {
        dgvDetalle.Columns.Clear();

        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(DetalleFacturaViewModel.Producto),
            HeaderText = "Producto",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            FillWeight = 50,
            MinimumWidth = 180,
            ReadOnly = true
        });

        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(DetalleFacturaViewModel.Cantidad),
            HeaderText = "Cant.",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
            ReadOnly = true
        });

        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(DetalleFacturaViewModel.PrecioUnitario),
            HeaderText = "Precio Unitario",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "C2",
                FormatProvider = CultureInfo.CurrentCulture
            },
            ReadOnly = true
        });

        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(DetalleFacturaViewModel.Subtotal),
            HeaderText = "Subtotal",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "C2",
                FormatProvider = CultureInfo.CurrentCulture
            },
            ReadOnly = true
        });
    }

    private sealed class DetalleFacturaViewModel
    {
        public string Producto { get; init; } = string.Empty;

        public short Cantidad { get; init; }

        public decimal PrecioUnitario { get; init; }

        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
