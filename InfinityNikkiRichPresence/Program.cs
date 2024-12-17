using System;
using System.Threading;

class Program
{
    public static Config config;

    static void Main()
    {
        // Prevention of Multiple RichPresence.exe's
        var mutex = new Mutex(true, "RichPresenceMutex", out bool isNewInstance);
        if (!isNewInstance)
        {
            Console.WriteLine("Another instance of the RichPresence application is already running.");
            return;
        }

        // Try to load or create the config file
        config = ConfigManager.LoadOrCreateConfig();

        if (config == null)
        {
            Console.WriteLine("Error: Config could not be loaded or created. Exiting program.");
            return;
        }

        // If legacy playtime.json exists, merge it into config.json
        LegacyVersionMerge();

        // Initialize Rich Presence manager
        RichPresenceManager.Initialize(config);

        Logger.Log(1, "Monitoring Infinity Nikki game... Press any key while inside this tab to exit gracefully.");
        Console.ReadKey();

        ConfigManager.BackupConfig(config);
    }

    static void LegacyVersionMerge()
    {
        // Check if playtime.json exists and merge it into config.json
        if (System.IO.File.Exists("playtime.json"))
        {
            string legacyJson = System.IO.File.ReadAllText("playtime.json");
            dynamic legacyData = Newtonsoft.Json.JsonConvert.DeserializeObject(legacyJson);
            config.PlaytimeElapsed = legacyData.ElapsedTime;  // Merge playtime data

            System.IO.File.Delete("playtime.json");

            ConfigManager.SaveConfig(config);  // Save after merging legacy playtime

            Logger.Log(1, "Legacy playtime value found! Merged into the new config file.");
        }
    }
}
