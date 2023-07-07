namespace wK_Manager.Base {
    public interface IWKPlugin {
        public const string PluginListNamePrefix = "plugin_";
        public const string UndefinedImageKey = "<i:undefined>";
        public const string UndefinedVersion = "<v:undefined>";
        public const string UnknownVersion = "<v:unknown>";

        public abstract string Identifier { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual string ImageKey => UndefinedImageKey;

        public abstract Task Initialize();
        public abstract void Dispose();
    }
}
