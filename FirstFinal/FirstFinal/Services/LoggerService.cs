using FirstFinal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFinal.Services
{
    public class LoggerService : ILoggerService
    {
        private const string LogFilePath = "C:\\Users\\n.kipshidze\\Desktop\\FirstFinal\\FirstFinal\\FirstFinal\\Data\\logs.log";


        public void LogInfo(string message)
        {
            Log($"INFO: {message}");
        }


        public void LogError(string message)
        {
            Log($"ERROR: {message}");
        }


        private void Log(string message)
        {
            var logEntry = $"[{DateTime.Now}] {message}{Environment.NewLine}";
            File.AppendAllText(LogFilePath, logEntry);
        }
    }
}
