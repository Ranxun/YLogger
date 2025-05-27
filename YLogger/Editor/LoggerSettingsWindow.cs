#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

namespace YLogger
{
    public class LoggerSettingsWindow : EditorWindow
    {
        private const string SettingsPath = "Assets/Resources/LoggerSettings.asset";
        private LoggerSettings settings;

        [MenuItem("Tools/Logger 设置面板")]
        public static void OpenWindow()
        {
            GetWindow<LoggerSettingsWindow>("Logger 配置");
        }

        private void OnEnable()
        {
            LoadOrCreateSettings();
        }

        private void OnGUI()
        {
            if (settings == null)
            {
                EditorGUILayout.HelpBox("LoggerSettings 资源加载失败。", MessageType.Error);
                if (GUILayout.Button("重新加载"))
                    LoadOrCreateSettings();
                return;
            }

            EditorGUILayout.LabelField("日志配置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            settings.enableConsole = EditorGUILayout.Toggle("启用控制台输出", settings.enableConsole);
            settings.enableFile = EditorGUILayout.Toggle("启用文件写入", settings.enableFile);
            settings.encryptLog = EditorGUILayout.Toggle("日志加密", settings.encryptLog);

            EditorGUILayout.Space();
            settings.autoClearOldLogs = EditorGUILayout.Toggle("自动清除旧日志", settings.autoClearOldLogs);
            if (settings.autoClearOldLogs)
            {
                EditorGUI.indentLevel++;
                settings.logRetentionDays = EditorGUILayout.IntSlider("保留天数", settings.logRetentionDays, 1, 30);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();
            settings.autoUploadLogs = EditorGUILayout.Toggle("自动上传日志", settings.autoUploadLogs);
            if (settings.autoUploadLogs)
            {
                EditorGUI.indentLevel++;
                settings.uploadEndpoint = EditorGUILayout.TextField("上传地址", settings.uploadEndpoint);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("保存设置"))
            {
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
            }
            if (GUILayout.Button("定位文件"))
            {
                Selection.activeObject = settings;
                EditorGUIUtility.PingObject(settings);
            }
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(settings);
            }
        }

        private void LoadOrCreateSettings()
        {
            settings = AssetDatabase.LoadAssetAtPath<LoggerSettings>(SettingsPath);

            if (settings == null)
            {
                settings = CreateInstance<LoggerSettings>();
                Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath));
                AssetDatabase.CreateAsset(settings, SettingsPath);
                AssetDatabase.SaveAssets();
                Debug.Log("已创建默认 LoggerSettings.asset");
            }
        }
    }
}

#endif
