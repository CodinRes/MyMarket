namespace MyMarket.Services.Models;

public class AppState
{
    public EmpleadoState? SesionActiva { get; set; }
    public string? EstadoVentana { get; set; }
}

public class EmpleadoState
{
    public int IdEmpleado { get; set; }
    public string CuilCuit { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public int IdRol { get; set; }
    public string RolDescripcion { get; set; } = string.Empty;
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
}
