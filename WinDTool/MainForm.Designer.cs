using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace WinDTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnSelectDll = new System.Windows.Forms.Button();
            this.btnSelectDirectory = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.txtDllPath = new System.Windows.Forms.TextBox();
            this.txtDirectoryPath = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.btnSelectBaseDirectory = new System.Windows.Forms.Button();
            this.btnSelectGitHubPath = new System.Windows.Forms.Button();
            this.txtGitHubPath = new System.Windows.Forms.TextBox();
            this.txtBaseDirectory = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectDll
            // 
            this.btnSelectDll.Location = new System.Drawing.Point(5, 7);
            this.btnSelectDll.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectDll.Name = "btnSelectDll";
            this.btnSelectDll.Size = new System.Drawing.Size(112, 24);
            this.btnSelectDll.TabIndex = 0;
            this.btnSelectDll.Text = "Select DLL(s)";
            this.btnSelectDll.UseVisualStyleBackColor = true;
            this.btnSelectDll.Click += new System.EventHandler(this.btnSelectDll_Click);
            // 
            // btnSelectDirectory
            // 
            this.btnSelectDirectory.Location = new System.Drawing.Point(5, 36);
            this.btnSelectDirectory.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectDirectory.Name = "btnSelectDirectory";
            this.btnSelectDirectory.Size = new System.Drawing.Size(112, 24);
            this.btnSelectDirectory.TabIndex = 1;
            this.btnSelectDirectory.Text = "Select Scan Location";
            this.btnSelectDirectory.UseVisualStyleBackColor = true;
            this.btnSelectDirectory.Click += new System.EventHandler(this.SelectDefaultScanLocation);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(5, 69);
            this.btnReplace.Margin = new System.Windows.Forms.Padding(2);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(112, 24);
            this.btnReplace.TabIndex = 2;
            this.btnReplace.Text = "Replace DLLs";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // txtDllPath
            // 
            this.txtDllPath.Location = new System.Drawing.Point(131, 11);
            this.txtDllPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtDllPath.Name = "txtDllPath";
            this.txtDllPath.ReadOnly = true;
            this.txtDllPath.Size = new System.Drawing.Size(301, 20);
            this.txtDllPath.TabIndex = 3;
            // 
            // txtDirectoryPath
            // 
            this.txtDirectoryPath.Location = new System.Drawing.Point(131, 40);
            this.txtDirectoryPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtDirectoryPath.Name = "txtDirectoryPath";
            this.txtDirectoryPath.ReadOnly = true;
            this.txtDirectoryPath.Size = new System.Drawing.Size(301, 20);
            this.txtDirectoryPath.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(509, 179);
            this.tabControl1.TabIndex = 6;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.btnSelectDll);
            this.tabPage1.Controls.Add(this.btnSelectDirectory);
            this.tabPage1.Controls.Add(this.txtDirectoryPath);
            this.tabPage1.Controls.Add(this.btnReplace);
            this.tabPage1.Controls.Add(this.txtDllPath);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(501, 153);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DLL Replacer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(382, 124);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 24);
            this.button1.TabIndex = 5;
            this.button1.Text = "Clear Log";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnRegister);
            this.tabPage2.Controls.Add(this.btnBrowse);
            this.tabPage2.Controls.Add(this.txtFolderPath);
            this.tabPage2.Controls.Add(this.lblFolder);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(501, 153);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Package Installer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(6, 47);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(100, 23);
            this.btnRegister.TabIndex = 8;
            this.btnRegister.Text = "Register Package";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(420, 7);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(114, 9);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(300, 20);
            this.txtFolderPath.TabIndex = 6;
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(3, 12);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(112, 13);
            this.lblFolder.TabIndex = 5;
            this.lblFolder.Text = "Appx Package Folder:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.btnSaveSettings);
            this.tabPage3.Controls.Add(this.btnSelectBaseDirectory);
            this.tabPage3.Controls.Add(this.btnSelectGitHubPath);
            this.tabPage3.Controls.Add(this.txtGitHubPath);
            this.tabPage3.Controls.Add(this.txtBaseDirectory);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(501, 153);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(383, 123);
            this.btnSaveSettings.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(112, 24);
            this.btnSaveSettings.TabIndex = 11;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.ssb_Click);
            // 
            // btnSelectBaseDirectory
            // 
            this.btnSelectBaseDirectory.Location = new System.Drawing.Point(6, 6);
            this.btnSelectBaseDirectory.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectBaseDirectory.Name = "btnSelectBaseDirectory";
            this.btnSelectBaseDirectory.Size = new System.Drawing.Size(112, 24);
            this.btnSelectBaseDirectory.TabIndex = 6;
            this.btnSelectBaseDirectory.Text = "Select Base Dir";
            this.btnSelectBaseDirectory.UseVisualStyleBackColor = true;
            this.btnSelectBaseDirectory.Click += new System.EventHandler(this.btnSelectBaseDirectory_Click);
            // 
            // btnSelectGitHubPath
            // 
            this.btnSelectGitHubPath.Location = new System.Drawing.Point(6, 35);
            this.btnSelectGitHubPath.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectGitHubPath.Name = "btnSelectGitHubPath";
            this.btnSelectGitHubPath.Size = new System.Drawing.Size(112, 24);
            this.btnSelectGitHubPath.TabIndex = 7;
            this.btnSelectGitHubPath.Text = "Select GitHub Path";
            this.btnSelectGitHubPath.UseVisualStyleBackColor = true;
            this.btnSelectGitHubPath.Click += new System.EventHandler(this.btnSetGitHubPath_Click);
            // 
            // txtGitHubPath
            // 
            this.txtGitHubPath.Location = new System.Drawing.Point(132, 39);
            this.txtGitHubPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtGitHubPath.Name = "txtGitHubPath";
            this.txtGitHubPath.ReadOnly = true;
            this.txtGitHubPath.Size = new System.Drawing.Size(301, 20);
            this.txtGitHubPath.TabIndex = 10;
            // 
            // txtBaseDirectory
            // 
            this.txtBaseDirectory.Location = new System.Drawing.Point(132, 10);
            this.txtBaseDirectory.Margin = new System.Windows.Forms.Padding(2);
            this.txtBaseDirectory.Name = "txtBaseDirectory";
            this.txtBaseDirectory.ReadOnly = true;
            this.txtBaseDirectory.Size = new System.Drawing.Size(301, 20);
            this.txtBaseDirectory.TabIndex = 9;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.Location = new System.Drawing.Point(0, 179);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(509, 121);
            this.txtLog.TabIndex = 7;
            this.txtLog.Text = "";
            this.txtLog.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtLog_MouseDoubleClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(267, 123);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 24);
            this.button2.TabIndex = 12;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(266, 124);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 24);
            this.button3.TabIndex = 6;
            this.button3.Text = "Open Log File";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 300);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "WinDTools";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnSelectDll;
        private Button btnSelectDirectory;
        private Button btnReplace;
        private TextBox txtDllPath;
        private TextBox txtDirectoryPath;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button btnRegister;
        private Button btnBrowse;
        private TextBox txtFolderPath;
        private Label lblFolder;
        private RichTextBox txtLog;
        private TabPage tabPage3;
        private Button button1;
        private Button btnSaveSettings;
        private Button btnSelectBaseDirectory;
        private Button btnSelectGitHubPath;
        private TextBox txtGitHubPath;
        private TextBox txtBaseDirectory;
        private Button button2;
        private Button button3;
    }
}