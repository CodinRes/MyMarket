using System.Configuration;
using Microsoft.Data.SqlClient;

namespace MyMarket.Datos.Infraestructura;

/// <summary>
///     Fabrica conexiones SQL Server a partir de la cadena configurada en <c>App.config</c>.
///     Centralizar la creación permite reutilizar la configuración y validar los parámetros.
/// </summary>
public class SqlConnectionFactory
{
    private readonly string _connectionString;

    /// <summary>
    ///     Inicializa la fábrica leyendo la cadena de conexión <c>TiendaDb</c>.
    ///     Lanza una excepción si la configuración es inexistente o está vacía.
    /// </summary>
    public SqlConnectionFactory()
    {
        // Obtiene la cadena de conexión y valida que exista y contenga un valor.
        var settings = ConfigurationManager.ConnectionStrings["TiendaDb"]
                       ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'TiendaDb'.");

        if (string.IsNullOrWhiteSpace(settings.ConnectionString))
        {
            throw new InvalidOperationException("La cadena de conexión 'TiendaDb' no puede estar vacía.");
        }

        _connectionString = settings.ConnectionString;
    }

    /// <summary>
    ///     Crea una nueva instancia de <see cref="SqlConnection"/> y la abre antes de devolverla.
    /// </summary>
    /// <returns>Conexión abierta lista para ejecutar comandos.</returns>
    public SqlConnection CreateOpenConnection()
    {
        // Cada repositorio obtiene una conexión lista para usarse.
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
