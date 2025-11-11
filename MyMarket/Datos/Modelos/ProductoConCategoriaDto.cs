namespace MyMarket.Datos.Modelos;

/// <summary>
///     ViewModel que representa un producto con el nombre de su categoría para mostrar en grillas.
/// </summary>
public class ProductoConCategoriaDto
{
    public long CodigoProducto { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public short Stock { get; set; }
    public int IdCategoria { get; set; }
    public string NombreCategoria { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
}
