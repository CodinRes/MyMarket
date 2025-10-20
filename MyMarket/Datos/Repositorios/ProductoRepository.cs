using System;
using System.Collections.Generic;
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
    ///     Recupera los productos disponibles para la venta considerando stock y fecha de vencimiento de los lotes.
    /// </summary>
    public IReadOnlyList<ProductoDto> GetProductosDisponiblesParaVenta()
    {
        var productos = new List<ProductoDto>();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT DISTINCT p.codigo_producto, p.nombre_producto, p.descripcion, p.precio_unitario, p.stock, p.id_categoria, p.estado
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
    ///     Verifica si existe un producto con el codigo indicado.
    /// </summary>
    public bool ExisteProductoPorCodigo(long codigoProducto)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM producto WHERE codigo_producto = @codigo";
        command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = codigoProducto });

        var resultado = command.ExecuteScalar();
        return Convert.ToInt32(resultado) > 0;
    }

    public void CrearProducto(ProductoDto producto)
    {
        if (producto is null) throw new ArgumentNullException(nameof(producto));

        // Verificar existencia previa para evitar violación de PK
        if (ExisteProductoPorCodigo(producto.CodigoProducto))
        {
            throw new InvalidOperationException("Ya existe un producto con el código especificado.");
        }

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO producto (codigo_producto, precio_unitario, nombre_producto, descripcion, stock, estado, id_categoria)
                                 VALUES (@codigo, @precio, @nombre, @descripcion, @stock, @estado, @idCategoria)";

        command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = producto.CodigoProducto });

        var paramPrecio = new SqlParameter("@precio", SqlDbType.Decimal) { Value = producto.Precio };
        paramPrecio.Precision = 10;
        paramPrecio.Scale = 2;
        command.Parameters.Add(paramPrecio);

        command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = producto.Nombre });
        command.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.VarChar, 200) { Value = (object?)producto.Descripcion ?? DBNull.Value });
        command.Parameters.Add(new SqlParameter("@stock", SqlDbType.SmallInt) { Value = producto.Stock });
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = producto.Estado });
        command.Parameters.Add(new SqlParameter("@idCategoria", SqlDbType.Int) { Value = producto.IdCategoria });

        command.ExecuteNonQuery();
    }

    public void ActualizarProducto(ProductoDto producto)
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

        var paramPrecio = new SqlParameter("@precio", SqlDbType.Decimal) { Value = producto.Precio };
        paramPrecio.Precision = 10;
        paramPrecio.Scale = 2;
        command.Parameters.Add(paramPrecio);

        command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = producto.Nombre });
        command.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.VarChar, 200) { Value = (object?)producto.Descripcion ?? DBNull.Value });
        command.Parameters.Add(new SqlParameter("@stock", SqlDbType.SmallInt) { Value = producto.Stock });
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = producto.Estado });
        command.Parameters.Add(new SqlParameter("@idCategoria", SqlDbType.Int) { Value = producto.IdCategoria });
        command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = producto.CodigoProducto });

        var filas = command.ExecuteNonQuery();
        if (filas == 0)
        {
            throw new InvalidOperationException("No se encontró el producto especificado.");
        }
    }

    /// <summary>
    ///     Aumenta el stock de forma atómica hasta un máximo y devuelve el nuevo valor de stock.
    /// </summary>
    public short IncrementarStock(long codigoProducto, short delta, short maxStock)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();

        // Actualiza el stock de forma atómica y devuelve el stock resultante
        command.CommandText = @"UPDATE producto
                            SET stock = CASE WHEN stock + @delta > @max THEN @max ELSE stock + @delta END
                            WHERE codigo_producto = @codigo;
                            SELECT stock FROM producto WHERE codigo_producto = @codigo;";

        command.Parameters.Add(new SqlParameter("@delta", SqlDbType.SmallInt) { Value = delta });
        command.Parameters.Add(new SqlParameter("@max", SqlDbType.SmallInt) { Value = maxStock });
        command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = codigoProducto });

        var result = command.ExecuteScalar();
        if (result is null)
        {
            throw new InvalidOperationException("No se encontró el producto especificado.");
        }

        return Convert.ToInt16(result);
    }
}
