namespace MyMarket.Datos.Modelos;

/// <summary>
///     Describe un rol disponible para asignar a los empleados.
/// </summary>
public class RolDto
{
    /// <summary>
    ///     Identificador del rol en la base de datos.
    /// </summary>
    public int IdRol { get; set; }

    /// <summary>
    ///     Nombre descriptivo del rol.
    /// </summary>
    public string Descripcion { get; set; } = string.Empty;

    public override string ToString() => Descripcion;
}
