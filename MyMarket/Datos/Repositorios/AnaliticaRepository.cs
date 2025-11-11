using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Modelos.AnaliticaDTOs;
using MyMarket.Datos.Modelos;

namespace MyMarket.Datos.Repositorios
{
    public class AnaliticaRepository
    {
        private readonly string _connectionString;

        public AnaliticaRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["TiendaDb"]?.ConnectionString
                ?? throw new InvalidOperationException("Connection string 'TiendaDb' no encontrada.");
        }

        // Constructor para permitir pasar la cadena de conexión desde el diseñador o desde tests
        public AnaliticaRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("connectionString no puede estar vacío.", nameof(connectionString));

            _connectionString = connectionString;
        }

        public List<ReporteVendedorDTO> GetReporteVendedores(DateTime? fechaDesde = null)
        {
            var lista = new List<ReporteVendedorDTO>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query =
                    "SELECT e.nombre + ' ' + e.apellido AS NombreEmpleado, " +
                    "COUNT(f.id_factura) AS CantidadVentas, " +
                    "SUM(f.subtotal) AS TotalVendido " +
                    "FROM factura f " +
                    "INNER JOIN empleado e ON e.id_empleado = f.id_empleado";

                if (fechaDesde.HasValue)
                {
                    query += " WHERE f.fecha_emision >= @desde";
                }

                query += " GROUP BY e.nombre, e.apellido ORDER BY TotalVendido DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (fechaDesde.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@desde", System.Data.SqlDbType.DateTime) { Value = fechaDesde.Value });
                    }

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteVendedorDTO
                            {
                                NombreEmpleado = dr["NombreEmpleado"]?.ToString() ?? string.Empty,
                                CantidadVentas = dr["CantidadVentas"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CantidadVentas"]),
                                TotalVendido = dr["TotalVendido"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["TotalVendido"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<ReporteProductoDTO> GetReporteProductos(DateTime? fechaDesde = null)
        {
            var lista = new List<ReporteProductoDTO>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query =
                    "SELECT p.id_producto AS IdProducto, p.nombre_producto AS NombreProducto, p.id_categoria AS IdCategoria, " +
                    "SUM(df.cantidad_producto) AS UnidadesVendidas, " +
                    "SUM(df.cantidad_producto * p.precio_unitario) AS TotalGenerado " +
                    "FROM detalle_factura df " +
                    "INNER JOIN producto p ON p.id_producto = df.id_producto " +
                    "INNER JOIN factura f ON f.id_factura = df.id_factura";

                if (fechaDesde.HasValue)
                {
                    query += " WHERE f.fecha_emision >= @desde";
                }

                query += " GROUP BY p.id_producto, p.nombre_producto, p.id_categoria ORDER BY UnidadesVendidas DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (fechaDesde.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@desde", System.Data.SqlDbType.DateTime) { Value = fechaDesde.Value });
                    }

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteProductoDTO
                            {
                                IdProducto = dr["IdProducto"] == DBNull.Value ? 0 : Convert.ToInt64(dr["IdProducto"]),
                                NombreProducto = dr["NombreProducto"]?.ToString() ?? string.Empty,
                                IdCategoria = dr["IdCategoria"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdCategoria"]),
                                UnidadesVendidas = dr["UnidadesVendidas"] == DBNull.Value ? 0 : Convert.ToInt32(dr["UnidadesVendidas"]),
                                TotalGenerado = dr["TotalGenerado"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["TotalGenerado"]) 
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<CategoriaDto> GetCategorias()
        {
            var lista = new List<CategoriaDto>();
            using var conn = new SqlConnection(_connectionString);
            string query = "SELECT id_categoria, nombre_categoria, estado FROM categoria WHERE estado = 1 ORDER BY nombre_categoria";
            using var cmd = new SqlCommand(query, conn);
            conn.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new CategoriaDto
                {
                    IdCategoria = dr["id_categoria"] == DBNull.Value ? 0 : Convert.ToInt32(dr["id_categoria"]),
                    NombreCategoria = dr["nombre_categoria"]?.ToString() ?? string.Empty,
                    Activo = dr["estado"] == DBNull.Value ? true : Convert.ToBoolean(dr["estado"])
                });
            }
            return lista;
        }

        public List<CategoriaVentaDTO> GetVentasPorCategoria(DateTime? fechaDesde = null)
        {
            var lista = new List<CategoriaVentaDTO>();
            using var conn = new SqlConnection(_connectionString);
            string query =
                "SELECT c.id_categoria AS IdCategoria, c.nombre_categoria AS NombreCategoria, " +
                "SUM(df.cantidad_producto) AS UnidadesVendidas, " +
                "SUM(df.cantidad_producto * p.precio_unitario) AS TotalGenerado " +
                "FROM detalle_factura df " +
                "INNER JOIN producto p ON p.id_producto = df.id_producto " +
                "INNER JOIN factura f ON f.id_factura = df.id_factura " +
                "INNER JOIN categoria c ON c.id_categoria = p.id_categoria";

            if (fechaDesde.HasValue)
            {
                query += " WHERE f.fecha_emision >= @desde";
            }

            query += " GROUP BY c.id_categoria, c.nombre_categoria ORDER BY UnidadesVendidas DESC";

            using var cmd = new SqlCommand(query, conn);
            if (fechaDesde.HasValue)
                cmd.Parameters.Add(new SqlParameter("@desde", System.Data.SqlDbType.DateTime) { Value = fechaDesde.Value });

            conn.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new CategoriaVentaDTO
                {
                    IdCategoria = dr["IdCategoria"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdCategoria"]),
                    NombreCategoria = dr["NombreCategoria"]?.ToString() ?? string.Empty,
                    UnidadesVendidas = dr["UnidadesVendidas"] == DBNull.Value ? 0 : Convert.ToInt32(dr["UnidadesVendidas"]),
                    TotalGenerado = dr["TotalGenerado"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["TotalGenerado"]) 
                });
            }

            return lista;
        }

        public List<ProductoStockDTO> GetProductosBajoStock(int umbral = 5)
        {
            var lista = new List<ProductoStockDTO>();
            using var conn = new SqlConnection(_connectionString);
            string query =
                "SELECT p.codigo_producto AS CodigoProducto, p.nombre_producto AS NombreProducto, ISNULL(p.stock,0) AS Stock, c.nombre_categoria AS NombreCategoria " +
                "FROM producto p " +
                "INNER JOIN categoria c ON c.id_categoria = p.id_categoria " +
                "WHERE p.stock <= @umbral " +
                "ORDER BY p.stock ASC";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@umbral", System.Data.SqlDbType.Int) { Value = umbral });

            conn.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new ProductoStockDTO
                {
                    CodigoProducto = dr["CodigoProducto"] == DBNull.Value ? 0 : Convert.ToInt64(dr["CodigoProducto"]),
                    NombreProducto = dr["NombreProducto"]?.ToString() ?? string.Empty,
                    Stock = dr["Stock"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Stock"]),
                    NombreCategoria = dr["NombreCategoria"]?.ToString() ?? string.Empty
                });
            }

            return lista;
        }

        public ProductoDetalleDTO? GetProductoDetalle(long idProducto)
        {
            using var conn = new SqlConnection(_connectionString);
            string query =
                "SELECT p.id_producto AS IdProducto, p.nombre_producto AS NombreProducto, p.precio_unitario, ISNULL(p.stock,0) AS Stock " +
                "FROM producto p WHERE p.id_producto = @id";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.BigInt) { Value = idProducto });

            conn.Open();
            using var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new ProductoDetalleDTO
                {
                    IdProducto = dr["IdProducto"] == DBNull.Value ? 0 : Convert.ToInt64(dr["IdProducto"]),
                    NombreProducto = dr["NombreProducto"]?.ToString() ?? string.Empty,
                    PrecioUnitario = dr["precio_unitario"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["precio_unitario"]),
                    // costo_unitario no existe en la base, asignar 0 por compatibilidad
                    CostoUnitario = 0m,
                    StockActual = dr["Stock"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Stock"])
                };
            }

            return null;
        }

        public List<VentaMesDTO> GetVentasPorMes(long idProducto)
        {
            var lista = new List<VentaMesDTO>();
            using var conn = new SqlConnection(_connectionString);
            string query =
                "SELECT YEAR(f.fecha_emision) AS Yr, MONTH(f.fecha_emision) AS Mth, " +
                "SUM(df.cantidad_producto) AS Unidades, SUM(df.cantidad_producto * p.precio_unitario) AS Total " +
                "FROM detalle_factura df " +
                "INNER JOIN factura f ON f.id_factura = df.id_factura " +
                "INNER JOIN producto p ON p.id_producto = df.id_producto " +
                "WHERE df.id_producto = @id " +
                "GROUP BY YEAR(f.fecha_emision), MONTH(f.fecha_emision) " +
                "ORDER BY YEAR(f.fecha_emision), MONTH(f.fecha_emision)";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.BigInt) { Value = idProducto });

            conn.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new VentaMesDTO
                {
                    Year = dr["Yr"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Yr"]),
                    Month = dr["Mth"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Mth"]),
                    Unidades = dr["Unidades"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Unidades"]),
                    Total = dr["Total"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["Total"]) 
                });
            }

            return lista;
        }

        public DateTime? GetFechaUltimaVenta(long idProducto)
        {
            using var conn = new SqlConnection(_connectionString);
            string query =
                "SELECT MAX(f.fecha_emision) AS Ultima FROM detalle_factura df " +
                "INNER JOIN factura f ON f.id_factura = df.id_factura " +
                "WHERE df.id_producto = @id";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.BigInt) { Value = idProducto });

            conn.Open();
            var result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
                return Convert.ToDateTime(result);
            return null;
        }

        // Se mantiene un método simplificado para compatibilidad: rentabilidad removida, costo siempre 0.
        public (int totalUnidades, decimal totalRevenue, decimal totalCost) GetTotalesProducto(long idProducto)
        {
            using var conn = new SqlConnection(_connectionString);
            string query =
                "SELECT SUM(df.cantidad_producto) AS TotalUnidades, " +
                "SUM(df.cantidad_producto * p.precio_unitario) AS Revenue " +
                "FROM detalle_factura df " +
                "INNER JOIN producto p ON p.id_producto = df.id_producto " +
                "WHERE df.id_producto = @id";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.BigInt) { Value = idProducto });

            conn.Open();
            using var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                int unidades = dr["TotalUnidades"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TotalUnidades"]);
                decimal rev = dr["Revenue"] == DBNull.Value ? 0m : Convert.ToDecimal(dr["Revenue"]);
                // Cost is not tracked in this schema; return 0 to indicate not available.
                decimal cost = 0m;
                return (unidades, rev, cost);
            }

            return (0, 0m, 0m);
        }
    }
}
