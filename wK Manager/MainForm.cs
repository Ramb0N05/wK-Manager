using System.Reflection;
using wK_Manager.Base;
using wK_Manager.Base.Extensions;

namespace wK_Manager {

    public partial class MainForm : Form {
        private const string MenuControlsNamespace = $"{nameof(wK_Manager)}.{nameof(MenuControls)}";

        public WKManagerBase? Base { get; private set; }
        private WKManagerBaseOptions startupOptions { get; set; }

        #region Constructor

        public MainForm() {
            InitializeComponent();

            startupOptions = new WKManagerBaseOptions() {
                MainConfigFilePath = Path.Combine(Application.StartupPath, Properties.Settings.Default.mainConfigName),
                MasterAssembly = GetType().Assembly,
                MasterForm = this,
                MasterListView = menuListView,
                MasterTabControl = menuTabControl,
                NonPlugInsListViewGroup = new("Allgemein", HorizontalAlignment.Center),
                NonPlugInsMenuControlsNamespace = MenuControlsNamespace,
                PlugInsListViewGroup = new("Erweiterungen", HorizontalAlignment.Center)
            };

            menuListView.Visible = false;
            menuTabControl.Visible = false;
        }

        #endregion Constructor

        #region EventHandlers

        private async void mainForm_Load(object sender, EventArgs e) {
            Base = await WKManagerBase.Initialize(startupOptions);

            menuListView.SmallImageList = Base.SmallImageList;
            menuListView.Width--;
            menuListView.Visible = true;
            menuTabControl.Visible = true;
            menuListView.Select();
        }

        private void menuListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            if (e.IsSelected && e.Item?.Tag is string tabName)
                menuTabControl.SelectTab(tabName);
        }

        private void menuListView_Resize(object sender, EventArgs e) {
            if (sender is ListView lv && lv.Columns.Count > 0)
                lv.Columns[0].Width = lv.Width - 5;
        }

        #endregion EventHandlers
    }
}
