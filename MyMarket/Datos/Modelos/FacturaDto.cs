namespace MyMarket.Datos.Modelos;

public class FacturaDto
{
    public long CodigoFactura { get; set; }
    public DateTime FechaEmision { get; set; }
    public decimal Descuento { get; set; }
    public decimal Subtotal { get; set; }
    public int IdEmpleado { get; set; }
    public string? DniCliente { get; set; }
    public long IdentificacionPago { get; set; }
    public string EstadoVenta { get; set; } = string.Empty;
    public byte PorcentajeImpuestos { get; set; }
    public List<FacturaDetalleDto> Detalles { get; set; } = new();
}
