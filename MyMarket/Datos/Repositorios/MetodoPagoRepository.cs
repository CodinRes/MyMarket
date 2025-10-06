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
        command.CommandText = @"SELECT identificacion_pago, proveedor_pago, comision_proveedor, estado
                                 FROM metodo_pago_detalle
                                 WHERE estado = @estado
                                 ORDER BY proveedor_pago";
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = true });

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            metodos.Add(new MetodoPagoDto
            {
                IdentificacionPago = reader.GetInt64(0),
                ProveedorPago = reader.GetString(1),
                ComisionProveedor = reader.GetDecimal(2),
                Activo = reader.GetBoolean(3)
            });
        }

        return metodos;
    }
}
