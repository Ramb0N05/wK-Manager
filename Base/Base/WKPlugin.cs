using wK_Manager.Base.Providers;

namespace wK_Manager.Base {

    public abstract class WKPlugIn : IWKPlugIn {
        public abstract string ConfigIdentifier { get; }
        public abstract string Description { get; }
        public virtual string DirectoryPath { get; }
        public virtual string Identifier => IWKPlugIn.PlugInListNamePrefix + HashingProvider.SHA1_Simple(Name);
        public virtual string ImageKey => IWKPlugIn.UndefinedImageKey;
        public abstract string Name { get; }
        public virtual WKManagerBase Base { get; }
        public virtual string Version { get; internal set; } = IWKPlugIn.UndefinedVersion;

        #region Constructor

        protected WKPlugIn(string assemblyLocation, WKManagerBase @base) {
            DirectoryPath = Path.GetDirectoryName(assemblyLocation) ?? Path.GetDirectoryName(GetType().Assembly.Location) ?? string.Empty;
            Base = @base;
        }

        #endregion Constructor

        #region Methods

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract Task Initialize();

        protected abstract void Dispose(bool disposing);

        #endregion Methods

        #region Finalizer

        ~WKPlugIn() {
            Dispose(false);
        }

        #endregion Finalizer
    }
}
