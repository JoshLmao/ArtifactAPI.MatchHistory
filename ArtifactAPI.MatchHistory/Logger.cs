using System;
using System.IO;

namespace ArtifactAPI.MatchHistory
{
    /// <summary>
    /// Simple logger class to track output messages like crashes and useful information
    /// </summary>
    public class Logger
    {
        private static string FILE_NAME = "output.txt";
        private static string FILE_PATH = null;

        static bool IS_CONFIGURED = false;

        public Logger()
        {
            Setup();
        }

        public static void OutputInfo(string message)
        {
            if (!IS_CONFIGURED)
                Setup();

            string formatted = $"{DateTime.Now} -- Info -- {message}{Environment.NewLine}";
            Append(formatted);
        }

        public static void OutputError(string message)
        {
            if (!IS_CONFIGURED)
                Setup();

            string formatted = $"{DateTime.Now} -- Error -- {message}{Environment.NewLine}";
            Append(formatted);
        }

        private static void Append(string message)
        {
            File.AppendAllText(FILE_PATH, message);
        }

        private static void Setup()
        {
            FILE_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILE_NAME);
            if (string.IsNullOrEmpty(FILE_PATH))
                return;

            if (!File.Exists(FILE_PATH))
            {
                File.Create(FILE_PATH).Close();
            }

            Append($"------{Environment.NewLine}");

            IS_CONFIGURED = true;
        }
    }
}
