using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Infraestructura;
using MyMarket.Datos.Modelos;

namespace MyMarket.Datos.Repositorios
{
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
        public FacturaDto? ObtenerFacturaPorCodigo(long codigoFactura)
        {
            using var connection = _connectionFactory.CreateOpenConnection();

            FacturaDto? factura = null;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT codigo_factura, fecha_emision, descuento, subtotal, id_empleado, dni_cliente, identificacion_pago, estado_venta, porcentaje_impuestos
                                     FROM factura WHERE codigo_factura = @codigo";
                command.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = codigoFactura });

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    factura = new FacturaDto
                    {
                        CodigoFactura = reader.GetInt64(0),
                        FechaEmision = reader.GetDateTime(1),
                        Descuento = reader.GetDecimal(2),
                        Subtotal = reader.GetDecimal(3),
                        IdEmpleado = reader.GetInt32(4),
                        DniCliente = reader.IsDBNull(5) ? null : reader.GetString(5),
                        IdentificacionPago = reader.GetInt64(6),
                        EstadoVenta = reader.GetString(7),
                        PorcentajeImpuestos = reader.GetByte(8)
                    };
                }
            }

            if (factura is null)
            {
                return null;
            }

            using var detalleCommand = connection.CreateCommand();
            detalleCommand.CommandText = @"SELECT codigo_factura, codigo_producto, cantidad_producto
                                         FROM detalle_factura WHERE codigo_factura = @codigo";
            detalleCommand.Parameters.Add(new SqlParameter("@codigo", SqlDbType.BigInt) { Value = codigoFactura });

            using var detalleReader = detalleCommand.ExecuteReader();
            while (detalleReader.Read())
            {
                // Agrega cada detalle recuperado a la colección de la factura.
                factura.Detalles.Add(new FacturaDetalleDto
                {
                    CodigoFactura = detalleReader.GetInt64(0),
                    CodigoProducto = detalleReader.GetInt64(1),
                    CantidadProducto = detalleReader.GetInt16(2)
                });
            }

            return factura;
        }

        /// <summary>
        ///     Obtiene las facturas más recientes (sin sus detalles) para listar en la UI.
        ///     Si se proporciona idEmpleado se filtra por ese empleado (solo sus facturas).
        /// </summary>
        public IEnumerable<FacturaDto> ObtenerFacturasRecientes(int top = 100, int? idEmpleado = null)
        {
            var resultados = new List<FacturaDto>();

            using var connection = _connectionFactory.CreateOpenConnection();
            using var command = connection.CreateCommand();

            if (idEmpleado.HasValue)
            {
                command.CommandText = @"
                    SELECT TOP (@top) codigo_factura, fecha_emision, subtotal, dni_cliente, porcentaje_impuestos, id_empleado
                    FROM factura
                    WHERE id_empleado = @idEmpleado
                    ORDER BY fecha_emision DESC, codigo_factura DESC";
                command.Parameters.Add(new SqlParameter("@top", SqlDbType.Int) { Value = top });
                command.Parameters.Add(new SqlParameter("@idEmpleado", SqlDbType.Int) { Value = idEmpleado.Value });
            }
            else
            {
                command.CommandText = @"
                    SELECT TOP (@top) codigo_factura, fecha_emision, subtotal, dni_cliente, porcentaje_impuestos, id_empleado
                    FROM factura
                    ORDER BY fecha_emision DESC, codigo_factura DESC";
                command.Parameters.Add(new SqlParameter("@top", SqlDbType.Int) { Value = top });
            }

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dto = new FacturaDto
                {
                    CodigoFactura = reader.GetInt64(0),
                    FechaEmision = reader.GetDateTime(1),
                    Subtotal = reader.GetDecimal(2),
                    DniCliente = reader.IsDBNull(3) ? null : reader.GetString(3),
                    PorcentajeImpuestos = reader.GetByte(4),
                    IdEmpleado = reader.GetInt32(5)
                };

                resultados.Add(dto);
            }

            return resultados;
        }
    }
}
