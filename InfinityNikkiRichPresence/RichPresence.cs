using DiscordRPC;
using System;
using System.Diagnostics;
using System.Threading;

public static class RichPresenceManager
{
    static DiscordRpcClient client;
    static Stopwatch stopwatch;
    static bool isGameRunning = false;
    static string gameProcessName = "X6Game-Win64-Shipping";
    static Config config;

    public static void Initialize(Config appConfig)
    {
        config = appConfig;
        Thread gameDetectionThread = new Thread(GameDetectionLoop);
        gameDetectionThread.IsBackground = true;
        gameDetectionThread.Start();
    }

    static void GameDetectionLoop()
    {
        while (true)
        {
            Process[] processes = Process.GetProcessesByName(gameProcessName);

            if (processes.Length > 0 && !isGameRunning)
            {
                Logger.Log(1, "Infinity Nikki was started! Calling RichPresence to Discord! We have Contact!");
                isGameRunning = true;
                stopwatch = new Stopwatch();
                stopwatch.Start();

                client = new DiscordRpcClient("1318345278088024128"); // My application ID
                client.Initialize();
            }
            else if (processes.Length == 0 && isGameRunning)
            {
                Logger.Log(1, "Infinity Nikki was stopped. RichPresence to Discord! I stopped playing, you may stop running now!");
                isGameRunning = false;
                stopwatch.Stop();
                config.PlaytimeElapsed += stopwatch.Elapsed;
                ConfigManager.SaveConfig(config);
                stopwatch.Reset();

                client.ClearPresence();
            }

            if (isGameRunning)
            {
                UpdateRichPresence();
            }

            Thread.Sleep(5000);
        }
    }

    static void UpdateRichPresence()
    {
        TimeSpan totalPlaytimeElapsed = config.PlaytimeElapsed + stopwatch.Elapsed;

        Logger.Log(2, $"Updating Rich Presence: Total Playtime: {totalPlaytimeElapsed.Hours}h {totalPlaytimeElapsed.Minutes}m");

        client.SetPresence(new RichPresence()
        {
            Details = "Playing Infinity Nikki",
            State = $"Total Playtime: {totalPlaytimeElapsed.Hours}h {totalPlaytimeElapsed.Minutes}m",
            Timestamps = new Timestamps() { Start = DateTime.UtcNow - stopwatch.Elapsed },
            Buttons = new[]
            {
                new Button()
                {
                    Label = "Download Now!",
                    Url = "https://infinitynikki.infoldgames.com/en/home"
                }
            }
        });

        Logger.Log(2, "Rich Presence was updated.");
    }
}
