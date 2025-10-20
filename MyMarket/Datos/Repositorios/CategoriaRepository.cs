using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Infraestructura;

namespace MyMarket.Datos.Repositorios;

public class CategoriaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public CategoriaRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IReadOnlyDictionary<int, string> ObtenerCategorias()
    {
        var dict = new Dictionary<int, string>();
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT id_categoria, nombre_categoria FROM categoria";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var nombre = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
            dict[id] = nombre;
        }

        return dict;
    }
}
