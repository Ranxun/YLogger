#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

namespace YLogger
{
    public class LogViewerWindow : EditorWindow
    {
        private Vector2 scroll;
        private string logContent;
        private string selectedFile;

        [MenuItem("Tools/日志查看器")]
        public static void ShowWindow()
        {
            GetWindow<LogViewerWindow>("日志查看器");
        }

        private void OnGUI()
        {
            string logDir = Path.Combine(Application.persistentDataPath, "Logs");
            if (!Directory.Exists(logDir))
            {
                GUILayout.Label("未找到日志目录");
                return;
            }

            var files = Directory.GetFiles(logDir, "*.log");
            foreach (var file in files)
            {
                if (GUILayout.Button(Path.GetFileName(file)))
                {
                    selectedFile = file;
                    logContent = File.ReadAllText(file);
                    // 尝试 Base64 解码
                    try
                    {
                        byte[] decodedBytes = Convert.FromBase64String(logContent);
                        logContent = System.Text.Encoding.UTF8.GetString(decodedBytes);
                    }
                    catch
                    {
                        //logContent = logContent; // 如果不是 Base64（或部分混合内容），原样显示
                    }
                }
            }

            GUILayout.Space(10);
            scroll = EditorGUILayout.BeginScrollView(scroll);
            EditorGUILayout.TextArea(logContent ?? "选择日志文件以查看", GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();
        }
    }
}

#endif
