using System.Linq;

namespace MyMarket.Datos.Modelos;

public class EmpleadoDto
{
    public int IdEmpleado { get; set; }
    public string CuilCuit { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public int IdRol { get; set; }
    public string RolDescripcion { get; set; } = string.Empty;
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }

    public string NombreParaMostrar
    {
        get
        {
            var nombreCompuesto = string.Join(" ", new[] { Nombre, Apellido }.Where(s => !string.IsNullOrWhiteSpace(s)));
            return string.IsNullOrWhiteSpace(nombreCompuesto) ? Email : nombreCompuesto;
        }
    }
}
