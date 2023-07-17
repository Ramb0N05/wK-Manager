using SharpRambo.ExtensionsLib;
using Stimulsoft.Svg;
using System.Collections;
using System.Net;
using System.Reflection;
using System.Resources;
using wK_Manager.Base;
using wK_Manager.Base.Extensions;
using wK_Manager.Base.Providers;
using wK_Manager.Base.Resources;

namespace wK_Manager {

    public sealed class WKManagerBase : IDisposable {
        public static readonly ResourceManager DefaultResourceManager = Images.ResourceManager;
        private const string MenuItemTabPostfix = "_tab";

        public MainConfig GlobalConfig { get; private set; }
        public ResourceManager GlobalResourceManager { get; }
        public HttpClient HttpClient { get; }
        public ImageList LargeImageList { get; }
        public IEnumerable<(string Name, string DisplayName)> MenuItems { get; private set; }
        public WKManagerBaseOptionsReadOnly Options { get; }
        public PlugInManager PM { get; }
        public IDictionary<string, IWKConfig> RegisteredUserConfigs { get; }
        public ImageList SmallImageList { get; }

        #region Constructor

        private WKManagerBase(WKManagerBaseOptionsReadOnly optionsRO) {
            GlobalConfig = new(optionsRO.MainConfigFilePath);
            GlobalResourceManager = DefaultResourceManager;
            LargeImageList = new() { ColorDepth = optionsRO.DefaultImageListColorDepth, ImageSize = optionsRO.LargeImageListSize };
            MenuItems = Array.Empty<(string Name, string DisplayName)>();
            Options = optionsRO;
            PM = new(optionsRO.PlugInsMenuControlType, optionsRO.PlugInsSearchPaths);
            SmallImageList = new() { ColorDepth = optionsRO.DefaultImageListColorDepth, ImageSize = optionsRO.SmallImageListSize };
            RegisteredUserConfigs = new Dictionary<string, IWKConfig>();

            HttpClient = optionsRO.HttpClientProxy != null
                ? new(new HttpClientHandler() { UseProxy = true, Proxy = optionsRO.HttpClientProxy }, true)
                : new(new HttpClientHandler() { UseProxy = false }, true);
        }

        #endregion Constructor

        #region Methods

        public static async Task<ResourceManager?> FillImageLists(IEnumerable<ImageList> lists, ResourceManager rm) {
            if (!lists.Any())
                return null;

            ResourceSet? set = rm.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true);

            if (set is null)
                return null;

            await lists.ForEachAsync(async (il) => {
                await set.Cast<DictionaryEntry>()
                    .Where((r) => r.Value is byte[])
                    .Select((r) => new { Name = r.Key.ToString(), Image = r.Value as byte[] })
                    .ForEachAsync(async (r) => {
                        if (r.Name is not null && r.Image is not null && !il.Images.ContainsKey(r.Name))
                            il.Images.Add(r.Name, getImage(r.Image, il.ImageSize));

                        await Task.CompletedTask;
                    });

                await Task.CompletedTask;
            });

            return rm;
        }

