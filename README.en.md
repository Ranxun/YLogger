YLogger Logging System

YLogger is a lightweight logging and viewing solution for Unity projects. It supports runtime file logging, in-game and editor log viewing, log uploading, encryption, and configuration.

✨ Features

✅ Multi-level log recording (Log, Warning, Error, Exception)

✅ Supports writing to local files (daily rotation by level)

✅ Configurable output to console, file, encryption toggle, auto-clean

✅ Supports log uploading and compression

✅ In-game LoggerPanel for viewing and uploading logs

✅ Unity Editor tools: settings window & log viewer

📁 Directory Structure

YLogger/
├── Editor/                          # Editor-only tools
│   ├── LoggerSettingsWindow.cs     # Settings UI for logging configuration
│   └── LogViewerWindow.cs          # Editor log file viewer
├── Runtime/                         # Core runtime logic and config
│   ├── Scripts/                     # Logging functionality
│   │   ├── Logger.cs               # Logging interface
│   │   ├── LogUploader.cs          # Upload logic (extendable)
│   │   ├── LogCompressor.cs        # Compression logic
│   │   ├── CrashLogHelper.cs       # Crash log monitor
│   │   ├── MultiLogWriter.cs       # Multithreaded file writer
│   │   ├── LogInitializer.cs       # Initialization logic
│   │   └── LoggerPanel.cs          # In-game UI panel
│   └── Settings/                   # ScriptableObject-based settings
│       ├── LoggerConfig.cs         # SO class definition
│       └── LoggerSettings.cs       # Loader and accessor
├── Resources/
│   ├── LoggerPanel.prefab          # UI prefab for LoggerPanel
│   └── LoggerSettings.asset        # Logger configuration asset

⚙️ How to Use

Initialization

Call during your game startup:

LogInitializer.Initialize();

This will load LoggerSettings.asset from Resources and apply config.

Logging API

Logger.Log("Info message");
Logger.LogWarning("Warning message");
Logger.LogError("Error message");
Logger.LogException(new Exception("Exception message"));

Editor Tools

Logger Settings Window: Tools > Logger Settings

Log Viewer: Tools > Log Viewer

Runtime LoggerPanel

A draggable UI panel shown in Development/Editor builds

Toggle display by clicking the Log button

Remembers its screen position across runs

Supports viewing and uploading logs by severity level

🛠️ Extension Ideas

Custom upload endpoint integration

Add performance diagnostics (e.g. slow function tracking)

Export ZIP + user report data for QA

📌 Notes

Log file path: Application.persistentDataPath/Logs/

One file per day and level: log_2025-05-27.log, etc.

LoggerSettings.asset controls all toggles globally

📮 Feedback & Support

Feel free to submit issues or ideas for improvement!

