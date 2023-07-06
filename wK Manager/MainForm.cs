using SharpRambo.ExtensionsLib;
using System.Reflection;
using System.Windows.Forms;
using wK_Manager.Base;

namespace wK_Manager
{
    public partial class MainForm : Form
    {
        public static IEnumerable<KeyValuePair<string, string>> MenuItems { get; private set; } = Array.Empty<KeyValuePair<string, string>>();
        public PluginManager PM { get; private set; } = new();

        private const string menuItemTabPostfix = "_tab";
        private const string menuControlsNamespace = $"{nameof(wK_Manager)}.{nameof(MenuControls)}";
        private const string extensionMenuControlsNamespace = $"{nameof(wK_Manager)}.Plugins.{nameof(MenuControls)}";

        public MainForm()
        {
            ConfigProvider.Global.ConfigFilePath = Path.Combine(Application.StartupPath, Properties.Settings.Default.mainConfigName);

            InitializeComponent();
            menuListView.Visible = false;
            menuTabControl.Visible = false;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await ConfigProvider.Global.Load();
            await PM.Initialize(this);

            Type controlType = typeof(IWKMenuControl);
            IEnumerable<Type> generalControls = Assembly.GetExecutingAssembly().GetTypes().Where(
                t => t.ImplementsOrDerives(controlType) && t.Namespace == menuControlsNamespace
            );

            List<ListViewItem> rawItems = new();
            ListViewGroup generalListViewGroup = new("Allgemein", HorizontalAlignment.Center);
            ListViewGroup pluginsListViewGroup = new("Erweiterungen", HorizontalAlignment.Center);
            menuListView.Groups.AddRange(new[] { pluginsListViewGroup, generalListViewGroup });

            await generalControls.ForEachAsync(async (c) =>
            {
                WKMenuControl cInstance = (WKMenuControl?)Activator.CreateInstance(c, this) ?? throw new NullReferenceException();

                string menuItemName = cInstance.MenuItemName ?? cInstance.Name;
                string tabName = cInstance.Name + menuItemTabPostfix;
                ListViewItem item = new(menuItemName)
                {
                    Group = generalListViewGroup,
                    ImageKey = cInstance.MenuImageKey,
                    Name = cInstance.MenuItemOrder.ToString() + "_" + menuItemName.Trim(),
                    Tag = tabName
                };

                rawItems.Add(item);

                TabPage tab = new(menuItemName) { AutoScroll = true, Name = tabName };
                cInstance.Dock = DockStyle.Fill;
                cInstance.Padding = new(2, 5, 5, 5);
                tab.Controls.Add(cInstance);
                menuTabControl.TabPages.Add(tab);

                await Task.CompletedTask;
            });

            await PM.PluginMenuControls.ForEachAsync(async (plugin) => await plugin.Value.ForEachAsync(async (c) =>
            {
                string menuItemName = c.MenuItemName ?? c.Name;
                string tabName = c.Name + menuItemTabPostfix;
                ListViewItem item = new(menuItemName)
                {
                    Group = pluginsListViewGroup,
                    ImageKey = c.MenuImageKey,
                    Name = c.MenuItemOrder.ToString() + "_" + menuItemName.Trim(),
                    Tag = tabName
                };

                rawItems.Add(item);

                TabPage tab = new(menuItemName) { AutoScroll = true, Name = tabName };
                c.Dock = DockStyle.Fill;
                c.Padding = new(2, 5, 5, 5);
                tab.Controls.Add(c);
                menuTabControl.TabPages.Add(tab);

                await Task.CompletedTask;
            }));

            await rawItems.OrderBy((i) => i.Name).ForEachAsync(async (item) =>
            {
                menuListView.Items.Add(item);
                MenuItems = MenuItems.Append(new KeyValuePair<string, string>(item.Tag.ToString() ?? string.Empty, item.Text));

                await Task.CompletedTask;
            });

            if (!ConfigProvider.Global.StartupWindowName.IsNull())
                menuTabControl.SelectTab(ConfigProvider.Global.StartupWindowName);
            else
                menuTabControl.SelectedIndex = 0;

            if (!ConfigProvider.Global.UserConfigDirectory.IsNull() && !Path.Exists(ConfigProvider.Global.UserConfigDirectory))
            {
                DirectoryInfo configDir = new(ConfigProvider.Global.UserConfigDirectory);
                configDir.CreateAnyway();
            }

            menuListView.Width--;
            menuListView.Visible = true;
            menuTabControl.Visible = true;
            menuListView.Select();
        }

        private void menuListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected && e.Item != null)
                menuTabControl.SelectTab(e.Item.Tag.ToString());
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

        }

        private void menuListView_Resize(object sender, EventArgs e)
        {
            if (sender is ListView lv)
                if (lv.Columns.Count > 0)
                    lv.Columns[0].Width = lv.Width - 5;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PM.Dispose();
        }
    }
}