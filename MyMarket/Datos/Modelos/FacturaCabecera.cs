using System;

namespace MyMarket.Datos.Modelos;

/// <summary>
///     Datos necesarios para generar la cabecera de una factura antes de persistirla.
/// </summary>
public class FacturaCabecera
{
    /// <summary>
    ///     Fecha y hora en la que se emite la factura.
    /// </summary>
    public DateTime FechaEmision { get; set; } = DateTime.Now;

    /// <summary>
    ///     Descuento monetario aplicado al subtotal.
    /// </summary>
    public decimal Descuento { get; set; }

    /// <summary>
    ///     Subtotal antes de impuestos y descuentos.
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    ///     Identificador del empleado que registra la venta.
    /// </summary>
    public int IdEmpleado { get; set; }

    /// <summary>
    ///     Identificador del cliente en caso de ventas registradas.
    /// </summary>
    public long? IdCliente { get; set; }

    /// <summary>
    ///     Identificador del método de pago utilizado para la operación.
    ///     Si es null, se considera pago en efectivo.
    /// </summary>
    public long? IdMetodoPago { get; set; }

    /// <summary>
    ///     Estado actual de la venta (por defecto, pendiente).
    /// </summary>
    public string EstadoVenta { get; set; } = "pendiente";

    /// <summary>
    ///     Alícuota de impuestos aplicada al subtotal.
    /// </summary>
    public byte PorcentajeImpuestos { get; set; } = 21;
}
