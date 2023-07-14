using SharpRambo.ExtensionsLib;
using wK_Manager.Base;

namespace wK_Manager.MenuControls {

    public partial class PlugInsControl : WKMenuControl {
        public override IWKMenuControlConfig Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private readonly MainForm? main;

        #region Constructor

        public PlugInsControl(object sender) : base(sender) {
            InitializeComponent();

            if (sender is MainForm main) {
                this.main = main;
                pluginsListView.SmallImageList = main.menuImageList;
            }
        }

        #endregion Constructor

        #region EventHandlers

        private void openPlugInDirButton_Click(object sender, EventArgs e) {
        }

        private async void plugInsControl_Load(object sender, EventArgs e) {
            if (main != null) {
                await main.PM.PlugIns.ForEachAsync(async (plugin) => {
                    ListViewItem pluginItem = new(plugin.Name) {
                        Name = plugin.Identifier,
                        ImageKey = plugin.ImageKey
                    };

                    pluginsListView.Items.Add(pluginItem);
                    await Task.CompletedTask;
                });
            }
        }

        private void plugInsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            if (main != null) {
                if (sender is ListView plv && e.Item != null) {
                    IWKPlugIn? plugin = main.PM.PlugIns.FirstOrDefault((p) => p.Identifier == e.Item.Name);

                    if (plugin is not null and WKPlugIn p) {
                        string description = p.Description + Environment.NewLine + Environment.NewLine + "--" + Environment.NewLine
#if DEBUG
                                            + "(" + p.Identifier + "; "
                                            + (main.PM.PlugInMenuControls.TryGetValue(p.Identifier, out IEnumerable<WKMenuControl>? pVal)
                                                ? pVal.Count().ToString() + " GUI Controls"
                                                : string.Empty)
                                            + ")"
#endif
                        ;

                        pluginDescriptionTextBox.Text = description;
                        pluginLogoPictureBox.Image = main.menuImageList_large.Images.ContainsKey(p.ImageKey) ? main.menuImageList_large.Images[p.ImageKey] : null;
                        pluginNameLabel.Text = p.Name;
                        pluginVersionLabel.Text = p.Version;
                    }
                }
            }
        }

        #endregion EventHandlers
    }
}
