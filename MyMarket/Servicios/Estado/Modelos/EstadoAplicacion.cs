namespace MyMarket.Servicios.Estado.Modelos;

/// <summary>
///     Representa la información persistida del estado de la aplicación.
/// </summary>
public class EstadoAplicacion
{
    /// <summary>
    ///     Información sobre el empleado autenticado al cerrar la aplicación.
    /// </summary>
    public EstadoEmpleado? SesionActiva { get; set; }

    /// <summary>
    ///     Último estado de la ventana principal (maximizada, normal, etc.).
    /// </summary>
    public string? EstadoVentana { get; set; }
}
