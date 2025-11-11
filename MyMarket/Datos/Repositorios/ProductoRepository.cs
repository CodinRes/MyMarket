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
        command.CommandText = @"SELECT codigo_producto, nombre_producto, descripcion, precio_unitario, stock, id_categoria, estado
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
    ///     Recupera los productos disponibles para la venta considerando stock.
    /// </summary>
    public IReadOnlyList<ProductoDto> GetProductosDisponiblesParaVenta()
    {
        var productos = new List<ProductoDto>();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT codigo_producto, nombre_producto, descripcion, precio_unitario, stock, id_categoria, estado
                                 FROM producto
                                 WHERE estado = @estado
                                   AND stock > 0";
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

    /// <summary>
    ///     Crea un nuevo producto en la base de datos.
    /// </summary>
    public bool CrearProducto(ProductoDto producto)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO producto (codigo_producto, nombre_producto, descripcion, precio_unitario, stock, id_categoria, estado)
                                VALUES (@codigo, @nombre, @descripcion, @precio, @stock, @categoria, @estado)";
        command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = producto.CodigoProducto });
        command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = producto.Nombre });
        command.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.VarChar, 200) { Value = (object?)producto.Descripcion ?? DBNull.Value });
        command.Parameters.Add(new SqlParameter("@precio", SqlDbType.Decimal) { Value = producto.Precio, Precision = 10, Scale = 2 });
        command.Parameters.Add(new SqlParameter("@stock", SqlDbType.SmallInt) { Value = producto.Stock });
        command.Parameters.Add(new SqlParameter("@categoria", SqlDbType.Int) { Value = producto.IdCategoria });
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = producto.Estado });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
    }

    /// <summary>
    ///     Actualiza la información de un producto existente.
    /// </summary>
    public bool ActualizarProducto(ProductoDto producto)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"UPDATE producto
                                 SET nombre_producto = @nombre,
                                     descripcion = @descripcion,
                                     precio_unitario = @precio,
                                     stock = @stock,
                                     id_categoria = @categoria,
                                     estado = @estado
                                 WHERE codigo_producto = @codigo";
        command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = producto.Nombre });
        command.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.VarChar, 200) { Value = (object?)producto.Descripcion ?? DBNull.Value });
        command.Parameters.Add(new SqlParameter("@precio", SqlDbType.Decimal) { Value = producto.Precio, Precision = 10, Scale = 2 });
        command.Parameters.Add(new SqlParameter("@stock", SqlDbType.SmallInt) { Value = producto.Stock });
        command.Parameters.Add(new SqlParameter("@categoria", SqlDbType.Int) { Value = producto.IdCategoria });
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = producto.Estado });
        command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = producto.CodigoProducto });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
    }

    /// <summary>
    ///     Obtiene todos los productos (activos e inactivos) de la base de datos.
    /// </summary>
    public IReadOnlyList<ProductoDto> GetTodosProductos()
    {
        var productos = new List<ProductoDto>();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT codigo_producto, nombre_producto, descripcion, precio_unitario, stock, id_categoria, estado
                                 FROM producto
                                 ORDER BY nombre_producto";

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
    ///     Obtiene todos los productos con el nombre de su categoría para mostrar en grillas.
    /// </summary>
    public IReadOnlyList<ProductoConCategoriaDto> GetProductosConCategoria()
    {
        var productos = new List<ProductoConCategoriaDto>();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT p.codigo_producto, p.nombre_producto, p.descripcion, p.precio_unitario, 
                                       p.stock, p.id_categoria, c.nombre_categoria, p.estado
                                 FROM producto p
                                 INNER JOIN categoria c ON p.id_categoria = c.id_categoria
                                 ORDER BY p.nombre_producto";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            productos.Add(new ProductoConCategoriaDto
            {
                CodigoProducto = reader.GetInt64(0),
                Nombre = reader.GetString(1),
                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                Precio = reader.GetDecimal(3),
                Stock = reader.GetInt16(4),
                IdCategoria = reader.GetInt32(5),
                NombreCategoria = reader.GetString(6),
                Activo = !reader.IsDBNull(7) && reader.GetBoolean(7)
            });
        }

        return productos;
    }

    /// <summary>
    ///     Actualiza el estado (activo/inactivo) de un producto.
    /// </summary>
    public bool ActualizarEstadoProducto(long codigoProducto, bool activo)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"UPDATE producto
                                SET estado = @estado
                                WHERE codigo_producto = @codigo";
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = activo });
        command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = codigoProducto });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
    }
}
