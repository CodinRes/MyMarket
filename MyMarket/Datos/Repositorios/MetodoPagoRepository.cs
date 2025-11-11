using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Infraestructura;
using MyMarket.Datos.Modelos;

namespace MyMarket.Datos.Repositorios;

/// <summary>
///     Permite recuperar los métodos de pago disponibles para registrar ventas.
/// </summary>
public class MetodoPagoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public MetodoPagoRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    /// <summary>
    ///     Devuelve todos los métodos de pago activos ordenados por proveedor.
    /// </summary>
    public IReadOnlyList<MetodoPagoDto> ObtenerMetodosActivos()
    {
        var metodos = new List<MetodoPagoDto>();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT id_metodo_pago, identificacion_pago, proveedor_pago, comision_proveedor, estado
                                 FROM metodo_pago_detalle
                                 WHERE estado = @estado
                                 ORDER BY proveedor_pago";
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = true });

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            metodos.Add(new MetodoPagoDto
            {
                IdMetodoPago = reader.GetInt64(0),
                IdentificacionPago = reader.GetInt64(1),
                ProveedorPago = reader.GetString(2),
                ComisionProveedor = reader.GetDecimal(3),
                Activo = reader.GetBoolean(4)
            });
        }

        return metodos;
    }

    /// <summary>
    ///     Devuelve todos los métodos de pago (activos e inactivos) ordenados por proveedor.
    /// </summary>
    public IReadOnlyList<MetodoPagoDto> ObtenerTodosMetodos()
    {
        var metodos = new List<MetodoPagoDto>();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT id_metodo_pago, identificacion_pago, proveedor_pago, comision_proveedor, estado
                                 FROM metodo_pago_detalle
                                 ORDER BY proveedor_pago";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            metodos.Add(new MetodoPagoDto
            {
                IdMetodoPago = reader.GetInt64(0),
                IdentificacionPago = reader.GetInt64(1),
                ProveedorPago = reader.GetString(2),
                ComisionProveedor = reader.GetDecimal(3),
                Activo = reader.GetBoolean(4)
            });
        }

        return metodos;
    }

    /// <summary>
    ///     Crea un nuevo método de pago.
    /// </summary>
    public long CrearMetodoPago(long identificacionPago, string proveedorPago, decimal comisionProveedor)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO metodo_pago_detalle (identificacion_pago, proveedor_pago, comision_proveedor, estado)
                                VALUES (@identificacion, @proveedor, @comision, 1);
                                SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";
        
        command.Parameters.Add(new SqlParameter("@identificacion", SqlDbType.BigInt) { Value = identificacionPago });
        command.Parameters.Add(new SqlParameter("@proveedor", SqlDbType.VarChar, 100) { Value = proveedorPago });
        command.Parameters.Add(new SqlParameter("@comision", SqlDbType.Decimal) { Value = comisionProveedor, Precision = 5, Scale = 2 });

        var result = command.ExecuteScalar();
        return Convert.ToInt64(result);
    }

    /// <summary>
    ///     Actualiza un método de pago existente.
    /// </summary>
    public bool ActualizarMetodoPago(long idMetodoPago, long identificacionPago, string proveedorPago, decimal comisionProveedor, bool activo)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"UPDATE metodo_pago_detalle
                                SET identificacion_pago = @identificacion,
                                    proveedor_pago = @proveedor,
                                    comision_proveedor = @comision,
                                    estado = @estado
                                WHERE id_metodo_pago = @id";
        
        command.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt) { Value = idMetodoPago });
        command.Parameters.Add(new SqlParameter("@identificacion", SqlDbType.BigInt) { Value = identificacionPago });
        command.Parameters.Add(new SqlParameter("@proveedor", SqlDbType.VarChar, 100) { Value = proveedorPago });
        command.Parameters.Add(new SqlParameter("@comision", SqlDbType.Decimal) { Value = comisionProveedor, Precision = 5, Scale = 2 });
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = activo });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
    }

    /// <summary>
    ///     Crea un método de pago especial para efectivo (si no existe).
    /// </summary>
    public long ObtenerOCrearMetodoPagoEfectivo()
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        
        // Primero verificar si ya existe
        using (var selectCommand = connection.CreateCommand())
        {
            selectCommand.CommandText = @"SELECT id_metodo_pago
                                         FROM metodo_pago_detalle
                                         WHERE proveedor_pago = 'Efectivo'";
            
            var result = selectCommand.ExecuteScalar();
            if (result != null)
            {
                return Convert.ToInt64(result);
            }
        }

        // Si no existe, crearlo
        using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText = @"INSERT INTO metodo_pago_detalle (identificacion_pago, proveedor_pago, comision_proveedor, estado)
                                     VALUES (0, 'Efectivo', 0, 1);
                                     SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";
        
        var insertResult = insertCommand.ExecuteScalar();
        return Convert.ToInt64(insertResult);
    }
}