        public static async Task<WKManagerBase> Initialize(WKManagerBaseOptions options) {
            WKManagerBaseOptionsReadOnly optionsRO = new(options);

            WKManagerBase @base = new(optionsRO);
            bool configLoaded = await @base.GlobalConfig.Load();
            await @base.PM.Initialize(ref @base);

            IEnumerable<Type> nonPlugInControls = !string.IsNullOrWhiteSpace(optionsRO.NonPlugInsMenuControlsNamespace)
                ? getTypesFromNamespace(typeof(IWKMenuControl), optionsRO.NonPlugInsMenuControlsNamespace, optionsRO.MasterAssembly)
                : Array.Empty<Type>();

            (IEnumerable<ListViewItem> listViewItems, IEnumerable<TabPage> tabPages) nonPlugInItems = await initializeNonPlugInMenuControls(nonPlugInControls, @base, optionsRO.NonPlugInsListViewGroup);
            (IEnumerable<ListViewItem> listViewItems, IEnumerable<TabPage> tabPages) plugInItems = await initializePlugInMenuControls(@base.PM.PlugInMenuControls, optionsRO.PlugInsListViewGroup);

            IEnumerable<ListViewItem> listViewItems = nonPlugInItems.listViewItems.Concat(plugInItems.listViewItems);
            IEnumerable<TabPage> tabPages = nonPlugInItems.tabPages.Concat(plugInItems.tabPages);

            optionsRO.MasterListView.Groups.AddRange(new[] { optionsRO.PlugInsListViewGroup, optionsRO.NonPlugInsListViewGroup });

            IEnumerable<(string Name, string DisplayName)> menuItems = Array.Empty<(string Name, string DisplayName)>();
            await listViewItems.OrderBy((i) => i.Name).ForEachAsync(async (item) => {
                if (item.Tag is string itemTabName) {
                    optionsRO.MasterListView.Items.Add(item);

                    if (menuItems.Any((i) => i.Name == itemTabName))
                        throw new InvalidOperationException("The Name \"" + item.Tag.ToString() + "\" already exists in the collection!");

                    menuItems = menuItems.Append(new() { Name = itemTabName, DisplayName = item.Text });
                } else if (item.Tag is not null)
                    throw new InvalidOperationException("Invalid type of ListViewItem.Tag! (" + item.Tag.GetType().FullName + ")");
                else
                    throw new InvalidOperationException("ListViewItem.Tag is empty!");

                await Task.CompletedTask;
            });

            @base.MenuItems = menuItems;

            await tabPages.ForEachAsync(async (p) => {
                optionsRO.MasterTabControl.TabPages.Add(p);
                await Task.CompletedTask;
            });

            if (configLoaded)
                applyGlobalConfig(optionsRO.MasterTabControl, @base.GlobalConfig);

            IEnumerable<ImageList> imageLists = optionsRO.ImageListsToFill.Concat(new[] { @base.LargeImageList, @base.SmallImageList });
            await FillImageLists(imageLists, @base.GlobalResourceManager);

            return @base;
        }

