using System.ComponentModel;

namespace wK_Manager.Base
{
    public class WKMenuControl : UserControl, IWKMenuControl
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

        public WKMenuControl() : base()
        {
            Dock = DockStyle.Fill;
            Padding = new(2, 5, 5, 5);
        }
    }
}
