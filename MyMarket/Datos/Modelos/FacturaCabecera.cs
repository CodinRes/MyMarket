using System;

namespace MyMarket.Datos.Modelos;

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
