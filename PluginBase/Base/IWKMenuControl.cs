namespace wK_Manager.Base
{
    public interface IWKMenuControl
    {
        public abstract string MenuImageKey { get; set; }
        public abstract string MenuItemName { get; set; }
        public abstract int MenuItemOrder { get; set; }

        public abstract void ConfigToControls(IWKMenuControlConfig config);
        public abstract IWKMenuControlConfig? ConfigFromControls();

        public abstract void Dispose();

        public abstract Task<bool> LoadConfig();
        public abstract Task<bool> SaveConfig();
    }
}
