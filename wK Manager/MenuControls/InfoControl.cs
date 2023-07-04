using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wK_Manager.Base;

namespace wK_Manager.MenuControls
{
    public partial class InfoControl : WKMenuControl
    {
        public override IWKMenuControlConfig Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public InfoControl()
        {
            InitializeComponent();

            productInfoLabel.Text = productInfoLabel.Text
                                    .Replace("%ProductName%", Application.ProductName)
                                    .Replace("%ProductVersion%", Application.ProductVersion)
                                    .Replace("%Author%", Application.CompanyName);
        }
    }
}
