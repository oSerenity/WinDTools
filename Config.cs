using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Config
{
    public string BaseDirectory { get; set; }
    public string GitHubPath { get; set; }
    public bool DebugMode { get; set; }
    public List<string> IgnoredFolders { get; set; }
    public string DefaultScanPath { get; set; }
    public bool DeveloperMode { get; set; }

    private static readonly string ConfigPath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

    // Load config from file or create default if missing
    public static Config Load()
    {
        if (File.Exists(ConfigPath))
        {
            string json = File.ReadAllText(ConfigPath);
            return JsonSerializer.Deserialize<Config>(json);
        }

        var defaultConfig = new Config
        {
            BaseDirectory = $"C:\\Users\\{Environment.UserName}\\Documents\\XboxOneGames",
            GitHubPath = $"C:\\Users\\{Environment.UserName}\\Documents\\GitHub\\WinDurango",
            DebugMode = true,
            IgnoredFolders = new List<string> { "Archive", "Backup", "Logs" },
            DefaultScanPath = $"C:\\Users\\{Environment.UserName}\\Documents\\XboxOneGames",
            DeveloperMode = true
        };

        defaultConfig.Save(); // Save default config to file
        return defaultConfig;
    }

    // Save current config to file
    public void Save()
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ConfigPath, json);
    }
}
