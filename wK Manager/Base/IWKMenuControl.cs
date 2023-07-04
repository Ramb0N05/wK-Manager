using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wK_Manager.Base
{
    public interface IWKMenuControl : IContainerControl
    {
        public abstract string MenuImageKey { get; set; }
        public abstract string MenuItemName { get; set; }
        public abstract int MenuItemOrder { get; set; }

        public Task<bool> LoadConfig(string configFilePath);
        public Task<bool> SaveConfig(string configFilePath);

        public void ConfigToControls(IWKMenuControlConfig config);
        public IWKMenuControlConfig? ConfigFromControls();
    }
}
