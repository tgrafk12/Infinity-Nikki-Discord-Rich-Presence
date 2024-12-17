using System;
using System.IO;

public static class Logger
{
    static string logFilePath = $"Application{DateTime.Now:MM-dd-yy-HH-mm}.log";

    public static void Log(int level, string message)
    {
        // Check if config is null before accessing its properties
        if (Program.config == null)
        {
            Console.WriteLine("Error: Config is null/broken. Attempting to start anyway...");
            Console.WriteLine("Backup file will be used for now... (Total Playtime will not be saved)");
            return;
        }

        if (level > Program.config.LogLvl)
            return;

        string timestamp = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string logMessage = $"[{timestamp}] - {message}";

        // Console output
        Console.WriteLine(logMessage);

        // Write to log file
        try
        {
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }
}
