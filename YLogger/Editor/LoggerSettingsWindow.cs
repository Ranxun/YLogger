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

        [MenuItem("Tools/Logger �������")]
        public static void OpenWindow()
        {
            GetWindow<LoggerSettingsWindow>("Logger ����");
        }

        private void OnEnable()
        {
            LoadOrCreateSettings();
        }

        private void OnGUI()
        {
            if (settings == null)
            {
                EditorGUILayout.HelpBox("LoggerSettings ��Դ����ʧ�ܡ�", MessageType.Error);
                if (GUILayout.Button("���¼���"))
                    LoadOrCreateSettings();
                return;
            }

            EditorGUILayout.LabelField("��־����", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            settings.enableConsole = EditorGUILayout.Toggle("���ÿ���̨���", settings.enableConsole);
            settings.enableFile = EditorGUILayout.Toggle("�����ļ�д��", settings.enableFile);
            settings.encryptLog = EditorGUILayout.Toggle("��־����", settings.encryptLog);

            EditorGUILayout.Space();
            settings.autoClearOldLogs = EditorGUILayout.Toggle("�Զ��������־", settings.autoClearOldLogs);
            if (settings.autoClearOldLogs)
            {
                EditorGUI.indentLevel++;
                settings.logRetentionDays = EditorGUILayout.IntSlider("��������", settings.logRetentionDays, 1, 30);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();
            settings.autoUploadLogs = EditorGUILayout.Toggle("�Զ��ϴ���־", settings.autoUploadLogs);
            if (settings.autoUploadLogs)
            {
                EditorGUI.indentLevel++;
                settings.uploadEndpoint = EditorGUILayout.TextField("�ϴ���ַ", settings.uploadEndpoint);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("��������"))
            {
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
            }
            if (GUILayout.Button("��λ�ļ�"))
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
                Debug.Log("�Ѵ���Ĭ�� LoggerSettings.asset");
            }
        }
    }
}

#endif
