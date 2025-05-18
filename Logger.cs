using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace WinDTools
{

    public enum LogLevel { SUCCESS, INFO, WARNING, ERROR }

    public static class Logger
    {
        private static string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");

        public static event Action<string, Color> OnLogMessage;

        public static void Log(string message, LogLevel level = LogLevel.INFO)
        {
            string formattedMessage = $"[{DateTime.Now:hh:mm:ss tt}] [{level}] {message}";

            // Write to log file (append)
            File.AppendAllText(logFilePath, formattedMessage + Environment.NewLine);

            // Trigger event to UI
            Color logColor = MapLogLevelToColor(level);
            formattedMessage = $"[{DateTime.Now:hh:mm:ss tt}] {message}";

            OnLogMessage?.Invoke(formattedMessage, logColor);
        }
        public static void LogException(Exception ex, string context = "")
        {
            string prefix = string.IsNullOrWhiteSpace(context) ? string.Empty : $"{context}: ";
            Log($"{prefix}{ex.GetType().Name} - {ex.Message}", LogLevel.ERROR);
        }

        public static LogLevel ParseLogLevel(string logLine)
        {
            if (logLine.Contains("[SUCCESS]")) return LogLevel.SUCCESS;
            if (logLine.Contains("[ERROR]")) return LogLevel.ERROR;
            if (logLine.Contains("[WARNING]")) return LogLevel.WARNING;
            return LogLevel.INFO;
        }
        public static string RemoveLevel(string log, LogLevel level)
        {
            if (log.Contains(level.ToString()))
                return log.Replace($"[{level}] ", string.Empty).Trim();
            else
                return log;
        }
        public static Color MapLogLevelToColor(LogLevel level)
        {
            Color logColor = Color.Black; // Default color
            if (level == LogLevel.SUCCESS)
                logColor = Color.Green;
            else if (level == LogLevel.ERROR)
                logColor = Color.Red;
            else if (level == LogLevel.WARNING)
                logColor = Color.Orange;
            else if (level == LogLevel.INFO)
                logColor = Color.Blue;
            return logColor;
        }

        public static void Clear()
        {
            File.WriteAllText(logFilePath, string.Empty);
            OnLogMessage?.Invoke("Logs cleared.", MapLogLevelToColor(LogLevel.SUCCESS));
        }
        public static void OpenLogFile()
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
            if (File.Exists(logFilePath))
                Process.Start("explorer.exe", logFilePath);
        }

    }

}
