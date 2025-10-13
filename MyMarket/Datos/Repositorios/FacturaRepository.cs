using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Infraestructura;
using MyMarket.Datos.Modelos;

namespace MyMarket.Datos.Repositorios;

/// <summary>
///     Encapsula las operaciones de lectura y escritura de facturas.
/// </summary>
public class FacturaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    /// <summary>
    ///     Recibe la fábrica de conexiones utilizada para abrir conexiones.
    /// </summary>
    public FacturaRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    ///     Crea una nueva factura y sus detalles dentro de una transacción.
    /// </summary>
    public long CrearFactura(FacturaCabecera cabecera, IEnumerable<FacturaDetalle> detalles)
    {
        if (cabecera is null)
        {
            throw new ArgumentNullException(nameof(cabecera));
        }

        if (detalles is null)
        {
            throw new ArgumentNullException(nameof(detalles));
        }

        // Se materializa la enumeración para evitar recorrerla múltiples veces.
        var detalleList = detalles.ToList();

        using var connection = _connectionFactory.CreateOpenConnection();
        using var transaction = connection.BeginTransaction();

        try
        {
            long codigoFactura;
            using (var insertFacturaCommand = new SqlCommand(@"INSERT INTO factura
                (fecha_emision, descuento, subtotal, id_empleado, dni_cliente, identificacion_pago, estado_venta, porcentaje_impuestos)
                VALUES (@fecha_emision, @descuento, @subtotal, @id_empleado, @dni_cliente, @identificacion_pago, @estado_venta, @porcentaje_impuestos);
                SELECT CAST(SCOPE_IDENTITY() AS BIGINT);", connection, transaction))
            {
                // Se cargan los parámetros para la cabecera de la factura.
                insertFacturaCommand.Parameters.Add(new SqlParameter("@fecha_emision", SqlDbType.DateTime) { Value = cabecera.FechaEmision });

                var descuentoParam = new SqlParameter("@descuento", SqlDbType.Decimal)
                {
                    Precision = 18,
                    Scale = 2,
                    Value = cabecera.Descuento
                };
                insertFacturaCommand.Parameters.Add(descuentoParam);

                var subtotalParam = new SqlParameter("@subtotal", SqlDbType.Decimal)
                {
                    Precision = 18,
                    Scale = 2,
                    Value = cabecera.Subtotal
                };
                insertFacturaCommand.Parameters.Add(subtotalParam);

                insertFacturaCommand.Parameters.Add(new SqlParameter("@id_empleado", SqlDbType.Int) { Value = cabecera.IdEmpleado });
                insertFacturaCommand.Parameters.Add(new SqlParameter("@dni_cliente", SqlDbType.VarChar, 8)
                {
                    Value = cabecera.DniCliente ?? (object)DBNull.Value
                });
                insertFacturaCommand.Parameters.Add(new SqlParameter("@identificacion_pago", SqlDbType.BigInt) { Value = cabecera.IdentificacionPago });
                insertFacturaCommand.Parameters.Add(new SqlParameter("@estado_venta", SqlDbType.VarChar, 20)
                {
                    Value = string.IsNullOrWhiteSpace(cabecera.EstadoVenta) ? "pendiente" : cabecera.EstadoVenta
                });
                insertFacturaCommand.Parameters.Add(new SqlParameter("@porcentaje_impuestos", SqlDbType.TinyInt) { Value = cabecera.PorcentajeImpuestos });

                // Recupera el identificador generado para asociar los detalles.
                var result = insertFacturaCommand.ExecuteScalar();
                codigoFactura = Convert.ToInt64(result, CultureInfo.InvariantCulture);
            }

            foreach (var detalle in detalleList)
            {
                // Inserta cada renglón asociado a la factura creada.
                using var insertDetalleCommand = new SqlCommand(@"INSERT INTO detalle_factura
                    (codigo_factura, codigo_producto, cantidad_producto)
                    VALUES (@codigo_factura, @codigo_producto, @cantidad_producto);", connection, transaction);

                insertDetalleCommand.Parameters.Add(new SqlParameter("@codigo_factura", SqlDbType.BigInt) { Value = codigoFactura });
                insertDetalleCommand.Parameters.Add(new SqlParameter("@codigo_producto", SqlDbType.BigInt) { Value = detalle.CodigoProducto });
                insertDetalleCommand.Parameters.Add(new SqlParameter("@cantidad_producto", SqlDbType.SmallInt) { Value = detalle.CantidadProducto });

                insertDetalleCommand.ExecuteNonQuery();
            }

            transaction.Commit();
            return codigoFactura;
        }
        catch
        {
            // Cualquier error debe revertir los cambios parciales para mantener la integridad.
            transaction.Rollback();
            throw;
        }
    }

    /// <summary>
    ///     Obtiene una factura junto con los detalles registrados.
    /// </summary>
    public IReadOnlyList<FacturaListadoDto> ObtenerFacturasEmitidas()
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();

        command.CommandText = @"SELECT f.codigo_factura,
                                        f.fecha_emision,
                                        f.subtotal,
                                        f.descuento,
                                        f.porcentaje_impuestos,
                                        f.estado_venta,
                                        f.dni_cliente,
                                        c.nombre AS nombre_cliente,
                                        c.apellido AS apellido_cliente,
                                        e.nombre AS nombre_empleado,
                                        e.apellido AS apellido_empleado
                                 FROM factura f
                                 LEFT JOIN cliente c ON f.dni_cliente = c.dni_cliente
                                 INNER JOIN empleado e ON f.id_empleado = e.id_empleado
                                 ORDER BY f.fecha_emision DESC, f.codigo_factura DESC";

        using var reader = command.ExecuteReader();
        var facturas = new List<FacturaListadoDto>();

        while (reader.Read())
        {
            var nombreCliente = reader.IsDBNull(7) ? null : reader.GetString(7);
            var apellidoCliente = reader.IsDBNull(8) ? null : reader.GetString(8);
            var nombreEmpleado = reader.GetString(9);
            var apellidoEmpleado = reader.GetString(10);

            facturas.Add(new FacturaListadoDto
            {
                CodigoFactura = reader.GetInt64(0),
                FechaEmision = reader.GetDateTime(1),
                Subtotal = reader.GetDecimal(2),
                Descuento = reader.GetDecimal(3),
                PorcentajeImpuestos = reader.GetByte(4),
                EstadoVenta = reader.GetString(5),
                DniCliente = reader.IsDBNull(6) ? null : reader.GetString(6),
                ClienteNombreCompleto = ObtenerNombreCompleto(nombreCliente, apellidoCliente, "Cliente ocasional"),
                EmpleadoNombreCompleto = ObtenerNombreCompleto(nombreEmpleado, apellidoEmpleado, string.Empty)
            });
        }

        return facturas;
    }

    public FacturaDto? ObtenerFacturaPorCodigo(long codigoFactura)
    {
        using var connection = _connectionFactory.CreateOpenConnection();

        FacturaDto? factura = null;
        using (var command = connection.CreateCommand())
        {
            command.CommandText = @"SELECT f.codigo_factura,
                                           f.fecha_emision,
                                           f.descuento,
                                           f.subtotal,
                                           f.id_empleado,
                                           f.dni_cliente,
                                           f.identificacion_pago,
                                           f.estado_venta,
                                           f.porcentaje_impuestos,
                                           c.nombre AS nombre_cliente,
                                           c.apellido AS apellido_cliente,
                                           e.nombre AS nombre_empleado,
                                           e.apellido AS apellido_empleado,
                                           mp.proveedor_pago
                                    FROM factura f
                                    LEFT JOIN cliente c ON f.dni_cliente = c.dni_cliente
                                    INNER JOIN empleado e ON f.id_empleado = e.id_empleado
                                    LEFT JOIN metodo_pago_detalle mp ON f.identificacion_pago = mp.identificacion_pago
                                    WHERE f.codigo_factura = @codigo";
            command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = codigoFactura });

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var nombreCliente = reader.IsDBNull(9) ? null : reader.GetString(9);
                var apellidoCliente = reader.IsDBNull(10) ? null : reader.GetString(10);
                var nombreEmpleado = reader.GetString(11);
                var apellidoEmpleado = reader.GetString(12);

                factura = new FacturaDto
                {
                    CodigoFactura = reader.GetInt64(0),
                    FechaEmision = reader.GetDateTime(1),
                    Descuento = reader.GetDecimal(2),
                    Subtotal = reader.GetDecimal(3),
                    IdEmpleado = reader.GetInt32(4),
                    DniCliente = reader.IsDBNull(5) ? null : reader.GetString(5),
                    ClienteNombreCompleto = ObtenerNombreCompleto(nombreCliente, apellidoCliente, "Cliente ocasional"),
                    IdentificacionPago = reader.GetInt64(6),
                    MetodoPagoDescripcion = reader.IsDBNull(13) ? string.Empty : reader.GetString(13),
                    EstadoVenta = reader.GetString(7),
                    PorcentajeImpuestos = reader.GetByte(8),
                    EmpleadoNombreCompleto = ObtenerNombreCompleto(nombreEmpleado, apellidoEmpleado, string.Empty)
                };
            }
        }

        if (factura is null)
        {
            return null;
        }

        using var detalleCommand = connection.CreateCommand();
        detalleCommand.CommandText = @"SELECT df.codigo_factura,
                                             df.codigo_producto,
                                             df.cantidad_producto,
                                             p.nombre_producto,
                                             p.precio_unitario
                                      FROM detalle_factura df
                                      INNER JOIN producto p ON df.codigo_producto = p.codigo_producto
                                      WHERE df.codigo_factura = @codigo";
        detalleCommand.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = codigoFactura });

        using var detalleReader = detalleCommand.ExecuteReader();
        while (detalleReader.Read())
        {
            // Agrega cada detalle recuperado a la colección de la factura.
            factura.Detalles.Add(new FacturaDetalleDto
            {
                CodigoFactura = detalleReader.GetInt64(0),
                CodigoProducto = detalleReader.GetInt64(1),
                CantidadProducto = detalleReader.GetInt16(2),
                NombreProducto = detalleReader.GetString(3),
                PrecioUnitario = detalleReader.GetDecimal(4)
            });
        }

        return factura;
    }

    private static string ObtenerNombreCompleto(string? nombre, string? apellido, string defaultValue)
    {
        if (string.IsNullOrWhiteSpace(nombre) && string.IsNullOrWhiteSpace(apellido))
        {
            return defaultValue;
        }

        if (string.IsNullOrWhiteSpace(nombre))
        {
            return apellido?.Trim() ?? string.Empty;
        }

        if (string.IsNullOrWhiteSpace(apellido))
        {
            return nombre.Trim();
        }

        return $"{nombre.Trim()} {apellido.Trim()}";
    }
}
