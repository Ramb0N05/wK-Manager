using SharpRambo.ExtensionsLib;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using wK_Manager.MenuControls;

namespace wK_Manager.Base
{
    public class WKMenuControl_Design : UserControl, IWKMenuControl {
        [Category("wK")]
        [Description("Set the displayed image in menu.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual string MenuImageKey { get; set; } = string.Empty;

        [Category("wK")]
        [Description("Set the displayed name in menu.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual string MenuItemName { get; set; } = string.Empty;

        [Category("wK")]
        [Description("Set the displayed order in menu.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual int MenuItemOrder { get; set; } = 0;

        public virtual Task<bool> LoadConfig(string _)
            => throw new NotImplementedException();

        public virtual Task<bool> SaveConfig(string _)
            => throw new NotImplementedException();

        public virtual void ConfigToControls(IWKMenuControlConfig _)
            => throw new NotImplementedException();

        public virtual IWKMenuControlConfig? ConfigFromControls()
            => throw new NotImplementedException();
    }

    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<WKMenuControl, WKMenuControl_Design>))]
    public abstract class WKMenuControl : UserControl, IWKMenuControl
    {
        public virtual string MenuImageKey { get; set; } = string.Empty;
        public virtual string MenuItemName { get; set; } = string.Empty;
        public virtual int MenuItemOrder { get; set; } = 0;

        public abstract IWKMenuControlConfig Config { get; set; }

        public WKMenuControl() : base()
        { }

        public virtual async Task<bool> LoadConfig(string configFilePath)
        {
            if (Config != null && await Config.Load(configFilePath))
            {
                ConfigToControls(Config);
                return true;
            }
            else
                return false;
        }

        public virtual async Task<bool> SaveConfig(string configFilePath)
        {
            string configFileDir = Path.GetDirectoryName(configFilePath) ?? throw new ArgumentException(nameof(configFilePath));

            if (!configFileDir.IsNull() && !Path.Exists(configFileDir))
                new DirectoryInfo(configFileDir).CreateAnyway();

            Config = ConfigFromControls() ?? throw new NullReferenceException(nameof(ConfigFromControls));
            return await Config.Save(configFilePath);
        }

        public virtual void ConfigToControls(IWKMenuControlConfig config)
            => throw new NotImplementedException();

        public virtual IWKMenuControlConfig? ConfigFromControls()
            => throw new NotImplementedException();
    }
}
