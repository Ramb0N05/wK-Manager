using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wK_Manager.Base
{
    public abstract class WKPlugin : IWKPlugin
    {
        public virtual object Sender { get; set; }
        public virtual string Identifier { get => IWKPlugin.PluginListNamePrefix + Name.GetHashCode().ToString(); }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual string Version { get; internal set; } = IWKPlugin.UndefinedVersion;
        public virtual string ImageKey { get => IWKPlugin.UndefinedImageKey; }

        public WKPlugin(object sender)
        {
            Sender = sender;
        }

        public abstract Task Initialize();
        public abstract void Dispose();
    }
}
