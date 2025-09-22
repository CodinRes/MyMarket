using System;
using System.IO;
using System.Text.Json;
using MyMarket.Services.Models;

namespace MyMarket.Services;

public class AppStateStorage
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public AppStateStorage(string? filePath = null)
    {
        if (!string.IsNullOrWhiteSpace(filePath))
        {
            _filePath = filePath;
            EnsureDirectoryExists(Path.GetDirectoryName(filePath));
            return;
        }

        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var directory = Path.Combine(appData, "MyMarket");
        EnsureDirectoryExists(directory);
        _filePath = Path.Combine(directory, "appstate.json");
    }

    public AppState Load()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                return new AppState();
            }

            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new AppState();
            }

            return JsonSerializer.Deserialize<AppState>(json, _jsonOptions) ?? new AppState();
        }
        catch
        {
            return new AppState();
        }
    }

    public void Save(AppState state)
    {
        try
        {
            var json = JsonSerializer.Serialize(state, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }
        catch
        {
            // La persistencia de estado es una característica de conveniencia;
            // si falla evitamos que la aplicación se interrumpa.
        }
    }

    private static void EnsureDirectoryExists(string? directory)
    {
        if (string.IsNullOrWhiteSpace(directory))
        {
            return;
        }

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}
