using SharpRambo.ExtensionsLib;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using wK_Manager.Base;

namespace wK_Manager
{
    public partial class MainForm : Form
    {
        public static MainConfig Config = new();
        public static IEnumerable<KeyValuePair<string, string>> MenuItems { get; private set; } = Array.Empty<KeyValuePair<string, string>>();

        private const string menuItemTabPostfix = "_tab";
        private const string menuControlsNamespace = $"{nameof(wK_Manager)}.{nameof(MenuControls)}";

        public MainForm()
        {
            if (File.Exists(MainConfig.ConfigFilePath))
            {
                string json = File.ReadAllText(MainConfig.ConfigFilePath);
                object? conf = JsonSerializer.Deserialize(json, typeof(MainConfig));
                Config = conf != null ? (MainConfig)conf : throw new NullReferenceException(nameof(conf));
            }
            else
            {
                string json = JsonSerializer.Serialize(Config, typeof(MainConfig), new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText(MainConfig.ConfigFilePath, json);
            }

            Type controlType = typeof(WKMenuControl<>);
            IEnumerable<Type> controls = controlType.Assembly.GetTypes().Where(t => t.ImplementsOrDerives(controlType) &&
                                                                                    t.Namespace == menuControlsNamespace);
            InitializeComponent();

            List<ListViewItem> rawItems = new();
            foreach (Type c in controls)
            {
                object cInstance = Activator.CreateInstance(c) ?? throw new NullReferenceException();

                string? cName = cInstance.GetPropValue<string>("Name");
                string? cMenuItemName = cInstance.GetPropValue<string>("MenuItemName");

                if (cName == null)
                    break;

                string cMenuImageKey = cInstance.GetPropValue<string>("MenuImageKey") ?? string.Empty;
                int cMenuItemOrder = cInstance.GetPropValue<int?>("MenuItemOrder") ?? 0;

                string menuItemName = cMenuItemName ?? cName ?? string.Empty;
                string tabName = cName + menuItemTabPostfix;
                ListViewItem item = new(menuItemName) {
                    ImageKey = cMenuImageKey,
                    Name = cMenuItemOrder.ToString() + "_" + menuItemName.Trim(),
                    Tag = tabName
                };

                rawItems.Add(item);

                TabPage tab = new(menuItemName) { AutoScroll = true, Name = tabName };
                tab.Controls.Add(cInstance as Control);
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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void menuListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected && e.Item != null)
                menuTabControl.SelectTab(e.Item.Tag.ToString());
        }
    }
}