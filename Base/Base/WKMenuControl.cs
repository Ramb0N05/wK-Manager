using System.ComponentModel;

namespace wK_Manager.Base {

    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<WKMenuControl, WKMenuControlDesign>))]
    public abstract class WKMenuControl : UserControl, IWKMenuControl {
        public abstract IWKConfig Config { get; set; }
        public virtual string MenuImageKey { get; set; } = string.Empty;
        public virtual string MenuItemName { get; set; } = string.Empty;
        public virtual int MenuItemOrder { get; set; }

        protected virtual WKManagerBase Base { get; set; }

        #region Constructor

        protected WKMenuControl(WKManagerBase @base) {
            Base = @base;
        }

        #endregion Constructor

        #region Methods

        public virtual IWKConfig? ConfigFromControls()
            => throw new NotImplementedException();

        public virtual void ConfigToControls(IWKConfig config)
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

        #endregion Methods
    }

    #region DesignerClass

    public class WKMenuControlDesign : UserControl, IWKMenuControl {

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

        public virtual IWKConfig? ConfigFromControls()
            => throw new NotImplementedException();

        public virtual void ConfigToControls(IWKConfig config)
            => throw new NotImplementedException();

        public virtual Task<bool> LoadConfig()
                            => throw new NotImplementedException();

        public virtual Task<bool> SaveConfig()
            => throw new NotImplementedException();
    }

    #endregion DesignerClass
}
