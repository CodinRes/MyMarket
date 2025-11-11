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
    public long IdFactura { get; set; }

    /// <summary>
    ///     Identificador del empleado que creó la factura (vendedor).
    /// </summary>
    public int IdEmpleado { get; set; }

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
    ///     Porcentaje de impuestos aplicado sobre el subtotal.
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
    ///     Identificador del método de pago (null = efectivo).
    /// </summary>
    public long? IdMetodoPago { get; set; }

    /// <summary>
    ///     Porcentaje de comisión aplicado por el proveedor del método de pago.
    ///     Se almacena como valor porcentual (ej: 2.5 = 2,5%).
    /// </summary>
    public decimal ComisionProveedor { get; set; }

    /// <summary>
    ///     Importe neto luego de aplicar el descuento (sin impuestos ni comisión).
    ///     Se mantiene por compatibilidad, aunque ya no se usa para el cálculo de impuestos.
    /// </summary>
    public decimal ImporteNeto => Subtotal - Descuento;

    /// <summary>
    ///     Importe de impuestos calculado sobre el subtotal original (sin aplicar descuento).
    /// </summary>
    public decimal ImporteImpuestos => Math.Round(Subtotal * PorcentajeImpuestos / 100m, 2, MidpointRounding.AwayFromZero);

    /// <summary>
    ///     Importe de comisión calculado sobre (subtotal + impuestos) si existe método de pago distinto de efectivo.
    /// </summary>
    public decimal ImporteComision => IdMetodoPago is null ? 0m : Math.Round((Subtotal + ImporteImpuestos) * ComisionProveedor / 100m, 2, MidpointRounding.AwayFromZero);

    /// <summary>
    ///     Total final a pagar: subtotal + impuestos + comisión - descuento.
    /// </summary>
    public decimal Total => Subtotal + ImporteImpuestos + ImporteComision - Descuento;

    /// <summary>
    ///     Formatea el ID de factura con ceros a la izquierda para su visualización.
    /// </summary>
    public string IdFacturaFormateado => IdFactura.ToString("00000000", CultureInfo.InvariantCulture);
}
