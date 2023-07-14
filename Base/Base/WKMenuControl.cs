using System.ComponentModel;

namespace wK_Manager.Base {

    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<WKMenuControl, WKMenuControl_Design>))]
    public abstract class WKMenuControl : UserControl, IWKMenuControl {
        private readonly object sender;

        public abstract IWKMenuControlConfig Config { get; set; }
        public virtual string MenuImageKey { get; set; } = string.Empty;
        public virtual string MenuItemName { get; set; } = string.Empty;
        public virtual int MenuItemOrder { get; set; }

        protected WKMenuControl(object sender) {
            this.sender = sender;
        }

        public virtual IWKMenuControlConfig? ConfigFromControls()
            => throw new NotImplementedException();

        public virtual void ConfigToControls(IWKMenuControlConfig config)
            => throw new NotImplementedException();

        public virtual async Task<bool> LoadConfig() {
            if (Config != null && await Config.Load()) {
                ConfigToControls(Config);
                return true;
            }

            return false;
        }

        public virtual async Task<bool> SaveConfig() {
            Config = ConfigFromControls() ?? throw new NullReferenceException(nameof(ConfigFromControls));
            return await Config.Save();
        }
    }

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
        public virtual int MenuItemOrder { get; set; }

        public virtual IWKMenuControlConfig? ConfigFromControls()
            => throw new NotImplementedException();

        public virtual void ConfigToControls(IWKMenuControlConfig config)
            => throw new NotImplementedException();

        public virtual Task<bool> LoadConfig()
                            => throw new NotImplementedException();

        public virtual Task<bool> SaveConfig()
            => throw new NotImplementedException();
    }
}
