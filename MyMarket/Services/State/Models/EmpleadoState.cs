namespace MyMarket.Services.State.Models;

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
