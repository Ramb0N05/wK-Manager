using wK_Manager.Base;
using wK_Manager.Base.Extensions;

namespace wK_Manager.MenuControls {

    public partial class InfoControl : WKMenuControl {
        public override IWKMenuControlConfig Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public InfoControl(WKManagerBase @base) : base(@base) {
            InitializeComponent();

            productInfoLabel.Text = productInfoLabel.Text
                                    .Replace("%ProductName%", Application.ProductName)
                                    .Replace("%ProductVersion%", Application.ProductVersion)
                                    .Replace("%Author%", Application.CompanyName);

            logoPictureBox.Image = Base.GetImage("beer", logoPictureBox.Size.GetWithAspectRatio());
        }
    }
}
