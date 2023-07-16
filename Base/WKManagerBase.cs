using SharpRambo.ExtensionsLib;
using Stimulsoft.Svg;
using System.Collections;
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

        public ResourceManager GlobalResourceManager { get; }
        public ImageList LargeImageList { get; }
        public IEnumerable<(string Name, string DisplayName)> MenuItems { get; private set; }
        public WKManagerBaseOptionsReadOnly Options { get; }
        public PlugInManager PM { get; }
        public ImageList SmallImageList { get; }

        #region Constructor

        private WKManagerBase(WKManagerBaseOptionsReadOnly optionsRO, PlugInManager pm) {
            GlobalResourceManager = DefaultResourceManager;
            LargeImageList = new() { ColorDepth = optionsRO.DefaultImageListColorDepth, ImageSize = optionsRO.LargeImageListSize };
            MenuItems = Array.Empty<(string Name, string DisplayName)>();
            Options = optionsRO;
            PM = pm;
            SmallImageList = new() { ColorDepth = optionsRO.DefaultImageListColorDepth, ImageSize = optionsRO.SmallImageListSize };
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

            ConfigProvider.Global.ConfigFilePath = File.Exists(optionsRO.MainConfigFilePath)
                ? optionsRO.MainConfigFilePath
                : throw new FileNotFoundException("Configuration file not found!", optionsRO.MainConfigFilePath);
            bool configLoaded = await ConfigProvider.Global.Load();

            WKManagerBase @base = new(optionsRO, new());
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
                applyGlobalConfig(optionsRO.MasterTabControl);

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

        private static void applyGlobalConfig(TabControl masterTabControl) {
            if (!ConfigProvider.Global.StartupWindowName.IsNull())
                masterTabControl.SelectTab(ConfigProvider.Global.StartupWindowName);
            else
                masterTabControl.SelectedIndex = 0;

            if (!ConfigProvider.Global.UserConfigDirectory.IsNull() && !Path.Exists(ConfigProvider.Global.UserConfigDirectory)) {
                DirectoryInfo configDir = new(ConfigProvider.Global.UserConfigDirectory);
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
                NetworkingProvider.HttpClient.Dispose();
                Images.ResourceManager.ReleaseAllResources();
            }
        }

        #endregion Methods
    }

    #region WKManagerBaseStartupOptions

    public class WKManagerBaseOptions {
        public ColorDepth DefaultImageListColorDepth { get; set; }
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

        public new string MainConfigFilePath { get; }
        public new Assembly MasterAssembly { get; }
        public new Form MasterForm { get; }
        public new ListView MasterListView { get; }
        public new TabControl MasterTabControl { get; }

        internal WKManagerBaseOptionsReadOnly(WKManagerBaseOptions options) {
            DefaultImageListColorDepth = options.DefaultImageListColorDepth;
            ImageListsToFill = options.ImageListsToFill;
            LargeImageListSize = options.LargeImageListSize;
            MainConfigFilePath = options.MainConfigFilePath ?? throw new ArgumentException(nameof(options.MainConfigFilePath) + DefaultNullExceptionMessage, nameof(options));
            MasterAssembly = options.MasterAssembly ?? throw new ArgumentException(nameof(options.MasterAssembly) + DefaultNullExceptionMessage, nameof(options));
            MasterForm = options.MasterForm ?? throw new ArgumentException(nameof(options.MasterForm) + DefaultNullExceptionMessage, nameof(options));
            MasterListView = options.MasterListView ?? throw new ArgumentException(nameof(options.MasterListView) + DefaultNullExceptionMessage, nameof(options));
            MasterTabControl = options.MasterTabControl ?? throw new ArgumentException(nameof(options.MasterTabControl) + DefaultNullExceptionMessage, nameof(options));
            NonPlugInsListViewGroup = options.NonPlugInsListViewGroup;
            NonPlugInsMenuControlsNamespace = options.NonPlugInsMenuControlsNamespace;
            PlugInsListViewGroup = options.PlugInsListViewGroup;
            SmallImageListSize = options.SmallImageListSize;
        }
    }

    #endregion WKManagerBaseStartupOptions
}
