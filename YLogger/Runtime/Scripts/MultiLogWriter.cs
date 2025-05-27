/// <summary>
/// MultiLogWriter - 用于后台线程写日志到文件
/// 本功能仅适用于支持文件系统和多线程的平台（非WebGL）
/// </summary>

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

namespace YLogger
{
    public class MultiLogWriter
    {
        private readonly ConcurrentQueue<(LogLevel level, string msg)> queue = new();
#if !UNITY_WEBGL
        private readonly Thread thread;
        private bool running = true;
        private string logDir;
        private readonly HashSet<string> writtenFiles = new();
#endif
        private readonly LoggerSettings config;
        private string deviceInfo;
        public MultiLogWriter(LoggerSettings settings)
        {
            config = settings;
#if !UNITY_WEBGL
            logDir = Path.Combine(Application.persistentDataPath, "Logs");
            Directory.CreateDirectory(logDir);

            // 主线程调用，缓存设备信息
            deviceInfo = GetDeviceInfo();

            if (config.autoClearOldLogs)
                ClearOldLogs(config.logRetentionDays);

            thread = new Thread(WriteLoop) { IsBackground = true };
            thread.Start();
#else
            Debug.LogWarning("MultiLogWriter is disabled on WebGL.");
#endif

        }

        public void Write(string message, LogLevel level)
        {
            queue.Enqueue((level, message));
        }
#if !UNITY_WEBGL
        private void WriteLoop()
        {
            while (running || !queue.IsEmpty)
            {
                if (queue.TryDequeue(out var item))
                {
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    string levelFile = Path.Combine(logDir, $"{item.level.ToString().ToLower()}_{date}.log");
                    bool needWriteHeader = false;

                    lock (writtenFiles)
                    {
                        if (!writtenFiles.Contains(levelFile))
                        {
                            writtenFiles.Add(levelFile);
                            needWriteHeader = true;
                        }
                    }

                    using (var sw = new StreamWriter(levelFile, true))
                    {
                        if (needWriteHeader)
                        {
                            sw.WriteLine(deviceInfo);
                        }
                        sw.WriteLine(Encrypt(item.msg));
                    }
                }
                else
                {
                    Thread.Sleep(10);
                }
            }
        }

        private string Encrypt(string input)
        {
            if (config.encryptLog)
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(input);
                return Convert.ToBase64String(data);
            }
            return input;
        }

        private void ClearOldLogs(int days)
        {
            var files = Directory.GetFiles(logDir, "*.log");
            foreach (var file in files)
            {
                if (File.GetCreationTime(file) < DateTime.Now.AddDays(-days))
                    File.Delete(file);
            }
        }

        public void Close()
        {
            running = false;
            if (!thread.Join(3000))
            {
                Debug.LogWarning("Logger thread did not finish in time. Some logs might be lost.");
            }
        }
#endif


        private static string GetDeviceInfo()
        {
            return $"Device: {SystemInfo.deviceModel}\n" +
                   $"OS: {SystemInfo.operatingSystem}\n" +
                   $"AppVersion: {Application.version}\n" +
                   $"Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                   $"----------------------------------------";
        }
    }

}
