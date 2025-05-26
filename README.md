# YLogger
LoggerSystem for Unity
# 📘 LoggerSystem for Unity

一个轻量级、高扩展性的 Unity 日志系统，支持控制台输出、文件保存、日志等级过滤、自动崩溃捕获、压缩上传、内置运行时面板等功能，适用于移动端与 PC 平台的调试和线上日志采集需求。

---

## ✨ 功能特性

- ✅ 支持日志等级（Info / Warning / Error）
- ✅ 支持写入本地日志文件（按天分文件）
- ✅ 异步写入日志，低开销
- ✅ 自动捕获崩溃日志（包括未捕获异常）
- ✅ 支持压缩（ZIP）与远程上传日志
- ✅ 游戏内日志查看器（InGameLogViewer）
- ✅ 支持设备信息采集（设备型号、系统版本等）
- ✅ 编辑器菜单内置日志浏览器（LogFileViewerWindow）

---

## 📂 文件结构

```plaintext
LoggerSystem/
├── Scripts/
│   ├── Logger.cs
│   ├── LogUploader.cs
│   ├── LogCompressor.cs
│   ├── CrashLogHelper.cs
│   ├── InGameLogViewer.cs
│   ├── LogInitializer.cs
│   └── DeviceInfoProvider.cs
├── Editor/
│   └── LogFileViewerWindow.cs
├── Prefabs/
│   └── InGameLogViewer.prefab
├── Resources/
│   └── LoggerSettings.asset
└── README.md
