using SharpRambo.ExtensionsLib;
using wK_Manager.Base;
using wK_Manager.Base.Extensions;
using wK_Manager.Base.NativeCode;

namespace wK_Manager.MenuControls {

    public partial class PlugInsControl : WKMenuControl {
        public override IWKConfig Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private IWKPlugIn? lastSelectedPlugIn;

        #region Constructor

        public PlugInsControl(WKManagerBase @base) : base(@base) {
            InitializeComponent();
            pluginsListView.SmallImageList = Base.SmallImageList;
        }

        #endregion Constructor

        #region EventHandlers

        private void openPlugInDirButton_Click(object sender, EventArgs e) {
            if (lastSelectedPlugIn != null) {
                Shell32.OpenFolder(lastSelectedPlugIn.DirectoryPath);
            }
        }

        private async void plugInsControl_Load(object sender, EventArgs e)
            => await Base.PM.PlugIns.ForEachAsync(async (plugin) => {
                ListViewItem pluginItem = new(plugin.Name) {
                    Name = plugin.Identifier,
                    ImageKey = plugin.ImageKey
                };

                pluginsListView.Items.Add(pluginItem);
                await Task.CompletedTask;
            });

        private void plugInsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            if (sender is ListView plv && e.Item != null) {
                plugInInfoTableLayoutPanel.Visible = true;
                lastSelectedPlugIn = Base.PM.PlugIns.FirstOrDefault((p) => p.Identifier == e.Item.Name);

                if (lastSelectedPlugIn is WKPlugIn p) {
                    string description = p.Description + Environment.NewLine + Environment.NewLine + "--" + Environment.NewLine
#if DEBUG
                                        + "(" + p.Identifier + "; "
                                        + (Base.PM.PlugInMenuControls.TryGetValue(p.Identifier, out IEnumerable<WKMenuControl>? pVal)
                                            ? pVal.Count().ToString() + " GUI Controls"
                                            : string.Empty)
                                        + ")" + Environment.NewLine + p.DirectoryPath
#endif
                    ;

                    pluginDescriptionTextBox.Text = description;
                    pluginLogoPictureBox.Image = Base.GetImage(p.ImageKey, pluginLogoPictureBox.Size.GetWithAspectRatio());
                    pluginNameLabel.Text = p.Name;
                    pluginVersionLabel.Text = p.Version;
                }
            } else
                plugInInfoTableLayoutPanel.Visible = true;
        }

        #endregion EventHandlers
    }
}
