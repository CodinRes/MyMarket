namespace MyMarket.Data.Models;

public class ProductoDto
{
    public long CodigoProducto { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public short Stock { get; set; }
    public int IdCategoria { get; set; }
    public bool Estado { get; set; }
}
