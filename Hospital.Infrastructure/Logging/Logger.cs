using System;
using System.IO;

namespace Hospital.Infrastructure.Logging;

public static class Logger
{
    // This is where LogFileName MUST be declared
    private const string LogFileName = "errorlog.txt";

    public static void Log(Exception ex)
    {
        var line =
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";

        try
        {
            File.AppendAllText(LogFileName, line);
        }
        catch
        {
            // Swallow logging errors to avoid crashing the app if the file can't be written.
        }
    }
}