# YLogger 日志系统

YLogger 是一个轻量、高扩展性的 Unity 内置日志采集系统，支持本地日志分类写入、UI 日志查看、日志上传、崩溃记录等功能，适用于开发、测试与线上调试。

## 功能特性

* 【多级别日志写入】按照 LogLevel (如: Info, Warning, Error) 分类写入日志文件
* 【日志打印控制】支持定制是否打印到 Console
* 【UI 日志面板】展示日志分类和分页，支持上传按钮
* 【异步写入】使用线程队列写入日志，保证性能
* 【日志加密】支持 Base64 简单加密
* 【日志自动清理】选择是否自动删除过日7天日志
* 【崩溃日志】自动捕获应用崩溃信息
* 【日志上传】可自定义上传方式
* 【Editor 日志查看器】在 Unity Editor 中打开日志文件
* 【配置文件.asset】日志配置持久化

## 目录结构说明

```
YLogger/
├── Editor/
│   ├── LoggerSettingsWindow.cs     # 编辑器配置 UI 面板
│   └── LogViewerWindow.cs         # 编辑器中查看日志的工具
├── Runtime/
│   └── Scripts/
│       ├── Logger.cs              # 日志入口类
│       ├── LogUploader.cs        # 自定义上传实现类
│       ├── LogCompressor.cs      # 可选的压缩工具类
│       ├── CrashLogHelper.cs     # 捕获崩溃信息的辅助类
│       ├── MultiLogWriter.cs     # 多级别日志写入器（多线程）
│       ├── LogInitializer.cs     # 自动初始化 Logger 的组件
│       └── LoggerPanel.cs        # UGUI 日志 UI 面板
│   └── Settings/
│       ├── LoggerConfig.cs       # 可序列化配置类
│       └── LoggerSettings.cs     # ScriptableObject 单例配置
├── Resources/
│   ├── LoggerPanel.prefab        # UGUI 面板预制体
│   └── LoggerSettings.asset      # 默认配置资源文件
```

## 使用说明

1. 将 `YLogger/` 拖入你的 Unity 项目
2. 在场景中添加 `LogInitializer` 脚本或调用 `Logger.Initialize()` 进行初始化
3. （可选）编辑 `LoggerSettings.asset` 或通过 `Tools/日志设置` 面板修改配置
4. 在需要的地方使用 `Logger.Log/Logger.LogError` 等方法进行日志记录

## 注意事项

* **日志位置**：所有日志默认写入 `Application.persistentDataPath/Logs/`
* **编辑器测试**：Editor 模式下默认启用控制台输出，Play 模式模拟移动端行为
* **自动清理**：配置中开启 `AutoCleanOldLogs` 会自动删除 7 天前的日志
* **日志等级**：Info/Warning/Error 分开写入不同文件，便于筛查
* **打包时包含设置资源**：确保 `LoggerSettings.asset` 放入 `Resources` 下

## 拓展建议

* 支持远程调试 / 自动上传服务器
* 支持帧率异常函数采样分析（可接入 Profiler）
* 支持云端远程配置开关

## 联系 & 许可

本项目可用于商用或私用，请注明出处。如有建议欢迎提交 PR 或 Issue。
