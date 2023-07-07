using System.ComponentModel;
using System.Windows.Forms;

namespace wK_Manager.Base {
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

        public virtual Task<bool> LoadConfig()
            => throw new NotImplementedException();

        public virtual Task<bool> SaveConfig()
            => throw new NotImplementedException();

        public virtual void ConfigToControls(IWKMenuControlConfig _)
            => throw new NotImplementedException();

        public virtual IWKMenuControlConfig? ConfigFromControls()
            => throw new NotImplementedException();
    }

    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<WKMenuControl, WKMenuControl_Design>))]
    public abstract class WKMenuControl : UserControl, IWKMenuControl {
        public virtual string MenuImageKey { get; set; } = string.Empty;
        public virtual string MenuItemName { get; set; } = string.Empty;
        public virtual int MenuItemOrder { get; set; } = 0;

        public abstract IWKMenuControlConfig Config { get; set; }

        private object sender { get; }

        public WKMenuControl(object sender) : base() {
            this.sender = sender;
        }

        public virtual async Task<bool> LoadConfig() {
            if (Config != null && await Config.Load()) {
                ConfigToControls(Config);
                return true;
            } else
                return false;
        }

        public virtual async Task<bool> SaveConfig() {
            Config = ConfigFromControls() ?? throw new NullReferenceException(nameof(ConfigFromControls));
            return await Config.Save();
        }

        public virtual void ConfigToControls(IWKMenuControlConfig config)
            => throw new NotImplementedException();

        public virtual IWKMenuControlConfig? ConfigFromControls()
            => throw new NotImplementedException();
    }
}
