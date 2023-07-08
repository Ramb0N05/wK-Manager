using SharpRambo.ExtensionsLib;
using System.Reflection;

namespace wK_Manager.Base {
    public class PluginManager {
        public const string MenuControlsNamespace = $"{nameof(wK_Manager)}.{nameof(Plugins)}.MenuControls";
        public static Type ControlType { get; private set; } = typeof(IWKMenuControl);
        public static IEnumerable<string> PluginSearchDirectories { get; private set; } = new[] { "plugins" };

        public IEnumerable<IWKPlugin> Plugins { get; private set; } = Enumerable.Empty<IWKPlugin>();

        public Dictionary<string, IEnumerable<WKMenuControl>> PluginMenuControls { get; private set; } = new();
        private Dictionary<string, IEnumerable<Type>> pluginControls { get; set; } = new Dictionary<string, IEnumerable<Type>>();

        public PluginManager() : this(null, null) { }
        public PluginManager(Type? controlType) : this(controlType, null) { }
        public PluginManager(Type? controlType, IEnumerable<string>? pluginSearchPaths) {
            if (controlType != null)
                ControlType = controlType;

            if (!pluginSearchPaths.IsNull())
#pragma warning disable CS8601 // Mögliche Nullverweiszuweisung.
                PluginSearchDirectories = pluginSearchPaths;
#pragma warning restore CS8601 // Mögliche Nullverweiszuweisung.
        }

        public async Task Initialize(object sender) {
            await PluginSearchDirectories.ForEachAsync(async (path) => {
                if (!path.IsNull() && Directory.Exists(path)) {
                    string fullPath = Path.GetFullPath(path);
                    IEnumerable<string> plugins = Directory.GetDirectories(fullPath);

                    await plugins.ForEachAsync(async (pluginPath) => {
                        string pluginName = new DirectoryInfo(pluginPath).Name;
                        FileInfo pluginFile = new(Path.Combine(pluginPath, pluginName + ".dll"));

                        if (pluginName != null && pluginFile != null && pluginFile.Exists) {
                            Assembly pluginAssembly = loadPlugin(pluginFile.FullName);
                            Plugins = Plugins.Concat(await createPlugins(pluginAssembly, sender ?? this));
                        }

                        await Task.CompletedTask;
                    });
                }

                await Task.CompletedTask;
            });

            await Plugins.ForEachAsync(async (plugin) => {
                if (plugin is WKPlugin p) {
                    IEnumerable<Type> controlTypes = plugin.GetType().Assembly.GetTypes().Where(
                        (type) => type.ImplementsOrDerives(ControlType) && type.Namespace == MenuControlsNamespace
                    );

                    PluginMenuControls.Add(p.Identifier, controlTypes.SelectMany((cType) => {
                        WKMenuControl? cInstance = (WKMenuControl?)Activator.CreateInstance(cType, p);
                        return cInstance != null ? (new[] { cInstance }) : Enumerable.Empty<WKMenuControl>();
                    }));

                    p.Version = p.GetType().Assembly.GetName().Version?.ToString() ?? IWKPlugin.UnknownVersion;
                    await p.Initialize();
                }
            });
        }

        public void Dispose() {
            foreach (KeyValuePair<string, IEnumerable<WKMenuControl>> pluginControls in PluginMenuControls)
                foreach (WKMenuControl control in pluginControls.Value)
                    control.Dispose();

            foreach (IWKPlugin plugin in Plugins)
                plugin.Dispose();
        }

        private static Assembly loadPlugin(string relativePath) {
            // Navigate up to the solution root
            string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(PluginManager).Assembly.Location))))) ?? string.Empty));

            string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            PluginLoadContext loadContext = new(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        private static async Task<IEnumerable<IWKPlugin>> createPlugins(Assembly assembly, object sender) {
            IEnumerable<IWKPlugin> plugins = Enumerable.Empty<IWKPlugin>();
            
            await assembly.GetTypes().ForEachAsync(async (type) => {
                if (typeof(IWKPlugin).IsAssignableFrom(type) && Activator.CreateInstance(type, "", sender) is IWKPlugin result)
                    plugins = plugins.Append(result);

                await Task.CompletedTask;
            });

            return plugins;
        }
    }
}
