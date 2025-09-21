namespace MyMarket.Data.Models;

public class FacturaDetalleDto
{
    public long CodigoFactura { get; set; }
    public long CodigoProducto { get; set; }
    public short CantidadProducto { get; set; }
}
