using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Maple._04._Manager._04._Log
{
    public enum LogLevel
    {
        DEBUG,
        INFO,
        WARNING,
        ERROR,
        FATAL,
        END
    }

    internal class LogManager
    {
        //========================================================================= 
        // 매니저 초기화
        //========================================================================= 
        private static LogManager? instance;
        public static LogManager Instance => instance ??= new LogManager();

        private LogLevel currentLogLevel;
        private string logFilePath;

        public LogManager(string logFileName = "application.log")
        {
            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFileName);
        }

        public void Log(LogLevel logLevel, string message)
        {
            // 현재 로깅 수준이 메시지의 로깅 수준 이상인 경우에만 기록
            if (logLevel >= currentLogLevel)
            {
                
                string logMessage = $"[{DateTime.Now}] [{logLevel}] {message}";
                //Console.WriteLine(logMessage); // 콘솔에도 출력
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine); // 파일에도 기록, [ 동기 - 블로킹 ]으로 작동함.
            }
        }

        public void SetLogLevel(LogLevel logLevel)
        {
            currentLogLevel = logLevel;
        }
    }
}
