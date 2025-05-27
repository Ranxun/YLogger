using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
namespace YLogger
{
    public class LoggerPanel : MonoBehaviour
    {
        public Button toggleButton;
        public RectTransform panelRoot;
        public TMP_Dropdown logLevelDropdown;
        public TextMeshProUGUI logText;
        public Button prevPageButton;
        public Button nextPageButton;
        public Button uploadButton;
        public TextMeshProUGUI pageLabel;

        private Vector2 dragOffset;
        private bool dragging = false;
        private int currentPage = 0;
        private const int logsPerPage = 100;
        private List<string> currentLogs = new();
        private LogLevel selectedLevel = LogLevel.Info;
        private string logFilePath;

        private const string PositionXKey = "LoggerPanel_PosX";
        private const string PositionYKey = "LoggerPanel_PosY";

        void Awake()
        {
            RegisterExceptionHandler();
        }

        private void Start()
        {
            toggleButton.onClick.AddListener(TogglePanel);
            prevPageButton.onClick.AddListener(PrevPage);
            nextPageButton.onClick.AddListener(NextPage);
            uploadButton.onClick.AddListener(UploadCurrentLog);
            logLevelDropdown.onValueChanged.AddListener(OnLogLevelChanged);

            logLevelDropdown.ClearOptions();
            logLevelDropdown.AddOptions(Enum.GetNames(typeof(LogLevel)).ToList());

            panelRoot.gameObject.SetActive(false);
            LoadButtonPosition();
            RefreshLogs();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(
                toggleButton.GetComponent<RectTransform>(), Input.mousePosition))
            {
                dragging = true;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    toggleButton.GetComponent<RectTransform>(), Input.mousePosition, null, out dragOffset);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (dragging)
                {
                    dragging = false;
                    SaveButtonPosition();
                }
            }

            if (dragging)
            {
                Vector2 pos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    toggleButton.transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out pos);
                toggleButton.GetComponent<RectTransform>().anchoredPosition = pos - dragOffset;
            }
        }

        private void TogglePanel()
        {
            panelRoot.gameObject.SetActive(!panelRoot.gameObject.activeSelf);
            if (panelRoot.gameObject.activeSelf)
            {
                RefreshLogs();
            }
        }

        private void OnLogLevelChanged(int index)
        {
            selectedLevel = (LogLevel)index;
            RefreshLogs();
        }

        private void RefreshLogs()
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string logDir = Path.Combine(Application.persistentDataPath, "Logs");
            logFilePath = Path.Combine(logDir, $"{selectedLevel.ToString().ToLower()}_{date}.log");

            if (File.Exists(logFilePath))
            {
                currentLogs = File.ReadAllLines(logFilePath).ToList();
            }
            else
            {
                currentLogs = new List<string> { "未找到日志文件" };
            }

            currentPage = 0;
            ShowCurrentPage();
        }

        private void ShowCurrentPage()
        {
            int totalPages = Mathf.CeilToInt(currentLogs.Count / (float)logsPerPage);
            int start = currentPage * logsPerPage;
            int end = Mathf.Min(start + logsPerPage, currentLogs.Count);
            logText.text = string.Join("\n", currentLogs.GetRange(start, end - start));
            pageLabel.text = $"第 {currentPage + 1}/{Mathf.Max(totalPages, 1)} 页";
        }

        private void PrevPage()
        {
            if (currentPage > 0)
            {
                currentPage--;
                ShowCurrentPage();
            }
        }

        private void NextPage()
        {
            int totalPages = Mathf.CeilToInt(currentLogs.Count / (float)logsPerPage);
            if (currentPage < totalPages - 1)
            {
                currentPage++;
                ShowCurrentPage();
            }
        }

        private void UploadCurrentLog()
        {
            if (File.Exists(logFilePath))
            {
                Debug.Log($"[模拟上传] 上传日志文件: {logFilePath}");
                // TODO: 实现真实上传逻辑，比如调用 HTTP 上传接口
            }
            else
            {
                Debug.LogWarning("日志文件不存在，无法上传");
            }
        }

        private void SaveButtonPosition()
        {
            var pos = toggleButton.GetComponent<RectTransform>().anchoredPosition;
            PlayerPrefs.SetFloat(PositionXKey, pos.x);
            PlayerPrefs.SetFloat(PositionYKey, pos.y);
            PlayerPrefs.Save();
        }

        private void LoadButtonPosition()
        {
            if (PlayerPrefs.HasKey(PositionXKey))
            {
                float x = PlayerPrefs.GetFloat(PositionXKey);
                float y = PlayerPrefs.GetFloat(PositionYKey);
                toggleButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            }
        }
        void RegisterExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Exception e = args.ExceptionObject as Exception;
                if (e != null)
                    Logger.LogError($"[UnhandledException] {e.Message}\n{e.StackTrace}");
            };

            Application.logMessageReceived += (condition, stackTrace, type) =>
            {
                if (type == LogType.Exception)
                    Logger.LogError($"[Exception] {condition}\n{stackTrace}");
            };
        }
        
    }
}
#endif


