using System.Configuration;
using Microsoft.Data.SqlClient;

namespace MyMarket.Data.Infrastructure;

public class SqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory()
    {
        var settings = ConfigurationManager.ConnectionStrings["TiendaDb"]
                       ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'TiendaDb'.");

        if (string.IsNullOrWhiteSpace(settings.ConnectionString))
        {
            throw new InvalidOperationException("La cadena de conexión 'TiendaDb' no puede estar vacía.");
        }

        _connectionString = settings.ConnectionString;
    }

    public SqlConnection CreateOpenConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
