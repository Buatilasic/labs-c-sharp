public class Log
{
    private static readonly string _logFilePath = "Logs.txt";

    public void ConsoleM(string message, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {message}");
        Console.ResetColor();

        WriteLog(message);
    }

    public void Debug(string message)
    {
        ConsoleM($"DEBUG: {message}", ConsoleColor.Cyan);
    }

    public void Info(string message)
    {
        ConsoleM($"INFO: {message}", ConsoleColor.Green);
    }

    public void Error(string message)
    {
        ConsoleM($": {message}", ConsoleColor.Red);
    }

    public void WriteLog(string message)
    {
        try
        {
            using (StreamWriter writer = File.AppendText(_logFilePath))
            {
                writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing to log file: " + ex.Message);
        }
    }
}
