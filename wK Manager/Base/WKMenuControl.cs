using SharpRambo.ExtensionsLib;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.Json;
using wK_Manager.MenuControls;

namespace wK_Manager.Base
{
    public class WKMenuControl<CType> : UserControl, IWKMenuControl<CType> where CType : new()
    {
        [Category("wK")]
        [Description("Set the displayed image in menu.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string MenuImageKey { get; set; } = string.Empty;

        [Category("wK")]
        [Description("Set the displayed name in menu.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string MenuItemName { get; set; } = string.Empty;

        [Category("wK")]
        [Description("Set the displayed order in menu.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MenuItemOrder { get; set; } = 0;

        public WKMenuControl() : base()
        {
            Dock = DockStyle.Fill;
            Padding = new(2, 5, 5, 5);
        }

        public virtual async Task<CType> LoadConfig(string configFilePath)
        {
            CType? config = new();

            if (File.Exists(configFilePath))
            {
                FileStream jsonFile = File.OpenRead(configFilePath);
                config = await JsonSerializer.DeserializeAsync<CType>(jsonFile);
                jsonFile.Close();
            }
            else
            {
                FileStream jsonFile = File.Open(configFilePath, FileMode.Create);
                await JsonSerializer.SerializeAsync(jsonFile, config, new JsonSerializerOptions() { WriteIndented = true });
                jsonFile.Close();
            }

            if (config == null)
                throw new NullReferenceException(nameof(config));

            ConfigToControls(config);
            return config;
        }

        public virtual async Task<CType> SaveConfig(string configFilePath)
        {
            string configFileDir = Path.GetDirectoryName(configFilePath) ?? throw new ArgumentException(nameof(configFilePath));

            if (!Path.Exists(configFileDir))
                new DirectoryInfo(configFileDir).CreateAnyway();

            CType config = ConfigFromControls() ?? throw new NullReferenceException(nameof(ConfigFromControls));

            FileStream jsonFile = File.Open(configFilePath, FileMode.Create);
            await JsonSerializer.SerializeAsync(jsonFile, config, new JsonSerializerOptions() { WriteIndented = true });
            jsonFile.Close();

            return config;
        }

        public virtual void ConfigToControls(CType config)
            => throw new NotImplementedException();

        public virtual CType? ConfigFromControls()
            => throw new NotImplementedException();
    }
}
