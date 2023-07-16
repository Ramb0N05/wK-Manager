using SharpRambo.ExtensionsLib;
using System.Reflection;
using wK_Manager.Base.Extensions;

namespace wK_Manager.Base {

    public class PlugInManager : IDisposable {
        public const string MenuControlsNamespace = $"{nameof(wK_Manager)}.{nameof(PlugIns)}.MenuControls";

        public static Type ControlType { get; private set; } = typeof(IWKMenuControl);
        public static IEnumerable<string>? PlugInSearchDirectories { get; private set; } = new[] { "plugins" };

        public Dictionary<string, IEnumerable<WKMenuControl>> PlugInMenuControls { get; } = new();
        public IEnumerable<IWKPlugIn> PlugIns { get; private set; } = Enumerable.Empty<IWKPlugIn>();
        public WKManagerBase? Base { get; private set; }

        #region Constructor

        public PlugInManager() : this(null, null) {
        }

        public PlugInManager(Type? controlType) : this(controlType, null) {
        }

        public PlugInManager(Type? controlType, IEnumerable<string>? plugInSearchPaths) {
            if (controlType != null)
                ControlType = controlType;

            if (!plugInSearchPaths.IsNull())
                PlugInSearchDirectories = plugInSearchPaths;
        }

        #endregion Constructor

        #region Methods

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                foreach (KeyValuePair<string, IEnumerable<WKMenuControl>> pluginControls in PlugInMenuControls) {
                    foreach (WKMenuControl control in pluginControls.Value)
                        control.Dispose();
                }

                foreach (IWKPlugIn plugin in PlugIns)
                    plugin.Dispose();
            }
        }

        public Task Initialize(ref WKManagerBase @base) {
            Base = @base;
            return initializeImpl();
        }

        private async Task initializeImpl() {
            if (Base is null)
                throw new InvalidOperationException(nameof(Base) + " is empty!");

            await PlugInSearchDirectories.ForEachAsync(async (path) => {
                if (!path.IsNull() && Directory.Exists(path)) {
                    string fullPath = Path.GetFullPath(path);
                    IEnumerable<string> plugins = Directory.GetDirectories(fullPath);

                    await plugins.ForEachAsync(async (pluginPath) => {
                        string pluginName = new DirectoryInfo(pluginPath).Name;
                        FileInfo pluginFile = new(Path.Combine(pluginPath, pluginName + ".dll"));

                        if (!pluginName.IsNull() && pluginFile?.Exists == true) {
                            Assembly pluginAssembly = loadPlugIn(pluginFile.FullName);
                            PlugIns = PlugIns.Concat(await createPlugIns(pluginAssembly, Base));
                        }

                        await Task.CompletedTask;
                    });
                }

                await Task.CompletedTask;
            });

            await PlugIns.ForEachAsync(async (plugin) => {
                if (plugin is WKPlugIn p) {
                    IEnumerable<Type> controlTypes = plugin.GetType().Assembly.GetTypes().Where(
                        (type) => type.ImplementsOrDerives(ControlType) && type.Namespace == MenuControlsNamespace
                    );

                    PlugInMenuControls.Add(p.Identifier, controlTypes.SelectMany((cType) => {
                        WKMenuControl? cInstance = (WKMenuControl?)Activator.CreateInstance(cType, p);
                        return cInstance != null ? (new[] { cInstance }) : Enumerable.Empty<WKMenuControl>();
                    }));

                    p.Version = p.GetType().Assembly.GetName().Version?.ToString() ?? IWKPlugIn.UnknownVersion;
                    await p.Initialize();
                }
            });
        }

        private static async Task<IEnumerable<IWKPlugIn>> createPlugIns(Assembly assembly, WKManagerBase @base) {
            IEnumerable<IWKPlugIn> plugins = Enumerable.Empty<IWKPlugIn>();

            await assembly.GetTypes().ForEachAsync(async (type) => {
                if (typeof(IWKPlugIn).IsAssignableFrom(type) && Activator.CreateInstance(type, assembly.Location, @base) is IWKPlugIn result)
                    plugins = plugins.Append(result);

                await Task.CompletedTask;
            });

            return plugins;
        }

        private Assembly loadPlugIn(string relativePath) {
            string root = Path.GetFullPath(GetType().Assembly.Location);
            string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));

            PlugInLoadContext loadContext = new(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        #endregion Methods
    }
}
