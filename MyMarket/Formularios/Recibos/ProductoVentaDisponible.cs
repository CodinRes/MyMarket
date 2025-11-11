using System;
using System.Globalization;
using MyMarket.Datos.Modelos;

namespace MyMarket.Formularios.Recibos;

/// <summary>
///     Representa un producto listo para ser mostrado en el selector de la venta con su stock disponible.
/// </summary>
internal sealed class ProductoVentaDisponible
{
    public ProductoVentaDisponible(ProductoDto producto, short stockDisponible)
    {
        Producto = producto ?? throw new ArgumentNullException(nameof(producto));
        StockDisponible = stockDisponible;
    }

    /// <summary>
    ///     Producto referenciado en la base de datos.
    /// </summary>
    public ProductoDto Producto { get; }

    /// <summary>
    ///     Stock que puede venderse considerando reservas temporales de la venta en curso.
    /// </summary>
    public short StockDisponible { get; }

    /// <summary>
    ///     Código del producto para mostrar en la grilla.
    /// </summary>
    public long Codigo => Producto.CodigoProducto;

    /// <summary>
    ///     Nombre del producto para mostrar en la grilla.
    /// </summary>
    public string Nombre => Producto.Nombre;

    /// <summary>
    ///     Descripción del producto para mostrar en la grilla.
    /// </summary>
    public string? Descripcion => Producto.Descripcion;

    /// <summary>
    ///     Precio del producto para mostrar en la grilla.
    /// </summary>
    public decimal Precio => Producto.Precio;

    /// <summary>
    ///     Descripción amigable para mostrar en la interfaz (usada en combobox legacy).
    /// </summary>
    public string DescripcionCompleta =>
        $"{Producto.Nombre} - {Producto.Precio.ToString("C2", CultureInfo.CurrentCulture)} (Stock: {StockDisponible})";
}
