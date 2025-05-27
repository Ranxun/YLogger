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

        [MenuItem("Tools/��־�鿴��")]
        public static void ShowWindow()
        {
            GetWindow<LogViewerWindow>("��־�鿴��");
        }

        private void OnGUI()
        {
            string logDir = Path.Combine(Application.persistentDataPath, "Logs");
            if (!Directory.Exists(logDir))
            {
                GUILayout.Label("δ�ҵ���־Ŀ¼");
                return;
            }

            var files = Directory.GetFiles(logDir, "*.log");
            foreach (var file in files)
            {
                if (GUILayout.Button(Path.GetFileName(file)))
                {
                    selectedFile = file;
                    logContent = File.ReadAllText(file);
                    // ���� Base64 ����
                    try
                    {
                        byte[] decodedBytes = Convert.FromBase64String(logContent);
                        logContent = System.Text.Encoding.UTF8.GetString(decodedBytes);
                    }
                    catch
                    {
                        //logContent = logContent; // ������� Base64���򲿷ֻ�����ݣ���ԭ����ʾ
                    }
                }
            }

            GUILayout.Space(10);
            scroll = EditorGUILayout.BeginScrollView(scroll);
            EditorGUILayout.TextArea(logContent ?? "ѡ����־�ļ��Բ鿴", GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();
        }
    }
}

#endif
