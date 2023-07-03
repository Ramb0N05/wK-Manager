using ScreenInformation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using wK_Manager.Base;

namespace wK_Manager.MenuControls
{
    public partial class SettingsControl : WKMenuControl<MainConfig>
    {
        private FolderBrowserDialog fbd = new();
        private OpenFileDialog ofd = new();

        public string ConfigFilePath = MainConfig.ConfigFilePath;

        public SettingsControl()
        {
            InitializeComponent();

            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DereferenceLinks = true;
            ofd.Multiselect = false;
            ofd.OkRequiresInteraction = true;
            ofd.RestoreDirectory = false;
            ofd.ValidateNames = true;

            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
            WindowsPrincipal currentPrincipal = new(currentIdentity);
            string currentSid = currentIdentity.User?.Value ?? throw new NullReferenceException(nameof(currentSid));

            if (currentPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
                currentSid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null).Value;

            string confDirName = Path.GetDirectoryName(ConfigFilePath) ?? Application.StartupPath;
            DirectoryInfo confDir = new(confDirName);
            DirectorySecurity confDirAcl = confDir.GetAccessControl(AccessControlSections.Access);
            AuthorizationRuleCollection confDirRules = confDirAcl.GetAccessRules(true, true, typeof(SecurityIdentifier));
            bool confDirIsWritable = false;

            foreach (AuthorizationRule rule in confDirRules)
            {
                if (rule.IdentityReference.Value.Equals(currentSid, StringComparison.CurrentCultureIgnoreCase))
                {
                    FileSystemAccessRule filesystemAccessRule = (FileSystemAccessRule)rule;

                    if ((filesystemAccessRule.FileSystemRights & FileSystemRights.WriteData) > 0 && filesystemAccessRule.AccessControlType != AccessControlType.Deny)
                        confDirIsWritable = true;
                }
            }

            settingsTableLayoutPanel.Enabled = confDirIsWritable;
            checkPermissionPictureBox.Visible = !confDirIsWritable;
            checkPermissionLabel.Visible = !confDirIsWritable;
        }

        public override Task<MainConfig> LoadConfig(string configFilePath)
        {
            ConfigToControls(MainForm.Config);
            return new Task<MainConfig>(() => MainForm.Config);
        }

        /*public override async Task<MainConfig> SaveConfig(string configFilePath)
        {
            MainConfig config = ConfigFromControls() ?? throw new NullReferenceException(nameof(ConfigFromControls));

            FileStream jsonFile = File.OpenWrite(configFilePath);
            await JsonSerializer.SerializeAsync(jsonFile, config, new JsonSerializerOptions() { WriteIndented = true });
            jsonFile.Close();

            return config;
        }*/

        public override void ConfigToControls(MainConfig config)
        {
            userConfigPathTextBox.Text = config.UserConfigDirectory ?? string.Empty;
            sevenZipPathTextBox.Text = config.SevenZipPath ?? string.Empty;

            if (config.StartupWindowName != null && config.StartupWindowName.Trim() != string.Empty)
                startWindowComboBox.SelectedItem = MainForm.MenuItems.First((i) => i.Key == config.StartupWindowName).Value;
            else
                startWindowComboBox.SelectedIndex = 0;
        }

        public override MainConfig? ConfigFromControls()
        {
            return new()
            {
                UserConfigDirectory = userConfigPathTextBox.Text,
                SevenZipPath = sevenZipPathTextBox.Text,
                StartupWindowName = MainForm.MenuItems.FirstOrDefault((i) => i.Value == startWindowComboBox.SelectedItem.ToString()).Key
            };
        }

        private void sevenZipPathButton_Click(object sender, EventArgs e)
        {
            ofd.DefaultExt = "exe";
            ofd.Filter = "7-Zip CLI|7z.exe;7za.exe;7zG.exe";
            ofd.Title = "7-Zip Pfad auswählen ...";

            ofd.InitialDirectory = sevenZipPathTextBox.Text != null && sevenZipPathTextBox.Text.Trim() != string.Empty
                ? sevenZipPathTextBox.Text
                : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            if (ofd.ShowDialog() == DialogResult.OK && File.Exists(ofd.FileName))
            {
                sevenZipPathTextBox.Text = ofd.FileName;
            }
        }

        private void userConfigPathButton_Click(object sender, EventArgs e)
        {
            fbd.Reset();
            fbd.ShowNewFolderButton = true;
            fbd.InitialDirectory = userConfigPathTextBox.Text != null && userConfigPathTextBox.Text.Trim() != string.Empty
                ? userConfigPathTextBox.Text
                : Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (fbd.ShowDialog() == DialogResult.OK && Directory.Exists(fbd.SelectedPath))
                userConfigPathTextBox.Text = fbd.SelectedPath;
        }

        private void SettingsControl_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, string> item in MainForm.MenuItems)
                startWindowComboBox.Items.Add(item.Value);

            LoadConfig(ConfigFilePath);
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            MainForm.Config = await SaveConfig(ConfigFilePath);
        }

        private void defaultsButton_Click(object sender, EventArgs e)
        {
            LoadConfig(ConfigFilePath);
        }
    }
}

