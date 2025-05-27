using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace YLogger
{
    public static class CrashLogHelper
    {
        private static string crashFilePath = Path.Combine(Application.persistentDataPath, "crash_ylog.log");

        public static void SaveCrashLog(string message)
        {
            File.WriteAllText(crashFilePath, message);
        }

        public static string GetCrashLogPath()
        {
            return File.Exists(crashFilePath) ? crashFilePath : null;
        }

        public static void ClearCrashLog()
        {
            if (File.Exists(crashFilePath))
                File.Delete(crashFilePath);
        }
    }

}

