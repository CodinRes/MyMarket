using System;
using System.IO;
using System.Text.Json;
using MyMarket.Datos.Modelos;

namespace MyMarket.Servicios.Estado;

/// <summary>
///     Administra la persistencia en disco de la configuración de la aplicación.
/// </summary>
public class AlmacenConfiguracion
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    ///     Permite indicar una ruta personalizada para guardar la configuración o utiliza la carpeta de AppData.
    /// </summary>
    public AlmacenConfiguracion(string? filePath = null)
    {
        if (!string.IsNullOrWhiteSpace(filePath))
        {
            _filePath = filePath;
            var fileDirectory = Path.GetDirectoryName(filePath);
            if (fileDirectory is not null)
            {
                AsegurarDirectorio(fileDirectory);
            }
            return;
        }

        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var directory = Path.Combine(appData, "MyMarket");
        AsegurarDirectorio(directory);
        _filePath = Path.Combine(directory, "config.json");
    }

    /// <summary>
    ///     Recupera la configuración almacenada desde el archivo JSON. Si hay un error se devuelve una configuración por defecto.
    /// </summary>
    public ConfiguracionDto Cargar()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                return ObtenerConfiguracionPorDefecto();
            }

            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return ObtenerConfiguracionPorDefecto();
            }

            return JsonSerializer.Deserialize<ConfiguracionDto>(json, _jsonOptions) ?? ObtenerConfiguracionPorDefecto();
        }
        catch
        {
            return ObtenerConfiguracionPorDefecto();
        }
    }

    /// <summary>
    ///     Serializa y guarda la configuración actual de la aplicación.
    /// </summary>
    public void Guardar(ConfiguracionDto configuracion)
    {
        try
        {
            var json = JsonSerializer.Serialize(configuracion, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }
        catch
        {
            // La persistencia de configuración es una característica de conveniencia;
            // si falla evitamos que la aplicación se interrumpa.
        }
    }

    /// <summary>
    ///     Devuelve la configuración por defecto con valores iniciales razonables.
    /// </summary>
    private static ConfiguracionDto ObtenerConfiguracionPorDefecto()
    {
        return new ConfiguracionDto
        {
            PorcentajeImpuestos = 21,
            DiasAntiguedadMinima = 365, // 1 año
            PorcentajeDescuentoAntiguedad = 5 // 5% de descuento
        };
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
