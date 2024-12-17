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
    static string gameProcessName = "X6Game-Win64-Shipping";  // The game process you want to monitor
    static bool isGameRunning = false;
    static TotalPlaytime totalPlaytime;
    static string playtimeFilePath = "playtime.json";  // File path to store the total playtime

    static void Main()
    {
        // Initialize total playtime from file
        totalPlaytime = LoadTotalPlaytime();

        // Start a background thread to check the game process
        Thread gameDetectionThread = new Thread(GameDetectionLoop);
        gameDetectionThread.IsBackground = true; // This ensures the thread stops when the main application exits
        gameDetectionThread.Start();

        Console.WriteLine("Monitoring game... Press any key to exit.");
        Console.ReadKey();  // Keeps the application running until the user presses a key

        // Clean up and dispose the Discord client when the application exits
        client?.Dispose();
    }

    static void GameDetectionLoop()
    {
        while (true)
        {
            Process[] processes = Process.GetProcessesByName(gameProcessName);

            if (processes.Length > 0 && !isGameRunning)
            {
                // Game has started and was not previously detected
                Console.WriteLine("Game started! Running Call to make RichPresence!");
                isGameRunning = true;
                stopwatch = new Stopwatch();
                stopwatch.Start();

                // Initialize Discord Rich Presence after detecting the game
                client = new DiscordRpcClient("1318345278088024128");  // Replace with your app ID
                client.Initialize();
            }
            else if (processes.Length == 0 && isGameRunning)
            {
                // Game has stopped, and it was previously running
                Console.WriteLine("Game stopped! Running Call to Stop RichPresence!");
                isGameRunning = false;
                stopwatch.Stop();
                totalPlaytime.ElapsedTime += stopwatch.Elapsed; // Update total playtime
                SaveTotalPlaytime(); // Save the updated total playtime
                stopwatch.Reset();

                // Clear Discord Rich Presence when the game is not running
                client.ClearPresence();
            }

            // Update Rich Presence every 5 seconds if the game is running
            if (isGameRunning)
            {
                UpdateRichPresence();
            }

            // Sleep for 5 seconds before checking again to prevent timeouts with Discord
            Thread.Sleep(5000);  // 5 seconds
        }
    }

    static void UpdateRichPresence()
    {
        // Calculate total accumulated playtime
        TimeSpan totalPlaytimeElapsed = totalPlaytime.ElapsedTime + stopwatch.Elapsed;

        // Update Rich Presence with total playtime only
        client.SetPresence(new RichPresence()
        {
            Details = "Playing Infinity Nikki",  // Game title
            State = $"Total Playtime: {totalPlaytimeElapsed.Hours}h {totalPlaytimeElapsed.Minutes}m", // Only show total playtime
            Timestamps = new Timestamps() { Start = DateTime.UtcNow - stopwatch.Elapsed },  // Session start time
        });
    }


    static TotalPlaytime LoadTotalPlaytime()
    {
        // Check if the playtime file exists
        if (File.Exists(playtimeFilePath))
        {
            // Read the file and deserialize the total playtime data
            string json = File.ReadAllText(playtimeFilePath);
            return JsonConvert.DeserializeObject<TotalPlaytime>(json);
        }
        else
        {
            // If the file doesn't exist, initialize total playtime to 0 hours and 0 minutes
            return new TotalPlaytime { ElapsedTime = TimeSpan.Zero };
        }
    }

    static void SaveTotalPlaytime()
    {
        // Serialize the total playtime to JSON and save it to the file
        string json = JsonConvert.SerializeObject(totalPlaytime, Formatting.Indented);
        File.WriteAllText(playtimeFilePath, json);
    }
}

// Class to hold the total playtime data
public class TotalPlaytime
{
    public TimeSpan ElapsedTime { get; set; }  // Total accumulated playtime
}
