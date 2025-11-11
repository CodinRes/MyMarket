namespace MyMarket.Datos.Modelos;

/// <summary>
///     DTO que representa un detalle ya persistido e identificado por una factura.
/// </summary>
public class FacturaDetalleDto
{
    /// <summary>
    ///     Identificador de la factura a la que pertenece el detalle.
    /// </summary>
    public long IdFactura { get; set; }

    /// <summary>
    ///     Código del producto vendido.
    /// </summary>
    public long CodigoProducto { get; set; }

    /// <summary>
    ///     Cantidad facturada.
    /// </summary>
    public short CantidadProducto { get; set; }

    /// <summary>
    ///     Descripción del producto facturado.
    /// </summary>
    public string NombreProducto { get; set; } = string.Empty;

    /// <summary>
    ///     Precio unitario aplicado al momento de la venta.
    /// </summary>
    public decimal PrecioUnitario { get; set; }
}
