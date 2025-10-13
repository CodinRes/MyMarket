using System;
using System.Globalization;

namespace MyMarket.Datos.Modelos;

/// <summary>
///     Representa la información principal de una factura para su visualización en listados.
/// </summary>
public class FacturaListadoDto
{
    /// <summary>
    ///     Identificador único de la factura.
    /// </summary>
    public long CodigoFactura { get; set; }

    /// <summary>
    ///     Fecha y hora de emisión del comprobante.
    /// </summary>
    public DateTime FechaEmision { get; set; }

    /// <summary>
    ///     Importe bruto antes de descuentos e impuestos.
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    ///     Descuento monetario aplicado al subtotal.
    /// </summary>
    public decimal Descuento { get; set; }

    /// <summary>
    ///     Porcentaje de impuestos aplicado sobre el importe neto.
    /// </summary>
    public byte PorcentajeImpuestos { get; set; }

    /// <summary>
    ///     Estado actual registrado en la base de datos.
    /// </summary>
    public string EstadoVenta { get; set; } = string.Empty;

    /// <summary>
    ///     DNI del cliente asociado, si corresponde.
    /// </summary>
    public string? DniCliente { get; set; }

    /// <summary>
    ///     Nombre completo del cliente o una leyenda genérica si es ocasional.
    /// </summary>
    public string ClienteNombreCompleto { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre completo del empleado que realizó la venta.
    /// </summary>
    public string EmpleadoNombreCompleto { get; set; } = string.Empty;

    /// <summary>
    ///     Importe neto luego de aplicar los descuentos.
    /// </summary>
    public decimal ImporteNeto => Subtotal - Descuento;

    /// <summary>
    ///     Importe de impuestos calculado según el porcentaje configurado.
    /// </summary>
    public decimal ImporteImpuestos => Math.Round(ImporteNeto * PorcentajeImpuestos / 100m, 2,
        MidpointRounding.AwayFromZero);

    /// <summary>
    ///     Total final a pagar luego de impuestos.
    /// </summary>
    public decimal Total => ImporteNeto + ImporteImpuestos;

    /// <summary>
    ///     Formatea el código de factura con ceros a la izquierda para su visualización.
    /// </summary>
    public string CodigoFormateado => CodigoFactura.ToString("00000000", CultureInfo.InvariantCulture);
}
