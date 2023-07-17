namespace wK_Manager.Base {

    public interface IWKMenuControl : IDisposable {
        public abstract string MenuImageKey { get; set; }
        public abstract string MenuItemName { get; set; }
        public abstract int MenuItemOrder { get; set; }

        public abstract void ConfigToControls(IWKConfig config);

        public abstract IWKConfig? ConfigFromControls();

        public abstract Task<bool> LoadConfig();

        public abstract Task<bool> SaveConfig();
    }
}
