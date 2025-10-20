using System.Data;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Infraestructura;
using MyMarket.Datos.Modelos;

namespace MyMarket.Datos.Repositorios;

/// <summary>
///     Repositorio encargado de las operaciones básicas sobre categorías.
/// </summary>
public class CategoriaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    /// <summary>
    ///     Inicializa el repositorio con la fábrica de conexiones.
    /// </summary>
    public CategoriaRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    ///     Obtiene todas las categorías de la base de datos.
    /// </summary>
    public IReadOnlyList<CategoriaDto> ObtenerCategorias()
    {
        var categorias = new List<CategoriaDto>();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT id_categoria, nombre_categoria
                                 FROM categoria
                                 ORDER BY nombre_categoria";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            categorias.Add(new CategoriaDto
            {
                IdCategoria = reader.GetInt32(0),
                NombreCategoria = reader.GetString(1)
            });
        }

        return categorias;
    }

    /// <summary>
    ///     Crea una nueva categoría en la base de datos.
    /// </summary>
    public bool CrearCategoria(string nombreCategoria)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO categoria (nombre_categoria)
                                VALUES (@nombre)";
        command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = nombreCategoria });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
    }

    /// <summary>
    ///     Elimina una categoría de la base de datos si no está asociada a productos.
    /// </summary>
    public bool EliminarCategoria(int idCategoria)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"DELETE FROM categoria
                                WHERE id_categoria = @id";
        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = idCategoria });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
    }
}
