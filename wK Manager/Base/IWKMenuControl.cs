using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wK_Manager.Base
{
    public interface IWKMenuControl<CType> where CType : new()
    {
        public string MenuImageKey { get; set; }
        public string MenuItemName { get; set; }
        public int MenuItemOrder { get; set; }

        public Task<CType> LoadConfig(string configFilePath);
        public Task<CType> SaveConfig(string configFilePath);

        public void ConfigToControls(CType config);
        public CType? ConfigFromControls();
    }
}
