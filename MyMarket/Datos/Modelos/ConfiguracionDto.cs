namespace MyMarket.Datos.Modelos;

/// <summary>
///     Representa la configuración general de la aplicación.
/// </summary>
public class ConfiguracionDto
{
    /// <summary>
    ///     Porcentaje de impuestos aplicable a las ventas (ej: 21 para IVA 21%).
    /// </summary>
    public byte PorcentajeImpuestos { get; set; }

    /// <summary>
    ///     Días mínimos de antigüedad del cliente para aplicar descuento por antigüedad.
    /// </summary>
    public int DiasAntiguedadMinima { get; set; }

    /// <summary>
    ///     Porcentaje de descuento aplicable a clientes con la antigüedad mínima requerida.
    /// </summary>
    public decimal PorcentajeDescuentoAntiguedad { get; set; }
}
