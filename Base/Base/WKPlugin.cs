namespace wK_Manager.Base {

    public abstract class WKPlugIn : IWKPlugIn {
        public abstract string Description { get; }
        public virtual string DirectoryPath { get; }
        public virtual string Identifier => IWKPlugIn.PlugInListNamePrefix + Name.GetHashCode().ToString();
        public virtual string ImageKey => IWKPlugIn.UndefinedImageKey;
        public abstract string Name { get; }
        public virtual object Sender { get; }
        public virtual string Version { get; internal set; } = IWKPlugIn.UndefinedVersion;

        #region Constructor

        protected WKPlugIn(string directoryPath, object sender) {
            DirectoryPath = directoryPath;
            Sender = sender;
        }

        #endregion Constructor

        #region Methods

        public abstract void Dispose();

        public abstract Task Initialize();

        #endregion Methods
    }
}
