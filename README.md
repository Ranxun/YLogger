🌐 [English README](README.en.md)
# YLogger 日志系统

YLogger 是一个适用于 Unity 项目的轻量级日志记录与查看系统，支持运行时文件记录、编辑器日志查看、日志上传与配置开关。

---

## ✨ 功能特性

- ✅ 多等级日志记录（Log、Warning、Error、Exception）
- ✅ 支持写入本地文件（自动每日分级）
- ✅ 可配置是否控制台输出、文件输出、日志加密等
- ✅ 支持日志上传、压缩、自动清除旧日志
- ✅ 提供运行时 `LoggerPanel` 可在游戏中查看并上传日志
- ✅ 提供 Unity 编辑器工具：日志设置窗口、日志文件查看器

---

## 📁 目录结构说明

```text
YLogger/
├── Editor/                          # 编辑器相关功能
│   ├── LoggerSettingsWindow.cs     # 编辑器中的日志配置窗口
│   └── LogViewerWindow.cs          # 日志文件浏览查看器
├── Runtime/                         # 运行时核心逻辑与配置
│   ├── Scripts/                     # 运行时日志功能脚本
│   │   ├── Logger.cs               # 日志统一封装入口
│   │   ├── LogUploader.cs          # 日志上传逻辑（可扩展上传服务）
│   │   ├── LogCompressor.cs        # 日志压缩（可配合上传）
│   │   ├── CrashLogHelper.cs       # 崩溃日志监听辅助类
│   │   ├── MultiLogWriter.cs       # 多线程日志文件写入器
│   │   ├── LogInitializer.cs       # 日志系统初始化
│   │   └── LoggerPanel.cs          # 运行时 UI 日志查看面板
│   └── Settings/                   # 配置定义及数据
│       ├── LoggerConfig.cs         # ScriptableObject 配置类
│       └── LoggerSettings.cs       # 静态加载配置的类
├── Resources/
│   ├── LoggerPanel.prefab          # LoggerPanel 的 UGUI 预制体
│   └── LoggerSettings.asset        # 配置实例文件（放置在 Resources 以供运行时加载）

📌 注意事项
- 文件日志写入路径为：Application.persistentDataPath/Logs/
- 日志文件每日生成，按等级命名（如：log_2025-05-27.log）
- LoggerSettings.asset 为全局控制开关，请勿遗漏

🛠️ 拓展建议
- 日志上传可自定义接入接口（LogUploader）
- 可接入性能分析插件（如帧率下降函数采集）
- 支持导出完整压缩包 + 自定义报错反馈信息

运行时日志面板
- LoggerPanel 为一个可拖拽的 UI 面板，运行时显示日志等级与上传按钮
- 仅在 UNITY_EDITOR 或 DEVELOPMENT_BUILD 下启用
- 日志位置可本地持久保存，下次运行时自动复位

编辑器功能
- 日志配置窗口：菜单栏 Tools > 日志配置
- 日志文件查看器：菜单栏 Tools > 日志查看器

使用日志 API
Logger.Log("普通日志");
Logger.LogWarning("警告信息");
Logger.LogError("错误信息");
Logger.LogException(new Exception("异常信息"));


