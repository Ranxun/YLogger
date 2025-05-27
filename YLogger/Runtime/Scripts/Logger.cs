using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace YLogger
{

    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Exception
    }
    public static class Logger 
    {
        private static MultiLogWriter logWriter;
        public static bool EnableConsole = true;
        public static bool EnableFile = false;
        public static LogLevel CurrentLevel = LogLevel.Debug;



        public static void Initialize(LoggerSettings settings = null)
        {
            if (settings == null)
                settings = LoggerConfig.Settings;

            EnableConsole = settings.enableConsole;
            EnableFile = settings.enableFile;

            if (EnableFile)
                logWriter = new MultiLogWriter(settings);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(string message)
        {
            LogInternal(message, LogLevel.Info);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogWarning(string message)
        {
            LogInternal(message, LogLevel.Warning);
        }

        public static void LogError(string message)
        {
            LogInternal(message, LogLevel.Error, forceFileOnlyInRelease: true);
        }

        public static void LogException(Exception ex)
        {
            LogInternal($"{ex.Message}\n{ex.StackTrace}", LogLevel.Exception, forceFileOnlyInRelease: true);
        }

        private static void LogInternal(string message, LogLevel level, bool forceFileOnlyInRelease = false)
        {
            if (level < CurrentLevel) return;

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string prefix = $"[{time}] [{level}]";
            string formatted = $"{prefix} {message}";

            bool isRelease =
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            true;
#else
            false;
#endif

            if (EnableConsole && !(isRelease && forceFileOnlyInRelease))
            {
                string colored = formatted;
                switch (level)
                {
                    case LogLevel.Warning:
                        colored = $"<color=yellow>{formatted}</color>";
                        Debug.LogWarning(colored);
                        break;
                    case LogLevel.Error:
                    case LogLevel.Exception:
                        colored = $"<color=red>{formatted}</color>";
                        Debug.LogError(colored);
                        break;
                    default:
                        colored = $"<color=white>{formatted}</color>";
                        Debug.Log(colored);
                        break;
                }
            }

            if (EnableFile)
            {
                logWriter?.Write(formatted, level);
            }
        }


        public static void Close()
        {
#if !UNITY_WEBGL
            logWriter?.Close();
#endif
        }
    }
}
