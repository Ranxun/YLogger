# YLogger - Unity In-Game Logging System

🌐 [中文文档 (Chinese)](README.md)

## Introduction

YLogger is a lightweight and customizable in-game logging system for Unity. It provides file-based logs, in-game UI log viewer, configurable logging levels, and uploading support.

---

## Features

* ✅ **Multi-level Logging:** Supports Log, Warning, Error, Exception, Assert.
* ✅ **Output Channels:** Console and file logging can be enabled/disabled independently.
* ✅ **File Management:** Logs are saved daily by level, and support auto-cleaning after 7 days.
* ✅ **In-Game Viewer:** UI panel for runtime log viewing, filtering, paging, and uploading.
* ✅ **Editor Tools:** Includes an editor viewer and configuration window.
* ✅ **Optional Encryption:** Simple Base64 encoding for log content.

---

## Directory Structure

```
YLogger/
├── Editor/                     # Editor tools
│   ├── LoggerSettingsWindow.cs
│   └── LogViewerWindow.cs
├── Runtime/                    # Runtime components
│   ├── Scripts/                # Logging logic
│   │   ├── Logger.cs
│   │   ├── LogUploader.cs
│   │   ├── LogCompressor.cs
│   │   ├── CrashLogHelper.cs
│   │   ├── MultiLogWriter.cs
│   │   └── LoggerPanel.cs
│   └── Settings/              # ScriptableObject assets
│       ├── LoggerConfig.cs
│       └── LoggerSettings.cs
├── Resources/
│   ├── LoggerPanel.prefab     # In-game log panel prefab
│   └── LoggerSettings.asset   # Serialized user configuration
```

---

## Usage Notes
* WebGL is not currently supported
* Only enable encryption in production if security is needed.
* Auto-cleaning and uploading should be toggled according to your release process.
* Logs are stored under `Application.persistentDataPath/Logs/`.

---

## Extension Ideas

* ⚡ Add FPS drops and slow method tracing.
* 🔹 Integrate crash reporting platforms (e.g., Backtrace, Sentry).
* 💡 Add filters, search, and advanced pagination to the UI.
* ✉ Export logs via email or clipboard for easier sharing.

---

## License

MIT License. Free to use and modify in personal or commercial Unity projects.

---

Made with ❤⃣ for Unity Developers.
