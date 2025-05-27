using UnityEngine;
namespace YLogger
{
    [CreateAssetMenu(fileName = "LoggerSettings", menuName = "Logger/Logger Settings")]
    public class LoggerSettings : ScriptableObject
    {
        public bool enableConsole = true;
        public bool enableFile = true;
        public bool encryptLog = false;

        public bool autoClearOldLogs = true;
        [Range(1, 30)]
        public int logRetentionDays = 7;

        public bool autoUploadLogs = false;
        public string uploadEndpoint = ""; // 留空则不上传
    }
}

