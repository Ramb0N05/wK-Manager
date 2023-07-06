using SharpRambo.ExtensionsLib;
using System.Security.AccessControl;
using wK_Manager.Base;

namespace wK_Manager.MenuControls
{
    public partial class SettingsControl : WKMenuControl
    {
        private FolderBrowserDialog fbd = new();
        private OpenFileDialog ofd = new();

        public override IWKMenuControlConfig Config { get => ConfigProvider.Global; set => ConfigProvider.Global.SetData(value); }

        public SettingsControl(object sender) : base(sender)
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

            settingsTableLayoutPanel.Visible = false;
        }

        public override Task<bool> LoadConfig()
        {
            ConfigToControls(ConfigProvider.Global);
            return new Task<bool>(() => true);
        }

        public override void ConfigToControls(IWKMenuControlConfig config)
        {
            if (config is MainConfig conf)
            {
                userConfigPathTextBox.Text = conf.UserConfigDirectory ?? string.Empty;
                sevenZipPathTextBox.Text = conf.SevenZipPath ?? string.Empty;

                if (conf.StartupWindowName != null && conf.StartupWindowName.Trim() != string.Empty)
                    startWindowComboBox.SelectedItem = MainForm.MenuItems.First((i) => i.Key == conf.StartupWindowName).Value;
                else
                    startWindowComboBox.SelectedIndex = 0;
            }
        }

        public override MainConfig ConfigFromControls()
        {

            ConfigProvider.Global.UserConfigDirectory = userConfigPathTextBox.Text;
            ConfigProvider.Global.SevenZipPath = sevenZipPathTextBox.Text;
            ConfigProvider.Global.StartupWindowName = MainForm.MenuItems.FirstOrDefault((i) => i.Value == startWindowComboBox.SelectedItem.ToString()).Key;
            
            return ConfigProvider.Global;
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
                sevenZipPathTextBox.Text = ofd.FileName;
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

        private async void SettingsControl_Load(object sender, EventArgs e)
        {
            DirectoryInfo confDir = new FileInfo(Config.ConfigFilePath).Directory ?? new DirectoryInfo(Application.StartupPath);
            bool confDirIsWritable = await confDir.CheckFileSystemRight(FileSystemRights.WriteData);

            settingsTableLayoutPanel.Enabled = confDirIsWritable;
            checkPermissionPictureBox.Visible = !confDirIsWritable;
            checkPermissionLabel.Visible = !confDirIsWritable;

            await MainForm.MenuItems.ForEachAsync( async (item) => {
                startWindowComboBox.Items.Add(item.Value);
                await Task.CompletedTask;
            });

            settingsTableLayoutPanel.Visible = true;
            await LoadConfig();
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            await SaveConfig();
        }

        private async void defaultsButton_Click(object sender, EventArgs e)
        {
            await LoadConfig();
        }
    }
}

