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
        // Se usan los nombres reales de columna del esquema: nombre_producto y precio_unitario
        // y se mantienen los índices que el mapeo espera.
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
    ///     Recupera los productos disponibles para la venta considerando stock y fecha de vencimiento de los lotes.
    /// </summary>
    public IReadOnlyList<ProductoDto> GetProductosDisponiblesParaVenta()
    {
        var productos = new List<ProductoDto>();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        // Alineado con el esquema: se seleccionan las columnas reales y se usan alias para mantener
        // la misma posición/contrato que usa el mapeo en el lector.
        command.CommandText = @"SELECT DISTINCT p.codigo_producto,
                                         p.nombre_producto AS nombre,
                                         p.descripcion,
                                         p.precio_unitario AS precio,
                                         p.stock,
                                         p.id_categoria,
                                         p.estado
                                 FROM producto p
                                 WHERE p.estado = @estado
                                   AND p.stock > 0
                                   AND EXISTS (SELECT 1
                                               FROM lote l
                                               WHERE l.codigo_producto = p.codigo_producto
                                                 AND l.fecha_vencimiento >= CAST(GETDATE() AS DATE))";
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
    ///     Obtiene todos los productos (activos e inactivos).
    /// </summary>
    public IReadOnlyList<ProductoDto> ObtenerTodosLosProductos()
    {
        var productos = new List<ProductoDto>();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT codigo_producto, nombre_producto, descripcion, precio_unitario, stock, id_categoria, estado
                                 FROM producto
                                 ORDER BY codigo_producto";

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
    ///     Inserta un nuevo producto.
    /// </summary>
    public long CrearProducto(ProductoDto producto)
    {
        if (producto is null) throw new ArgumentNullException(nameof(producto));

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO producto (codigo_producto, precio_unitario, nombre_producto, descripcion, stock, estado, id_categoria)
                                 VALUES (@codigo, @precio, @nombre, @descripcion, @stock, @estado, @idCategoria);
                                 SELECT CAST(@codigo AS BIGINT);"; // codigo is provided by caller

        command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = producto.CodigoProducto });
        command.Parameters.Add(new SqlParameter("@precio", SqlDbType.Decimal) { Precision = 10, Scale = 2, Value = producto.Precio });
        command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = producto.Nombre });
        command.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.VarChar, 200) { Value = (object?)producto.Descripcion ?? DBNull.Value });
        command.Parameters.Add(new SqlParameter("@stock", SqlDbType.SmallInt) { Value = producto.Stock });
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = producto.Estado });
        command.Parameters.Add(new SqlParameter("@idCategoria", SqlDbType.Int) { Value = producto.IdCategoria });

        // Ejecutar y devolver el código (en este esquema el código lo provee el caller)
        var res = command.ExecuteScalar();
        return producto.CodigoProducto;
    }

    /// <summary>
    ///     Actualiza un producto existente.
    /// </summary>
    public bool ActualizarProducto(ProductoDto producto)
    {
        if (producto is null) throw new ArgumentNullException(nameof(producto));

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"UPDATE producto
                                 SET precio_unitario = @precio,
                                     nombre_producto = @nombre,
                                     descripcion = @descripcion,
                                     stock = @stock,
                                     estado = @estado,
                                     id_categoria = @idCategoria
                                 WHERE codigo_producto = @codigo";

        command.Parameters.Add(new SqlParameter("@precio", SqlDbType.Decimal) { Precision = 10, Scale = 2, Value = producto.Precio });
        command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = producto.Nombre });
        command.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.VarChar, 200) { Value = (object?)producto.Descripcion ?? DBNull.Value });
        command.Parameters.Add(new SqlParameter("@stock", SqlDbType.SmallInt) { Value = producto.Stock });
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = producto.Estado });
        command.Parameters.Add(new SqlParameter("@idCategoria", SqlDbType.Int) { Value = producto.IdCategoria });
        command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = producto.CodigoProducto });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
    }

    /// <summary>
    ///     Cambia el estado (activo/inactivo) del producto.
    /// </summary>
    public bool CambiarEstadoProducto(long codigoProducto, bool activo)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "UPDATE producto SET estado = @estado WHERE codigo_producto = @codigo";
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = activo });
        command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = codigoProducto });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
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
    ///     Elimina definitivamente un producto de la base de datos.
    /// </summary>
    public bool EliminarProducto(long codigoProducto)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM producto WHERE codigo_producto = @codigo";
        command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = codigoProducto });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
    }
}
