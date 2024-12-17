using Newtonsoft.Json;
using System;
using System.IO;

public class Config
{
    public int LogLvl { get; set; }
    public TimeSpan PlaytimeElapsed { get; set; }
}

public static class ConfigManager
{
    static string configFilePath = "config.json";
    static string backupConfigFilePath = "config.backup";

    public static Config LoadOrCreateConfig()
    {
        try
        {
            if (File.Exists(configFilePath))
            {
                string json = File.ReadAllText(configFilePath);
                return JsonConvert.DeserializeObject<Config>(json);
            }
            else
            {
                Logger.Log(1, "Config file not found. Creating a new config.");
                return new Config { LogLvl = 1, PlaytimeElapsed = TimeSpan.Zero };
            }
        }
        catch (JsonException ex)
        {
            Logger.Log(1, $"Error loading a config: {ex.Message}. Attempting to load backup config...");

            if (File.Exists(backupConfigFilePath))
            {
                Logger.Log(1, "Backup config found. Reverting to backup configuration. Your playtime is safe, don't worry!");
                string backupJson = File.ReadAllText(backupConfigFilePath);
                Config backupConfig = JsonConvert.DeserializeObject<Config>(backupJson);

                SaveConfig(backupConfig);
                return backupConfig;
            }

            return null;
        }
    }

    public static void SaveConfig(Config configToSave)
    {
        if (configToSave != null)
        {
            string json = JsonConvert.SerializeObject(configToSave, Formatting.Indented);
            File.WriteAllText(configFilePath, json);
            Logger.Log(1, "Config file saved successfully.");
        }
        else
        {
            Logger.Log(1, "Config is null. Cannot save config.");
        }
    }

    public static void BackupConfig(Config config)
    {
        try
        {
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(backupConfigFilePath, json);
            Logger.Log(1, "Config file backed up successfully.");
        }
        catch (Exception ex)
        {
            Logger.Log(1, $"Error backing up config: {ex.Message}");
        }
    }
}
