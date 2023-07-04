using SharpRambo.ExtensionsLib;
using System.Windows.Forms;
using wK_Manager.Base;

namespace wK_Manager
{
    public partial class MainForm : Form
    {
        public static MainConfig Config { get; set; } = new();
        public static IEnumerable<KeyValuePair<string, string>> MenuItems { get; private set; } = Array.Empty<KeyValuePair<string, string>>();

        private const string menuItemTabPostfix = "_tab";
        private const string menuControlsNamespace = $"{nameof(wK_Manager)}.{nameof(MenuControls)}";

        public MainForm()
        {
            InitializeComponent();
            menuListView.Visible = false;
            menuTabControl.Visible = false;

            
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await Config.Load(MainConfig.ConfigFilePath);

            Type controlType = typeof(IWKMenuControl);
            IEnumerable<Type> controls = controlType.Assembly.GetTypes().Where(t => t.ImplementsOrDerives(controlType) &&
                                                                                    t.Namespace == menuControlsNamespace);

            List<ListViewItem> rawItems = new();
            foreach (Type c in controls)
            {
                WKMenuControl cInstance = (WKMenuControl?)Activator.CreateInstance(c) ?? throw new NullReferenceException();

                string menuItemName = cInstance.MenuItemName ?? cInstance.Name;
                string tabName = cInstance.Name + menuItemTabPostfix;
                ListViewItem item = new(menuItemName)
                {
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
            }

            foreach (ListViewItem item in rawItems.OrderBy((i) => i.Name))
            {
                menuListView.Items.Add(item);
                MenuItems = MenuItems.Append(new KeyValuePair<string, string>(item.Tag.ToString() ?? string.Empty, item.Text));
            }

            if (!Config.StartupWindowName.IsNull())
                menuTabControl.SelectTab(Config.StartupWindowName);
            else
                menuTabControl.SelectedIndex = 0;

            if (!Config.UserConfigDirectory.IsNull() && !Path.Exists(Config.UserConfigDirectory))
            {
                DirectoryInfo configDir = new(Config.UserConfigDirectory);
                configDir.CreateAnyway();
            }

            menuListView.Visible = true;
            menuTabControl.Visible = true;
            menuListView.Select();
        }

        private void menuListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected && e.Item != null)
                menuTabControl.SelectTab(e.Item.Tag.ToString());
        }
    }
}