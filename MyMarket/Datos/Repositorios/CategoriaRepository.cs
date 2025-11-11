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
    ///     Obtiene todas las categorías activas de la base de datos.
    /// </summary>
    public IReadOnlyList<CategoriaDto> ObtenerCategorias()
    {
        return ObtenerCategorias(true);
    }

    /// <summary>
    ///     Obtiene categorías de la base de datos filtradas por estado.
    /// </summary>
    public IReadOnlyList<CategoriaDto> ObtenerCategorias(bool soloActivas)
    {
        var categorias = new List<CategoriaDto>();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        
        if (soloActivas)
        {
            command.CommandText = @"SELECT id_categoria, nombre_categoria, estado
                                     FROM categoria
                                     WHERE estado = 1
                                     ORDER BY nombre_categoria";
        }
        else
        {
            command.CommandText = @"SELECT id_categoria, nombre_categoria, estado
                                     FROM categoria
                                     ORDER BY nombre_categoria";
        }

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            categorias.Add(new CategoriaDto
            {
                IdCategoria = reader.GetInt32(0),
                NombreCategoria = reader.GetString(1),
                Activo = reader.GetBoolean(2)
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
    ///     Desactiva lógicamente una categoría en lugar de eliminarla físicamente.
    /// </summary>
    public bool DesactivarCategoria(int idCategoria)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"UPDATE categoria
                                SET estado = 0
                                WHERE id_categoria = @id";
        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = idCategoria });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
    }

    /// <summary>
    ///     Activa una categoría previamente desactivada.
    /// </summary>
    public bool ActivarCategoria(int idCategoria)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"UPDATE categoria
                                SET estado = 1
                                WHERE id_categoria = @id";
        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = idCategoria });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
    }
}
