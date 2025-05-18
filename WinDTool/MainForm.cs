using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using WinDTools;

namespace WinDTool
{
    public partial class MainForm : Form
    {
        private string BaseDirectory;
        private Config config;
        private string[] DebugOrRelease = new string[2]
        {
      "Release",
      "Debug"
        };
        private Dictionary<string, string> dllHashes = new Dictionary<string, string>();
        private Dictionary<string, DateTime> dllLastChangeTimes = new Dictionary<string, DateTime>();
        private Timer dllMonitorTimer;
        private string githubPath;
        private Timer idleCheckTimer;
        private HashSet<string> ignoredFolders;
        private int IsDebug;
        private List<string> selectedDllPaths = new List<string>();


        public MainForm()
        {
            InitializeComponent();
            LoadConfig();
            CheckGitHubPath();
            CheckDefaultScanPath();
            string str = IsDeveloperModeEnabled() ? "Enabled" : "Disabled";
            string text = Text;
            if (IsRunAsAdmin())
                Text = "Administrator: " + text + " Developer Mode: " + str;
            else
                Text = text + " Developer Mode: " + str;
        }

        private void AppendLogLine(string logLine, Color logColor)
        {

            txtLog.SelectionStart = txtLog.TextLength;
            txtLog.SelectionLength = 0;

            txtLog.SelectionColor = logColor;
            txtLog.AppendText(logLine + Environment.NewLine);
            txtLog.SelectionColor = txtLog.ForeColor; // Reset color
        }



        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                    return;
                txtFolderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string str = txtFolderPath.Text.Trim();
            if (!Directory.Exists(str))
                Logger.Log("The specified path does not exist.", LogLevel.ERROR);
            try
            {
                using (Process process = Process.Start(new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"Add-AppxPackage -Register '" + Path.Combine(str, "AppxManifest.xml") + "'\"",
                    WorkingDirectory = str,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }))
                {
                    string end1 = process.StandardOutput.ReadToEnd();
                    string end2 = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    if (process.ExitCode == 0)
                    {
                        Logger.Log("Appx package has been registered successfully!", LogLevel.SUCCESS);
                        if (string.IsNullOrWhiteSpace(end1))
                            return;
                        Logger.Log("Output: " + end1, LogLevel.INFO);
                    }
                    else
                        Logger.Log("Error registering Appx package: " + end2, LogLevel.ERROR);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Exception occurred: " + ex.Message, LogLevel.ERROR);
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (selectedDllPaths.Count != 0)
            {
                if (!string.IsNullOrEmpty(BaseDirectory))
                {
                    try
                    {

                        IEnumerable<string> targetDirectories = (Directory.GetDirectories(BaseDirectory, "*", SearchOption.AllDirectories)).Where((dir => Directory.Exists(Path.Combine(dir, "Mount")) && !ignoredFolders.Contains(new DirectoryInfo(dir).Name)));
                        Logger.Clear();
                        foreach (string targetPath in targetDirectories)
                        {
                            foreach (string dllPath in selectedDllPaths)
                            {
                                if (!File.Exists(dllPath))
                                    continue;
                                ReplaceDllFileInto(targetPath, dllPath);
                            }
                        }
                        MessageBox.Show("DLL replacement completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                }
            }
            MessageBox.Show("Please select at least one DLL file and the base directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void btnSelectBaseDirectory_Click(object sender, EventArgs e)
        {
            var selected = SelectFolder("Select Base Directory", txtBaseDirectory.Text);
            if (selected != null)
                 txtBaseDirectory.Text = selected;
        }

        private void btnSelectDll_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(githubPath))
                throw new Exception("Error: GitHub path does not exist.");
            //using it means we are using OpenFileDialog window
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            //create a file dialog window new means it is it's first time being used
            {
                openFileDialog.Filter = "DLL files (*.dll)|*.dll";
                openFileDialog.InitialDirectory = Path.Combine(githubPath, "x64", DebugOrRelease[IsDebug]);
                openFileDialog.Multiselect = true;
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                selectedDllPaths = (openFileDialog.FileNames).ToList<string>();
                txtDllPath.Text = string.Join(", ", selectedDllPaths);
            }
        }

        private void btnSetGitHubPath_Click(object sender, EventArgs e)
        {
            var selected = SelectFolder("Select GitHub Path", txtGitHubPath.Text);
            if (selected != null)
                txtGitHubPath.Text = selected;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logger.Clear();
            txtLog.Clear();
        }

        private void CheckDefaultScanPath()
        {
            if (Directory.Exists(BaseDirectory))
                txtDirectoryPath.Text = BaseDirectory;
            else
                Directory.CreateDirectory(BaseDirectory);
        }

        private void CheckDllModifications(object sender, EventArgs e)
        {
            bool anyChange = false;

            foreach (string dllPath in selectedDllPaths)
            {
                if (!File.Exists(dllPath))
                    continue;

                string currentHash = GetFileHash(dllPath);
                if (currentHash == null)
                    continue;

                if (dllHashes.TryGetValue(dllPath, out string previousHash))
                {
                    if (currentHash != previousHash)
                    {
                        dllHashes[dllPath] = currentHash;
                        dllLastChangeTimes[dllPath] = DateTime.Now;
                        Logger.Log($"[HASH] DLL modified: {Path.GetFileName(dllPath)}", LogLevel.WARNING);

                        anyChange = true;
                    }
                }
                else
                {
                    dllHashes[dllPath] = currentHash;
                }
            }

            if (anyChange)
            {
                idleCheckTimer?.Stop();
                idleCheckTimer?.Start();
            }
        }

        private void CheckGitHubPath()
        {
            txtDllPath.Text = githubPath;
            string path = Path.Combine(githubPath, "x64", DebugOrRelease[IsDebug]);
            if (Directory.Exists(path))
                return;
            Directory.CreateDirectory(path);
        }

        private void EnableDeveloperMode()
        {
            try
            {
                if (IsDeveloperModeEnabled())
                {
                    Logger.Log("Developer Mode is already enabled.", LogLevel.INFO);
                }
                else
                {
                    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\AppModelUnlock", "AllowDevelopmentWithoutDevLicense", (object)1, RegistryValueKind.DWord);
                    Logger.Log("Developer Mode enabled successfully!", LogLevel.SUCCESS);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error enabling Developer Mode: " + ex.Message, LogLevel.ERROR);
            }
        }

        private string GetApplicationName(string targetPath, string GamesFolder = "XboxOneGames")
        {
            int num = targetPath.LastIndexOf(GamesFolder, StringComparison.OrdinalIgnoreCase);
            return num == -1 ? Path.GetFileName(targetPath) : targetPath.Substring(num + GamesFolder.Length);
        }

        private string GetFileHash(string filePath)
        {
            try
            {
                byte[] fileBytes;
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
                {
                    fileBytes = new byte[stream.Length];
                    stream.Read(fileBytes, 0, fileBytes.Length);
                }

                var md5 = System.Security.Cryptography.MD5.Create();
                byte[] hash = md5.ComputeHash(fileBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
            catch (IOException ex)
            {
                Logger.Log($"Skipped hashing {Path.GetFileName(filePath)}: {ex.Message}", LogLevel.WARNING);
                return null;
            }
        }
        private void InitializeDllMonitor()
        {
            UpdateDllHashes();

            dllMonitorTimer = new Timer();
            dllMonitorTimer.Interval = 8000;
            dllMonitorTimer.Tick += CheckDllModifications;
            dllMonitorTimer.Start();

            idleCheckTimer = new Timer();
            idleCheckTimer.Interval = 100; // check every 2 seconds
            idleCheckTimer.Tick += OnIdleCheck;
        }


        private bool IsDeveloperModeEnabled()
        {
            object obj = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\AppModelUnlock", "AllowDevelopmentWithoutDevLicense", (object)null);
            return obj != null && obj is int num && num == 1;
        }

        private static bool IsRunAsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void LoadConfig()
        {
            config = Config.Load(); // Simplified loading call
            // Assign class fields from loaded config
            BaseDirectory = config.BaseDirectory.Replace("{USER}", Environment.UserName);
            githubPath = config.GitHubPath.Replace("{USER}", Environment.UserName);
            IsDebug = config.DebugMode ? 1 : 0;
            ignoredFolders = new HashSet<string>(config.IgnoredFolders);
        }

        private void LoadExistingLogs()
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
            if (!File.Exists(logFilePath)) return;

            string[] logs = File.ReadAllLines(logFilePath);

            foreach (string log in logs)
            {
                var level = Logger.ParseLogLevel(log);
                AppendLogLine(log, Logger.MapLogLevelToColor(level));
            }

            txtLog.SelectionStart = txtLog.TextLength;
            txtLog.ScrollToCaret();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupDllMonitoring();
            PopulateSettingsTab();
            Logger.OnLogMessage += AppendLogLine;
            LoadExistingLogs();
        }

        private void OnIdleCheck(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            var readyToReplace = dllLastChangeTimes
                .Where(kv => now - kv.Value > TimeSpan.FromMilliseconds(1))
                .Select(kv => kv.Key)
                .ToList();

            foreach (string dllPath in readyToReplace)
            {
                dllLastChangeTimes.Remove(dllPath);
                Logger.Log($"Replacing: {Path.GetFileName(dllPath)}", LogLevel.WARNING);

                foreach (string targetPath in Directory.GetDirectories(BaseDirectory, "*", SearchOption.AllDirectories)
                             .Where(dir => Directory.Exists(Path.Combine(dir, "Mount")) && !ignoredFolders.Contains(new DirectoryInfo(dir).Name)))
                {
                    ReplaceDllFileInto(targetPath, dllPath);
                }
            }

            if (dllLastChangeTimes.Count == 0)
            {
                idleCheckTimer.Stop();
            }
        }


        private void PopulateSettingsTab()
        {
            txtBaseDirectory.Text = config.BaseDirectory;
            txtGitHubPath.Text = config.GitHubPath;
        }
        private void ReplaceDllFileInto(string targetPath, string dllPath)
        {
            try
            {
                string fileName = Path.GetFileName(dllPath);
                string destDllPath = Path.Combine(targetPath, "Mount", fileName);
                string sourcePdb = Path.ChangeExtension(dllPath, ".pdb");
                string destPdb = Path.ChangeExtension(destDllPath, ".pdb");

                RetryFileCopy(dllPath, destDllPath);

                Logger.Log($"Replaced: " + GetApplicationName(destDllPath), LogLevel.INFO);

                if (File.Exists(sourcePdb))
                {
                    RetryFileCopy(sourcePdb, destPdb);
                    Logger.Log("Added PDB: " + Path.GetFileName(destPdb), LogLevel.INFO);
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"While replacing {dllPath}: {ex.Message}", LogLevel.ERROR);
            }
        }

        private string SelectFolder(string description, string initialPath)
        {
            var dialog = new FolderBrowserDialog
            {
                Description = description,
                SelectedPath = initialPath
            };
            return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : null;
        }
        private void RestartAsAdmin()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = Application.ExecutablePath,
                Verb = "runas"
            };
            try
            {
                Process.Start(startInfo);
                Close();
            }
            catch
            {
            }
        }

        private void RetryFileCopy(string sourcePath, string destPath, int maxRetries = 5, int delayMs = 500)
        {
            string tempPath = destPath + ".tmp";

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    // Copy to a temporary file first
                    File.Copy(sourcePath, tempPath, true);

                    if (File.Exists(destPath))
                        File.Delete(destPath);

                    File.Move(tempPath, destPath); // Atomic move
                    return;
                }
                catch (IOException ex)
                {
                    if (File.Exists(tempPath))
                    {
                        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }
                    }

                    if (attempt == maxRetries)
                        throw;

                    Logger.Log($"{Path.GetFileName(destPath)} is locked ({attempt}/{maxRetries}): {ex.Message}", LogLevel.WARNING);
                    System.Threading.Thread.Sleep(delayMs);
                }
            }

            throw new IOException($"Failed to replace {destPath} after {maxRetries} retries.");
        }

