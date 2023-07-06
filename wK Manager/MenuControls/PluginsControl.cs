using SharpRambo.ExtensionsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wK_Manager.Base;

namespace wK_Manager.MenuControls
{
    public partial class PluginsControl : WKMenuControl
    {
        public override IWKMenuControlConfig Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private MainForm? Main { get; set; } = null;

        public PluginsControl(object sender) : base(sender)
        {
            InitializeComponent();

            if (sender is MainForm main)
            {
                Main = main;
                pluginsListView.SmallImageList = main.menuImageList;
            }
        }

        private async void PluginsControl_Load(object sender, EventArgs e)
        {
            if (Main != null)
            {
                await Main.PM.Plugins.ForEachAsync(async (plugin) =>
                {
                    ListViewItem pluginItem = new(plugin.Name)
                    {
                        Name = plugin.Identifier,
                        ImageKey = plugin.ImageKey
                    };

                    pluginsListView.Items.Add(pluginItem);
                    await Task.CompletedTask;
                });
            }
        }

        private void pluginsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (Main != null)
            {
                if (sender is ListView plv && e.Item != null)
                {
                    IWKPlugin? plugin = Main.PM.Plugins.FirstOrDefault((p) => p.Identifier == e.Item.Name);

                    if (plugin != null && plugin is WKPlugin p)
                    {
                        string description = p.Description + Environment.NewLine + Environment.NewLine + "--" + Environment.NewLine
#if DEBUG
                                            + "(" + p.Identifier + "; "
                                            + (Main.PM.PluginMenuControls.ContainsKey(p.Identifier)
                                                ? Main.PM.PluginMenuControls[p.Identifier].Count().ToString() + " GUI Controls"
                                                : string.Empty)
                                            + ")"
#endif
                        ;

                        pluginDescriptionTextBox.Text = description;
                        pluginLogoPictureBox.Image = Main.menuImageList.Images.ContainsKey(p.ImageKey) ? Main.menuImageList.Images[p.ImageKey] : null;
                        pluginNameLabel.Text = p.Name;
                        pluginVersionLabel.Text = p.Version;
                    }
                }
            }
        }
    }
}
