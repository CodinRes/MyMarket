using System;
using System.IO;
using System.Text.Json;
using MyMarket.Servicios.Estado.Modelos;

namespace MyMarket.Servicios.Estado;

/// <summary>
///     Administra la persistencia en disco del estado de la aplicación.
/// </summary>
public class AlmacenEstadoAplicacion
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    ///     Permite indicar una ruta personalizada para guardar el estado o utiliza la carpeta de AppData.
    /// </summary>
    public AlmacenEstadoAplicacion(string? filePath = null)
    {
        if (!string.IsNullOrWhiteSpace(filePath))
        {
            _filePath = filePath;
            AsegurarDirectorio(Path.GetDirectoryName(filePath));
            return;
        }

        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var directory = Path.Combine(appData, "MyMarket");
        AsegurarDirectorio(directory);
        _filePath = Path.Combine(directory, "appstate.json");
    }

    /// <summary>
    ///     Recupera el estado almacenado desde el archivo JSON. Si hay un error se devuelve un estado vacío.
    /// </summary>
    public EstadoAplicacion Cargar()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                return new EstadoAplicacion();
            }

            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new EstadoAplicacion();
            }

            return JsonSerializer.Deserialize<EstadoAplicacion>(json, _jsonOptions) ?? new EstadoAplicacion();
        }
        catch
        {
            return new EstadoAplicacion();
        }
    }

    /// <summary>
    ///     Serializa y guarda el estado actual de la aplicación.
    /// </summary>
    public void Guardar(EstadoAplicacion estado)
    {
        try
        {
            var json = JsonSerializer.Serialize(estado, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }
        catch
        {
            // La persistencia de estado es una característica de conveniencia;
            // si falla evitamos que la aplicación se interrumpa.
        }
    }

    /// <summary>
    ///     Crea el directorio destino si no existe.
    /// </summary>
    private static void AsegurarDirectorio(string? directorio)
    {
        if (string.IsNullOrWhiteSpace(directorio))
        {
            return;
        }

        if (!Directory.Exists(directorio))
        {
            Directory.CreateDirectory(directorio);
        }
    }
}
