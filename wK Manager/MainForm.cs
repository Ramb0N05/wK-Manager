using wK_Manager.Base;
using wK_Manager.MenuControls;

namespace wK_Manager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            string menuControlsNamespace = $"{nameof(wK_Manager)}.{nameof(wK_Manager.MenuControls)}";
            Type controlType = typeof(WKMenuControl);
            IEnumerable<Type> controls = controlType.Assembly.GetTypes().Where(t => controlType.IsAssignableFrom(t) &&
                                                                                    t.Namespace == menuControlsNamespace);

            InitializeComponent();

            foreach (Type c in controls)
            {
                object cType = Activator.CreateInstance(c) ?? throw new NullReferenceException();
                WKMenuControl cInstance = (WKMenuControl)cType;

                string menuItemName = cInstance.MenuItemName ?? cInstance.Name;
                string tabName = menuItemName.Trim().ToLower() + "_tab";
                ListViewItem item = new(menuItemName) { Tag = tabName, ImageKey = cInstance.MenuImageKey };
                menuListView.Items.Add(item);
                
                TabPage tab = new(menuItemName) { Name = tabName };
                tab.Controls.Add(cInstance);
                menuTabControl.TabPages.Add(tab);
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