        private void SelectDefaultScanLocation(object sender, EventArgs e)
        {
            var selected = SelectFolder("Select the default scan location", BaseDirectory);
            if (selected != null)
                BaseDirectory = selected;
            txtDirectoryPath.Text = BaseDirectory;
            config.DefaultScanPath = selected;
            config.Save();
        }
        private void SetupDllMonitoring()
        {
            if (!Directory.Exists(githubPath))
                throw new Exception("Error: GitHub path does not exist.");
            else if (Directory.Exists(Path.Combine(githubPath, "x64", DebugOrRelease[IsDebug])))
            {

                List<string> dllFiles = Directory.GetFiles(Path.Combine(githubPath, "x64", DebugOrRelease[IsDebug]), "*.dll").ToList();
                if (dllFiles.Count > 0)
                {
                    selectedDllPaths = dllFiles;
                    txtDllPath.Text = string.Join(", ", selectedDllPaths);
                    InitializeDllMonitor();
                    Logger.Log("DLL files found in the directory:", LogLevel.INFO);
                }
            }
        }

        private void ssb_Click(object sender, EventArgs e)
        {
            config.BaseDirectory = txtBaseDirectory.Text.Trim();
            config.GitHubPath = txtGitHubPath.Text.Trim();
            config.Save();

            MessageBox.Show("Settings saved successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            Logger.Clear();
            if (tabControl1.SelectedTab == null || !(tabControl1.SelectedTab.Name == "tabPage2"))
                return;
            if (!IsRunAsAdmin())
            {
                tabPage2.Enabled = false;
                Logger.Log("Page Disabled, You are not running as administrator.", LogLevel.ERROR);
                Logger.Log("You Must Be administrator To Use This Feature.", LogLevel.INFO);
                Logger.Log("Please Double Click Me To run this application as an administrator.", LogLevel.INFO);
            }
            else
            {
                Logger.Log("Enabling Developer Mode...", LogLevel.WARNING);
                EnableDeveloperMode();
            }
        }

        private void txtLog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = txtLog.GetCharIndexFromPosition(e.Location);
            int line = txtLog.GetLineFromCharIndex(index);
            string clickedLine = txtLog.Lines.ElementAtOrDefault(line);

            if (!string.IsNullOrEmpty(clickedLine) && clickedLine.StartsWith("Please Double Click Me"))
            {
                RestartAsAdmin();
            }
        }


        private void UpdateDllHashes()
        {
            dllHashes.Clear();
            foreach (string dllPath in selectedDllPaths)
            {
                if (File.Exists(dllPath))
                {
                    dllHashes[dllPath] = GetFileHash(dllPath);
                }
            }
        }
    }
}
