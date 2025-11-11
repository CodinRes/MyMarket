namespace MyMarket.Servicios.Estado.Modelos;

/// <summary>
///     Modelo simplificado del empleado autenticado para persistir el estado.
/// </summary>
public class EstadoEmpleado
{
    /// <summary>
    ///     Identificador del empleado.
    /// </summary>
    public int IdEmpleado { get; set; }

    /// <summary>
    ///     Número de CUIL/CUIT.
    /// </summary>
    public string CuilCuit { get; set; } = string.Empty;

    /// <summary>
    ///     Correo electrónico del empleado.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///     Estado de actividad del usuario al momento de guardar.
    /// </summary>
    public bool Activo { get; set; }

    /// <summary>
    ///     Rol asignado.
    /// </summary>
    public int IdRol { get; set; }

    /// <summary>
    ///     Descripción textual del rol.
    /// </summary>
    public string RolDescripcion { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre propio (opcional).
    /// </summary>
    public string? Nombre { get; set; }

    /// <summary>
    ///     Apellido (opcional).
    /// </summary>
    public string? Apellido { get; set; }
}
