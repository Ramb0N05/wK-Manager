using wK_Manager.Base;

namespace wK_Manager.MenuControls {

    public partial class InfoControl : WKMenuControl {
        public override IWKMenuControlConfig Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public InfoControl(object sender) : base(sender) {
            InitializeComponent();

            productInfoLabel.Text = productInfoLabel.Text
                                    .Replace("%ProductName%", Application.ProductName)
                                    .Replace("%ProductVersion%", Application.ProductVersion)
                                    .Replace("%Author%", Application.CompanyName);
        }
    }
}
