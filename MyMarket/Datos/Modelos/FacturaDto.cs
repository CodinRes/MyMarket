using System;
using System.Collections.Generic;

namespace MyMarket.Datos.Modelos;

/// <summary>
///     Representa una factura completa obtenida desde la base de datos, incluyendo sus detalles.
/// </summary>
public class FacturaDto
{
    /// <summary>
    ///     Identificador único de la factura.
    /// </summary>
    public long CodigoFactura { get; set; }

    /// <summary>
    ///     Fecha de emisión registrada en la cabecera.
    /// </summary>
    public DateTime FechaEmision { get; set; }

    /// <summary>
    ///     Importe total de descuento aplicado.
    /// </summary>
    public decimal Descuento { get; set; }

    /// <summary>
    ///     Subtotal antes de impuestos.
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    ///     Identificador del empleado que generó la venta.
    /// </summary>
    public int IdEmpleado { get; set; }

    /// <summary>
    ///     Documento del cliente si fue informado.
    /// </summary>
    public string? DniCliente { get; set; }

    /// <summary>
    ///     Referencia del pago asociado.
    /// </summary>
    public long IdentificacionPago { get; set; }

    /// <summary>
    ///     Estado actual de la factura.
    /// </summary>
    public string EstadoVenta { get; set; } = string.Empty;

    /// <summary>
    ///     Porcentaje de impuestos cargado en la operación.
    /// </summary>
    public byte PorcentajeImpuestos { get; set; }

    /// <summary>
    ///     Colección de renglones asociados a la factura.
    /// </summary>
    public List<FacturaDetalleDto> Detalles { get; set; } = new();
}
