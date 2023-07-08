namespace wK_Manager.Base {
    public abstract class WKPlugin : IWKPlugin {
        public abstract string Description { get; }
        public virtual string DirectoryPath { get; }
        public virtual string Identifier => IWKPlugin.PluginListNamePrefix + Name.GetHashCode().ToString();
        public virtual string ImageKey => IWKPlugin.UndefinedImageKey;
        public abstract string Name { get; }
        public virtual object Sender { get; }
        public virtual string Version { get; internal set; } = IWKPlugin.UndefinedVersion;
        
        public WKPlugin(string directoryPath, object sender) {
            DirectoryPath = directoryPath;
            Sender = sender;
        }

        public abstract Task Initialize();
        public abstract void Dispose();
    }
}