        public void Dispose() {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        public Bitmap? GetImage(string name) => GetImage(name, null);

        public Bitmap? GetImage(string name, Size? rasterSize) {
            if (name.IsNull())
                return null;

            object? svgArr = GlobalResourceManager.GetObject(name);

            return svgArr is byte[] buffer ? getImage(buffer, rasterSize) : null;
        }

        public T? GetUserConfig<T>(string identifier) where T : IWKConfig
            => (T?)GetUserConfig(identifier);

        public IWKConfig? GetUserConfig(string identifier)
            => RegisteredUserConfigs.ContainsKey(identifier) ? RegisteredUserConfigs[identifier] : null;

        public string? RegisterUserConfig(IWKConfig configInstance) {
            Type baseType = configInstance.GetType();
            FieldInfo? cfnField = baseType.GetField(nameof(IWKConfig.ConfigFileName));
            PropertyInfo? cfpProperty = baseType.GetProperty(nameof(IWKConfig.ConfigFilePath));

            if (cfnField != null && cfpProperty != null) {
                string cfnValue = (string?)cfnField.GetValue(null) ?? string.Empty;
                string cfpValue = (string?)cfpProperty.GetValue(configInstance) ?? string.Empty;

                if (!cfnValue.IsNull() && cfnValue != IWKConfig.ConfigFileName && cfpValue == IWKConfig.AutoDetect_ConfigFilePath) {
                    cfpProperty.SetValue(configInstance, GlobalConfig.GetUserConfigFilePath(cfnValue));
                    string configIdentifier = HashingProvider.SHA256_Simple(baseType.FullName + ";" + cfnValue);

                    if (RegisteredUserConfigs.TryAdd(configIdentifier, configInstance))
                        return configIdentifier;
                }
            }

            return null;
        }

        public void SetUserConfig(string identifier, IWKConfig data) {
            if (!RegisteredUserConfigs.ContainsKey(identifier))
                return;

            RegisteredUserConfigs[identifier] = data;
        }

        private static void applyGlobalConfig(TabControl masterTabControl, MainConfig config) {
            if (!config.StartupWindowName.IsNull())
                masterTabControl.SelectTab(config.StartupWindowName);
            else
                masterTabControl.SelectedIndex = 0;

            if (!config.UserConfigDirectory.IsNull() && !Path.Exists(config.UserConfigDirectory)) {
                DirectoryInfo configDir = new(config.UserConfigDirectory);
                configDir.CreateAnyway();
            }
        }

        private static Bitmap getImage(byte[] imageData, Size? rasterSize) {
            using MemoryStream ms = new(imageData);
            SvgDocument svgDoc = SvgDocument.Open<SvgDocument>(ms);

            return rasterSize is not null
                ? svgDoc.Draw(rasterSize.Value.Width, rasterSize.Value.Height)
                : svgDoc.Draw();
        }

        private static IEnumerable<Type> getTypesFromNamespace(Type type, string @namespace, Assembly assembly)
            => @namespace.IsNull()
                ? (IEnumerable<Type>)Array.Empty<Type>()
                : assembly.GetTypes().Where(t => t.ImplementsOrDerives(type) && t.Namespace == @namespace);

        private static async Task<(IEnumerable<ListViewItem> listViewItems, IEnumerable<TabPage> tabPages)> initializeNonPlugInMenuControls(IEnumerable<Type> controls, WKManagerBase @base, ListViewGroup lvGroup) {
            (IEnumerable<ListViewItem> listViewItems, IEnumerable<TabPage> tabPages) result = new() {
                listViewItems = Array.Empty<ListViewItem>(),
                tabPages = Array.Empty<TabPage>()
            };

            await controls.ForEachAsync(async (c) => {
                WKMenuControl cInstance = (WKMenuControl?)Activator.CreateInstance(c, @base) ?? throw new NullReferenceException();

                string menuItemName = cInstance.MenuItemName ?? cInstance.Name;
                string tabName = cInstance.Name + MenuItemTabPostfix;
                ListViewItem item = new(menuItemName) {
                    Group = lvGroup,
                    ImageKey = cInstance.MenuImageKey,
                    Name = cInstance.MenuItemOrder.ToString() + "_" + menuItemName.Trim(),
                    Tag = tabName
                };

                result.listViewItems = result.listViewItems.Append(item);

                TabPage tab = new(menuItemName) { AutoScroll = true, Name = tabName };
                cInstance.Dock = DockStyle.Fill;
                cInstance.Padding = new(2, 5, 5, 5);
                tab.Controls.Add(cInstance);
                result.tabPages = result.tabPages.Append(tab);

                await Task.CompletedTask;
            });

            return result;
        }

        private static async Task<(IEnumerable<ListViewItem> listViewItems, IEnumerable<TabPage> tabPages)> initializePlugInMenuControls(Dictionary<string, IEnumerable<WKMenuControl>> controls, ListViewGroup lvGroup) {
            (IEnumerable<ListViewItem> listViewItems, IEnumerable<TabPage> tabPages) result = new() {
                listViewItems = Array.Empty<ListViewItem>(),
                tabPages = Array.Empty<TabPage>()
            };

            await controls.ForEachAsync(async (plugin) => await plugin.Value.ForEachAsync(async (c) => {
                string menuItemName = c.MenuItemName ?? c.Name;
                string tabName = c.Name + MenuItemTabPostfix;
                ListViewItem item = new(menuItemName) {
                    Group = lvGroup,
                    ImageKey = c.MenuImageKey,
                    Name = c.MenuItemOrder.ToString() + "_" + menuItemName.Trim(),
                    Tag = tabName
                };

                result.listViewItems = result.listViewItems.Append(item);

                TabPage tab = new(menuItemName) { AutoScroll = true, Name = tabName };
                c.Dock = DockStyle.Fill;
                c.Padding = new(2, 5, 5, 5);
                tab.Controls.Add(c);
                result.tabPages = result.tabPages.Append(tab);

                await Task.CompletedTask;
            }));

            return result;
        }

        private void dispose(bool disposing) {
            if (disposing) {
                PM.Dispose();
                HttpClient.Dispose();
                Images.ResourceManager.ReleaseAllResources();
            }
        }

        #endregion Methods
    }

