using System.Linq;

namespace MyMarket.Datos.Modelos;

/// <summary>
///     Representa los datos mínimos de un empleado que maneja la aplicación.
///     Se utiliza tanto para autenticación como para gestión de usuarios.
/// </summary>
public class EmpleadoDto
{
    /// <summary>
    ///     Identificador del empleado en la base de datos.
    /// </summary>
    public int IdEmpleado { get; set; }

    /// <summary>
    ///     Número de CUIL/CUIT asociado al empleado.
    /// </summary>
    public string CuilCuit { get; set; } = string.Empty;

    /// <summary>
    ///     Correo electrónico de contacto y usuario de acceso.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///     Indica si el empleado se encuentra habilitado para operar.
    /// </summary>
    public bool Activo { get; set; }

    /// <summary>
    ///     Clave foránea al rol asociado.
    /// </summary>
    public int IdRol { get; set; }

    /// <summary>
    ///     Descripción legible del rol que posee el empleado.
    /// </summary>
    public string RolDescripcion { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre propio del empleado (opcional).
    /// </summary>
    public string? Nombre { get; set; }

    /// <summary>
    ///     Apellido del empleado (opcional).
    /// </summary>
    public string? Apellido { get; set; }

    /// <summary>
    ///     Valor calculado que muestra nombre y apellido cuando existen, o el correo en su defecto.
    /// </summary>
    public string NombreParaMostrar
    {
        get
        {
            var nombreCompuesto = string.Join(" ", new[] { Nombre, Apellido }.Where(s => !string.IsNullOrWhiteSpace(s)));
            return string.IsNullOrWhiteSpace(nombreCompuesto) ? Email : nombreCompuesto;
        }
    }
}
