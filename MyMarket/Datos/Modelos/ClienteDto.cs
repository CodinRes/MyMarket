using System;

namespace MyMarket.Datos.Modelos;

/// <summary>
///     Representa la información básica de un cliente suscripto.
/// </summary>
public class ClienteDto
{
    /// <summary>
    ///     Identificador único del cliente en la base de datos.
    /// </summary>
    public long IdCliente { get; set; }

    /// <summary>
    ///     Documento nacional de identidad del cliente.
    /// </summary>
    public string Dni { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre del cliente.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    ///     Apellido del cliente.
    /// </summary>
    public string Apellido { get; set; } = string.Empty;

    /// <summary>
    ///     Dirección de contacto.
    /// </summary>
    public string Direccion { get; set; } = string.Empty;

    /// <summary>
    ///     Fecha en la que el cliente se registró en el sistema.
    /// </summary>
    public DateTime FechaRegistro { get; set; }
        = DateTime.Today;

    /// <summary>
    ///     Correo electrónico de contacto. Debe ser único.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///     Indica si el cliente se encuentra activo.
    /// </summary>
    public bool Activo { get; set; }
        = true;
}
