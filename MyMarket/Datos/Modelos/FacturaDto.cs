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
    public long IdFactura { get; set; }

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
    ///     Identificador del cliente si fue informado.
    /// </summary>
    public long? IdCliente { get; set; }

    /// <summary>
    ///     Documento del cliente si fue informado.
    /// </summary>
    public string? DniCliente { get; set; }

    /// <summary>
    ///     Nombre completo del cliente asociado o una leyenda genérica.
    /// </summary>
    public string ClienteNombreCompleto { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre completo del empleado que emitió la factura.
    /// </summary>
    public string EmpleadoNombreCompleto { get; set; } = string.Empty;

    /// <summary>
    ///     Identificador del método de pago asociado.
    ///     Si es null, el pago se realizó en efectivo.
    /// </summary>
    public long? IdMetodoPago { get; set; }

    /// <summary>
    ///     Referencia del pago asociado.
    /// </summary>
    public long IdentificacionPago { get; set; }

    /// <summary>
    ///     Descripción del método de pago utilizado.
    /// </summary>
    public string MetodoPagoDescripcion { get; set; } = string.Empty;

    /// <summary>
    ///     Estado actual de la factura.
    /// </summary>
    public string EstadoVenta { get; set; } = string.Empty;

    /// <summary>
    ///     Porcentaje de impuestos cargado en la operación.
    /// </summary>
    public byte PorcentajeImpuestos { get; set; }

    /// <summary>
    ///     Comisión del proveedor del método de pago utilizado.
    /// </summary>
    public decimal ComisionProveedor { get; set; }

    /// <summary>
    ///     Colección de renglones asociados a la factura.
    /// </summary>
    public List<FacturaDetalleDto> Detalles { get; set; } = new();
}
