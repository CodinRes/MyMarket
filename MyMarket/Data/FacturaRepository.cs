using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Microsoft.Data.SqlClient;
using MyMarket.Data.Models;

namespace MyMarket.Data;

public class FacturaCabecera
{
    public DateTime FechaEmision { get; set; } = DateTime.Now;
    public decimal Descuento { get; set; }
    public decimal Subtotal { get; set; }
    public int IdEmpleado { get; set; }
    public string? DniCliente { get; set; }
    public long IdentificacionPago { get; set; }
    public string EstadoVenta { get; set; } = "pendiente";
    public byte PorcentajeImpuestos { get; set; } = 21;
}

public class FacturaDetalle
{
    public long CodigoProducto { get; set; }
    public short CantidadProducto { get; set; }
}

public class FacturaRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public FacturaRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

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

                var result = insertFacturaCommand.ExecuteScalar();
                codigoFactura = Convert.ToInt64(result, CultureInfo.InvariantCulture);
            }

            foreach (var detalle in detalleList)
            {
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
            transaction.Rollback();
            throw;
        }
    }

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
            factura.Detalles.Add(new FacturaDetalleDto
            {
                CodigoFactura = detalleReader.GetInt64(0),
                CodigoProducto = detalleReader.GetInt64(1),
                CantidadProducto = detalleReader.GetInt16(2)
            });
        }

        return factura;
    }
}
