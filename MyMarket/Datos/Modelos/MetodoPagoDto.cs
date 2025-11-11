namespace MyMarket.Datos.Modelos;

/// <summary>
///     Representa un método de pago habilitado para registrar ventas.
/// </summary>
public class MetodoPagoDto
{
    /// <summary>
    ///     Identificador único del método de pago en la base de datos.
    /// </summary>
    public long IdMetodoPago { get; set; }

    /// <summary>
    ///     Identificador único del pago, como un CBU/CVU.
    /// </summary>
    public long IdentificacionPago { get; set; }

    /// <summary>
    ///     Nombre del proveedor que procesa el pago (Visa, Mastercard, etc.).
    /// </summary>
    public string ProveedorPago { get; set; } = string.Empty;

    /// <summary>
    ///     Comisión aplicada por el proveedor sobre cada operación.
    /// </summary>
    public decimal ComisionProveedor { get; set; }

    /// <summary>
    ///     Indica si el método se encuentra habilitado para su utilización.
    /// </summary>
    public bool Activo { get; set; }
}
