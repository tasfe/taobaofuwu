
namespace RCSoft.Core.Plugins
{
    public abstract class BasePlugin : IPlugin
    {
        protected BasePlugin()
        { }
        public virtual PluginDescriptor PluginDescriptor { get; set; }

        public virtual void Install()
        {
            PluginManager.MarkPluginAsInstalled(this.PluginDescriptor.SystemName);
        }

        public virtual void Uninstall()
        {
            PluginManager.MarkPluginAsUninstalled(this.PluginDescriptor.SystemName);
        }
    }
}
