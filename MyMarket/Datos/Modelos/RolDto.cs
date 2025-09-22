namespace MyMarket.Datos.Modelos;

public class RolDto
{
    public int IdRol { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    public override string ToString() => Descripcion;
}
