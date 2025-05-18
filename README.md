# WinDTool

**WinDTool** is a Windows desktop utility built for developers and power users working with Xbox One Translation Layer, DLL patching, and Appx package registration. It features robust file replacement workflows, smart logging, configurable scan paths, and developer mode awareness.

## ✨ Features

- 🔁 **DLL Replacement** – Automatically replace DLLs across multiple mounted game folders.
- 📦 **Appx Package Registration** – Register Appx packages directly through the UI using PowerShell integration.
- 🔍 **DLL Change Monitoring** – Detect and respond to modifications in watched DLLs.
- 📝 **Persistent Logging** – Colored log output with support for saving, clearing, and double-click escalation.
- ⚙️ **Settings Tab** – Configure paths and preferences through a clean tabbed interface.
- 🛠️ **Developer Mode Check** – Detect and optionally enable Windows Developer Mode if not already set.

## 📷 UI Overview

| DLL Replacer | Appx Installer | Settings |

## 🚀 Getting Started

### Prerequisites

- .NET Framework 4.8 or higher
- Windows 10/11
- Administrator privileges (for some features like Appx registration or Developer Mode)

### Running

1. Clone the repo:
   ```bash
   git clone https://github.com/oSerenity/WinDTool.git
###⚙️ Configuration Example:
```json
{
  "BaseDirectory": "C:\\Users\\yourname\\Documents\\XboxOneGames",
  "GitHubPath": "C:\\Users\\yourname\\Documents\\GitHub\\WinDurango",
  "DebugMode": true,
  "IgnoredFolders": [
    "Archive",
    "Backup",
    "Logs"
  ],
  "DefaultScanPath": "C:\\Users\\yourname\\Documents\\XboxOneGames",
  "DeveloperMode": true
}
