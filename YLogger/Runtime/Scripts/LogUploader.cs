using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace YLogger
{
    public static class LogUploader
    {
        public static IEnumerator UploadLogFile(string filePath, string uploadUrl)
        {
#if UNITY_WEBGL
            Debug.LogWarning("WebGL does not support local file uploads.");
            yield break;
#else
            if (!File.Exists(filePath))
            {
                Logger.LogError($"日志文件不存在：{filePath}");
                yield break;
            }

            byte[] fileData = File.ReadAllBytes(filePath);
            UnityWebRequest request = new UnityWebRequest(uploadUrl, UnityWebRequest.kHttpVerbPOST);
            UploadHandlerRaw uploader = new UploadHandlerRaw(fileData);
            uploader.contentType = "application/octet-stream";
            request.uploadHandler = uploader;
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Logger.Log("日志上传成功");
            }
            else
            {
                Logger.LogError($"日志上传失败: {request.error}");
            }
#endif

        }

        public static void ClearOldLogs(int days = 7)
        {
            string logDir = Path.Combine(Application.persistentDataPath, "Logs");
            if (!Directory.Exists(logDir)) return;

            foreach (var file in Directory.GetFiles(logDir))
            {
                DateTime lastWrite = File.GetLastWriteTime(file);
                if ((DateTime.Now - lastWrite).TotalDays > days)
                {
                    File.Delete(file);
                }
            }
        }
    }
}