    #region WKManagerBaseOptions

    public class WKManagerBaseOptions {
        public ColorDepth DefaultImageListColorDepth { get; set; }
        public IWebProxy? HttpClientProxy { get; set; }
        public IEnumerable<ImageList> ImageListsToFill { get; set; }
        public Size LargeImageListSize { get; set; }
        public string? MainConfigFilePath { get; set; }
        public Assembly? MasterAssembly { get; set; }
        public Form? MasterForm { get; set; }
        public ListView? MasterListView { get; set; }
        public TabControl? MasterTabControl { get; set; }
        public ListViewGroup NonPlugInsListViewGroup { get; set; }
        public string? NonPlugInsMenuControlsNamespace { get; set; }
        public ListViewGroup PlugInsListViewGroup { get; set; }
        public Type? PlugInsMenuControlType { get; set; }
        public IEnumerable<string>? PlugInsSearchPaths { get; set; }
        public Size SmallImageListSize { get; set; }

        public WKManagerBaseOptions() {
            DefaultImageListColorDepth = ColorDepth.Depth32Bit;
            ImageListsToFill = Array.Empty<ImageList>();
            LargeImageListSize = new Size(128, 128);
            NonPlugInsListViewGroup = new("General", HorizontalAlignment.Center);
            PlugInsListViewGroup = new("PlugIns", HorizontalAlignment.Center);
            SmallImageListSize = new Size(24, 24);
        }
    }

    public sealed class WKManagerBaseOptionsReadOnly : WKManagerBaseOptions {
        private const string DefaultNullExceptionMessage = " is not set!";

        public new ColorDepth DefaultImageListColorDepth { get; }
        public new IWebProxy? HttpClientProxy { get; }
        public new IEnumerable<ImageList> ImageListsToFill { get; }
        public new Size LargeImageListSize { get; }
        public new string MainConfigFilePath { get; }
        public new Assembly MasterAssembly { get; }
        public new Form MasterForm { get; }
        public new ListView MasterListView { get; }
        public new TabControl MasterTabControl { get; }
        public new ListViewGroup NonPlugInsListViewGroup { get; }
        public new string? NonPlugInsMenuControlsNamespace { get; }
        public new ListViewGroup PlugInsListViewGroup { get; }
        public new Type? PlugInsMenuControlType { get; }
        public new IEnumerable<string>? PlugInsSearchPaths { get; }
        public new Size SmallImageListSize { get; }

        internal WKManagerBaseOptionsReadOnly(WKManagerBaseOptions options) {
            DefaultImageListColorDepth = options.DefaultImageListColorDepth;
            ImageListsToFill = options.ImageListsToFill;
            HttpClientProxy = options.HttpClientProxy;
            LargeImageListSize = options.LargeImageListSize;
            MainConfigFilePath = options.MainConfigFilePath ?? throw new ArgumentException(nameof(options.MainConfigFilePath) + DefaultNullExceptionMessage, nameof(options));
            MasterAssembly = options.MasterAssembly ?? throw new ArgumentException(nameof(options.MasterAssembly) + DefaultNullExceptionMessage, nameof(options));
            MasterForm = options.MasterForm ?? throw new ArgumentException(nameof(options.MasterForm) + DefaultNullExceptionMessage, nameof(options));
            MasterListView = options.MasterListView ?? throw new ArgumentException(nameof(options.MasterListView) + DefaultNullExceptionMessage, nameof(options));
            MasterTabControl = options.MasterTabControl ?? throw new ArgumentException(nameof(options.MasterTabControl) + DefaultNullExceptionMessage, nameof(options));
            NonPlugInsListViewGroup = options.NonPlugInsListViewGroup;
            NonPlugInsMenuControlsNamespace = options.NonPlugInsMenuControlsNamespace;
            PlugInsListViewGroup = options.PlugInsListViewGroup;
            PlugInsMenuControlType = options.PlugInsMenuControlType;
            PlugInsSearchPaths = options.PlugInsSearchPaths;
            SmallImageListSize = options.SmallImageListSize;
        }
    }

    #endregion WKManagerBaseOptions
}
