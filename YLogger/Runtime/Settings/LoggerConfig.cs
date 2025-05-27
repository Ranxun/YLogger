using UnityEngine;
namespace YLogger
{
    public static class LoggerConfig
    {
        private static LoggerSettings settings;

        public static LoggerSettings Settings
        {
            get
            {
                if (settings == null)
                {
#if UNITY_EDITOR
                    string[] guids = UnityEditor.AssetDatabase.FindAssets("t:LoggerSettings");
                    if (guids.Length > 0)
                    {
                        string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                        settings = UnityEditor.AssetDatabase.LoadAssetAtPath<LoggerSettings>(path);
                    }
#else
                settings = Resources.Load<LoggerSettings>("LoggerSettings");
#endif
                }
                return settings;
            }
        }
    }

}
