namespace MyMarket.Datos.Modelos;

/// <summary>
///     Representa una categoría de productos.
/// </summary>
public class CategoriaDto
{
    /// <summary>
    ///     Identificador de la categoría.
    /// </summary>
    public int IdCategoria { get; set; }

    /// <summary>
    ///     Nombre de la categoría.
    /// </summary>
    public string NombreCategoria { get; set; } = string.Empty;

    /// <summary>
    ///     Indica si la categoría está activa (true) o desactivada (false).
    /// </summary>
    public bool Activo { get; set; } = true;
}
