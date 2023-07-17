using SharpRambo.ExtensionsLib;
using System.Security.AccessControl;
using wK_Manager.Base;
using wK_Manager.Base.Extensions;

namespace wK_Manager.MenuControls {

    public partial class SettingsControl : WKMenuControl {
        private readonly FolderBrowserDialog fbd = new();
        private readonly OpenFileDialog ofd = new();

        public override IWKConfig Config { get => Base.GlobalConfig; set => Base.GlobalConfig.SetData(value); }

        #region Constructor

        public SettingsControl(WKManagerBase @base) : base(@base) {
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

        #endregion Constructor

        #region Methods

        public override MainConfig ConfigFromControls() {
            Base.GlobalConfig.UserConfigDirectory = userConfigPathTextBox.Text;
            Base.GlobalConfig.StartupWindowName = Base.MenuItems.FirstOrDefault((i) => i.DisplayName == startWindowComboBox.SelectedItem.ToString()).Name;

            return Base.GlobalConfig;
        }

        public override void ConfigToControls(IWKConfig config) {
            if (config is MainConfig conf) {
                userConfigPathTextBox.Text = conf.UserConfigDirectory ?? string.Empty;

                if (conf.StartupWindowName != null && conf.StartupWindowName.Trim() != string.Empty)
                    startWindowComboBox.SelectedItem = Base.MenuItems.First((i) => i.Name == conf.StartupWindowName).DisplayName;
                else
                    startWindowComboBox.SelectedIndex = 0;
            }
        }

        public override Task<bool> LoadConfig() {
            ConfigToControls(Base.GlobalConfig);
            return new Task<bool>(() => true);
        }

        #endregion Methods

        #region EventHandlers

        private async void defaultsButton_Click(object sender, EventArgs e)
            => await LoadConfig();

        private async void saveButton_Click(object sender, EventArgs e)
            => await SaveConfig();

        private async void settingsControl_Load(object sender, EventArgs e) {
            DirectoryInfo confDir = new FileInfo(Config.ConfigFilePath).Directory ?? new DirectoryInfo(Application.StartupPath);
            bool confDirIsWritable = await confDir.CheckFileSystemRight(FileSystemRights.WriteData);

            settingsTableLayoutPanel.Enabled = confDirIsWritable;
            checkPermissionPictureBox.Visible = !confDirIsWritable;
            checkPermissionLabel.Visible = !confDirIsWritable;

            await Base.MenuItems.ForEachAsync(async (item) => {
                startWindowComboBox.Items.Add(item.DisplayName);
                await Task.CompletedTask;
            });

            settingsTableLayoutPanel.Visible = true;
            await LoadConfig();
        }

        private void userConfigPathButton_Click(object sender, EventArgs e) {
            fbd.Reset();
            fbd.ShowNewFolderButton = true;
            fbd.InitialDirectory = userConfigPathTextBox.Text != null && userConfigPathTextBox.Text.Trim() != string.Empty
                ? userConfigPathTextBox.Text
                : Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (fbd.ShowDialog() == DialogResult.OK && Directory.Exists(fbd.SelectedPath))
                userConfigPathTextBox.Text = fbd.SelectedPath;
        }

        #endregion EventHandlers
    }
}
