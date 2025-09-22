using System.Data;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Infraestructura;
using MyMarket.Datos.Modelos;

namespace MyMarket.Datos.Repositorios;

/// <summary>
///     Repositorio encargado de las operaciones básicas sobre productos.
/// </summary>
public class ProductoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    /// <summary>
    ///     Inicializa el repositorio con la fábrica de conexiones.
    /// </summary>
    public ProductoRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    ///     Obtiene todos los productos activos en la base de datos.
    /// </summary>
    public IReadOnlyList<ProductoDto> GetProductosActivos()
    {
        var productos = new List<ProductoDto>();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT codigo_producto, nombre, descripcion, precio, stock, id_categoria, estado
                                 FROM producto
                                 WHERE estado = @estado";
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = true });

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            productos.Add(new ProductoDto
            {
                CodigoProducto = reader.GetInt64(0),
                Nombre = reader.GetString(1),
                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                Precio = reader.GetDecimal(3),
                Stock = reader.GetInt16(4),
                IdCategoria = reader.GetInt32(5),
                Estado = !reader.IsDBNull(6) && reader.GetBoolean(6)
            });
        }

        return productos;
    }

    /// <summary>
    ///     Actualiza el stock disponible para el producto especificado.
    /// </summary>
    public bool ActualizarStock(long codigoProducto, short cantidad)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"UPDATE producto
                                 SET stock = @stock
                                 WHERE codigo_producto = @codigo";
        command.Parameters.Add(new SqlParameter("@stock", SqlDbType.SmallInt) { Value = cantidad });
        command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = codigoProducto });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
    }
}
