using DiscordRPC;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

class Program
{
    static DiscordRpcClient client;
    static Stopwatch stopwatch;
    static string gameProcessName = "X6Game-Win64-Shipping";
    static bool isGameRunning = false;
    static TotalPlaytime totalPlaytime;
    static string playtimeFilePath = "playtime.json";

    static void Main()
    {
        totalPlaytime = LoadTotalPlaytime();

        Thread gameDetectionThread = new Thread(GameDetectionLoop);
        gameDetectionThread.IsBackground = true;
        gameDetectionThread.Start();

        Console.WriteLine("Monitoring game... Press any key to exit.");
        Console.ReadKey();

        client?.Dispose();
    }

    static void GameDetectionLoop()
    {
        while (true)
        {
            Process[] processes = Process.GetProcessesByName(gameProcessName);

            if (processes.Length > 0 && !isGameRunning)
            {
                Console.WriteLine("Game started! Running Call to make RichPresence!");
                isGameRunning = true;
                stopwatch = new Stopwatch();
                stopwatch.Start();

                client = new DiscordRpcClient("1318345278088024128");
                client.Initialize();
            }
            else if (processes.Length == 0 && isGameRunning)
            {
                Console.WriteLine("Game stopped! Running Call to Stop RichPresence!");
                isGameRunning = false;
                stopwatch.Stop();
                totalPlaytime.ElapsedTime += stopwatch.Elapsed;
                SaveTotalPlaytime();
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

        TimeSpan totalPlaytimeElapsed = totalPlaytime.ElapsedTime + stopwatch.Elapsed;


        client.SetPresence(new RichPresence()
        {
            Details = "Playing Infinity Nikki",
            State = $"Total Playtime: {totalPlaytimeElapsed.Hours}h {totalPlaytimeElapsed.Minutes}m",
            Timestamps = new Timestamps() { Start = DateTime.UtcNow - stopwatch.Elapsed },
        });
    }


    static TotalPlaytime LoadTotalPlaytime()
    {
        if (File.Exists(playtimeFilePath))
        {
            string json = File.ReadAllText(playtimeFilePath);
            return JsonConvert.DeserializeObject<TotalPlaytime>(json);
        }
        else
        {
            return new TotalPlaytime { ElapsedTime = TimeSpan.Zero };
        }
    }

    static void SaveTotalPlaytime()
    {
        string json = JsonConvert.SerializeObject(totalPlaytime, Formatting.Indented);
        File.WriteAllText(playtimeFilePath, json);
    }
}

public class TotalPlaytime
{
    public TimeSpan ElapsedTime { get; set; }
}
