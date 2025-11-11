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
            long idFactura;
            using (var insertFacturaCommand = new SqlCommand(@"INSERT INTO factura
                (fecha_emision, descuento, subtotal, id_empleado, id_cliente, id_metodo_pago, estado_venta, porcentaje_impuestos)
                VALUES (@fecha_emision, @descuento, @subtotal, @id_empleado, @id_cliente, @id_metodo_pago, @estado_venta, @porcentaje_impuestos);
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
                insertFacturaCommand.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.BigInt)
                {
                    Value = cabecera.IdCliente ?? (object)DBNull.Value
                });
                insertFacturaCommand.Parameters.Add(new SqlParameter("@id_metodo_pago", SqlDbType.BigInt)
                {
                    Value = cabecera.IdMetodoPago ?? (object)DBNull.Value
                });
                insertFacturaCommand.Parameters.Add(new SqlParameter("@estado_venta", SqlDbType.VarChar, 20)
                {
                    Value = string.IsNullOrWhiteSpace(cabecera.EstadoVenta) ? "pendiente" : cabecera.EstadoVenta
                });
                insertFacturaCommand.Parameters.Add(new SqlParameter("@porcentaje_impuestos", SqlDbType.TinyInt) { Value = cabecera.PorcentajeImpuestos });

                // Recupera el identificador generado para asociar los detalles.
                var result = insertFacturaCommand.ExecuteScalar();
                idFactura = Convert.ToInt64(result, CultureInfo.InvariantCulture);
            }

            foreach (var detalle in detalleList)
            {
                // Inserta cada renglón asociado a la factura creada. Se traduce codigo_producto -> id_producto para cumplir FK.
                using var insertDetalleCommand = new SqlCommand(@"INSERT INTO detalle_factura (id_factura, id_producto, cantidad_producto)
                    SELECT @id_factura, p.id_producto, @cantidad_producto
                    FROM producto p
                    WHERE p.codigo_producto = @codigo_producto;", connection, transaction);

                insertDetalleCommand.Parameters.Add(new SqlParameter("@id_factura", SqlDbType.BigInt) { Value = idFactura });
                insertDetalleCommand.Parameters.Add(new SqlParameter("@codigo_producto", SqlDbType.BigInt) { Value = detalle.CodigoProducto });
                insertDetalleCommand.Parameters.Add(new SqlParameter("@cantidad_producto", SqlDbType.SmallInt) { Value = detalle.CantidadProducto });

                var rows = insertDetalleCommand.ExecuteNonQuery();
                if (rows == 0)
                {
                    // Si no se insertó ninguna fila, el código no existe en la tabla producto.
                    throw new InvalidOperationException($"No existe un producto con el código {detalle.CodigoProducto}.");
                }
            }

            transaction.Commit();
            return idFactura;
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

        command.CommandText = @"SELECT f.id_factura,
                                        f.id_empleado,
                                        f.fecha_emision,
                                        f.subtotal,
                                        f.descuento,
                                        f.porcentaje_impuestos,
                                        f.estado_venta,
                                        c.dni_cliente,
                                        c.nombre AS nombre_cliente,
                                        c.apellido AS apellido_cliente,
                                        e.nombre AS nombre_empleado,
                                        e.apellido AS apellido_empleado,
                                        f.id_metodo_pago,
                                        mp.comision_proveedor
                                 FROM factura f
                                 LEFT JOIN cliente c ON f.id_cliente = c.id_cliente
                                 INNER JOIN empleado e ON f.id_empleado = e.id_empleado
                                 LEFT JOIN metodo_pago_detalle mp ON f.id_metodo_pago = mp.id_metodo_pago
                                 ORDER BY f.fecha_emision DESC, f.id_factura DESC";

        using var reader = command.ExecuteReader();
        var facturas = new List<FacturaListadoDto>();

        while (reader.Read())
        {
            var nombreCliente = reader.IsDBNull(8) ? null : reader.GetString(8);
            var apellidoCliente = reader.IsDBNull(9) ? null : reader.GetString(9);
            var nombreEmpleado = reader.GetString(10);
            var apellidoEmpleado = reader.GetString(11);
            var idMetodoPago = reader.IsDBNull(12) ? (long?)null : reader.GetInt64(12);
            var comisionProveedor = reader.IsDBNull(13) ? 0m : reader.GetDecimal(13);

            facturas.Add(new FacturaListadoDto
            {
                IdFactura = reader.GetInt64(0),
                IdEmpleado = reader.GetInt32(1),
                FechaEmision = reader.GetDateTime(2),
                Subtotal = reader.GetDecimal(3),
                Descuento = reader.GetDecimal(4),
                PorcentajeImpuestos = reader.GetByte(5),
                EstadoVenta = reader.GetString(6),
                DniCliente = reader.IsDBNull(7) ? null : reader.GetString(7),
                ClienteNombreCompleto = ObtenerNombreCompleto(nombreCliente, apellidoCliente, "Cliente ocasional"),
                EmpleadoNombreCompleto = ObtenerNombreCompleto(nombreEmpleado, apellidoEmpleado, string.Empty),
                IdMetodoPago = idMetodoPago,
                ComisionProveedor = comisionProveedor
            });
        }

        return facturas;
    }

    public FacturaDto? ObtenerFacturaPorId(long idFactura)
    {
        using var connection = _connectionFactory.CreateOpenConnection();

        FacturaDto? factura = null;
        using (var command = connection.CreateCommand())
        {
            command.CommandText = @"SELECT f.id_factura,
                                           f.fecha_emision,
                                           f.descuento,
                                           f.subtotal,
                                           f.id_empleado,
                                           f.id_cliente,
                                           c.dni_cliente,
                                           f.id_metodo_pago,
                                           mp.identificacion_pago,
                                           f.estado_venta,
                                           f.porcentaje_impuestos,
                                           c.nombre AS nombre_cliente,
                                           c.apellido AS apellido_cliente,
                                           e.nombre AS nombre_empleado,
                                           e.apellido AS apellido_empleado,
                                           mp.proveedor_pago,
                                           mp.comision_proveedor
                                    FROM factura f
                                    LEFT JOIN cliente c ON f.id_cliente = c.id_cliente
                                    INNER JOIN empleado e ON f.id_empleado = e.id_empleado
                                    LEFT JOIN metodo_pago_detalle mp ON f.id_metodo_pago = mp.id_metodo_pago
                                    WHERE f.id_factura = @id";
            command.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt) { Value = idFactura });

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var nombreCliente = reader.IsDBNull(11) ? null : reader.GetString(11);
                var apellidoCliente = reader.IsDBNull(12) ? null : reader.GetString(12);
                var nombreEmpleado = reader.GetString(13);
                var apellidoEmpleado = reader.GetString(14);
                var esMetodoPagoNulo = reader.IsDBNull(7);

                factura = new FacturaDto
                {
                    IdFactura = reader.GetInt64(0),
                    FechaEmision = reader.GetDateTime(1),
                    Descuento = reader.GetDecimal(2),
                    Subtotal = reader.GetDecimal(3),
                    IdEmpleado = reader.GetInt32(4),
                    IdCliente = reader.IsDBNull(5) ? null : reader.GetInt64(5),
                    DniCliente = reader.IsDBNull(6) ? null : reader.GetString(6),
                    IdMetodoPago = esMetodoPagoNulo ? null : reader.GetInt64(7),
                    IdentificacionPago = reader.IsDBNull(8) ? 0 : reader.GetInt64(8),
                    EstadoVenta = reader.GetString(9),
                    PorcentajeImpuestos = reader.GetByte(10),
                    ClienteNombreCompleto = ObtenerNombreCompleto(nombreCliente, apellidoCliente, "Cliente ocasional"),
                    EmpleadoNombreCompleto = ObtenerNombreCompleto(nombreEmpleado, apellidoEmpleado, string.Empty),
                    MetodoPagoDescripcion = esMetodoPagoNulo ? "Efectivo" : (reader.IsDBNull(15) ? string.Empty : reader.GetString(15)),
                    ComisionProveedor = esMetodoPagoNulo || reader.IsDBNull(16) ? 0 : reader.GetDecimal(16)
                };
            }
        }

        if (factura is null)
        {
            return null;
        }

        using var detalleCommand = connection.CreateCommand();
        detalleCommand.CommandText = @"SELECT f.id_factura,
                                             p.codigo_producto,
                                             df.cantidad_producto,
                                             p.nombre_producto,
                                             p.precio_unitario
                                      FROM detalle_factura df
                                      INNER JOIN producto p ON df.id_producto = p.id_producto
                                      INNER JOIN factura f ON df.id_factura = f.id_factura
                                      WHERE f.id_factura = @id";
        detalleCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt) { Value = idFactura });

        using var detalleReader = detalleCommand.ExecuteReader();
        while (detalleReader.Read())
        {
            // Agrega cada detalle recuperado a la colección de la factura.
            factura.Detalles.Add(new FacturaDetalleDto
            {
                IdFactura = detalleReader.GetInt64(0),
                CodigoProducto = detalleReader.GetInt64(1),
                CantidadProducto = detalleReader.GetInt16(2),
                NombreProducto = detalleReader.GetString(3),
                PrecioUnitario = detalleReader.GetDecimal(4)
            });
        }

        return factura;
    }

    /// <summary>
    ///     Actualiza el estado de una factura.
    /// </summary>
    public bool ActualizarEstadoFactura(long idFactura, string nuevoEstado)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"UPDATE factura
                                SET estado_venta = @estado
                                WHERE id_factura = @id";
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.VarChar, 20) { Value = nuevoEstado });
        command.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt) { Value = idFactura });

        var rows = command.ExecuteNonQuery();
        return rows > 0;
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
