namespace MyMarket.Datos.Modelos;

/// <summary>
///     Representa un renglón de la factura con el producto vendido y la cantidad.
/// </summary>
public class FacturaDetalle
{
    /// <summary>
    ///     Clave del producto incluido en la factura.
    /// </summary>
    public long CodigoProducto { get; set; }

    /// <summary>
    ///     Cantidad de unidades vendidas para el producto.
    /// </summary>
    public short CantidadProducto { get; set; }
}
