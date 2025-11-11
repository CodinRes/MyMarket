namespace MyMarket.Datos.Modelos.AnaliticaDTOs
{
    public class ReporteVendedorDTO
    {
        public string NombreEmpleado { get; set; } = string.Empty;
        public int CantidadVentas { get; set; }
        public decimal TotalVendido { get; set; }
    }

    public class ReporteProductoDTO
    {
        public long IdProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int IdCategoria { get; set; }
        public int UnidadesVendidas { get; set; }
        public decimal TotalGenerado { get; set; }
    }

    public class ProductoDetalleDTO
    {
        public long IdProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public decimal CostoUnitario { get; set; }
        public int StockActual { get; set; }
    }

    public class VentaMesDTO
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Unidades { get; set; }
        public decimal Total { get; set; }
    }

    public class CategoriaVentaDTO
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public int UnidadesVendidas { get; set; }
        public decimal TotalGenerado { get; set; }
    }

    public class ProductoStockDTO
    {
        public long CodigoProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int Stock { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
    }
}
