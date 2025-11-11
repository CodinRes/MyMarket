namespace MyMarket.Datos.Modelos;

/// <summary>
///     Representa un producto disponible en el catálogo de la tienda.
/// </summary>
public class ProductoDto
{
    /// <summary>
    ///     Identificador del producto.
    /// </summary>
    public long CodigoProducto { get; set; }

    /// <summary>
    ///     Nombre comercial del producto.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    ///     Descripción opcional con más detalle del artículo.
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    ///     Precio de venta actual.
    /// </summary>
    public decimal Precio { get; set; }

    /// <summary>
    ///     Cantidad disponible en stock.
    /// </summary>
    public short Stock { get; set; }

    /// <summary>
    ///     Identificador de la categoría a la que pertenece.
    /// </summary>
    public int IdCategoria { get; set; }

    /// <summary>
    ///     Indica si el producto está activo para la venta.
    /// </summary>
    public bool Estado { get; set; }
}
