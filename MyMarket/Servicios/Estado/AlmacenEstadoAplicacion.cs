using System;
using System.IO;
using System.Text.Json;
using MyMarket.Servicios.Estado.Modelos;

namespace MyMarket.Servicios.Estado;

public class AlmacenEstadoAplicacion
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

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
