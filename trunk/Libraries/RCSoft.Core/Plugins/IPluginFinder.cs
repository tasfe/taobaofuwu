using System.Collections.Generic;

namespace RCSoft.Core.Plugins
{
    public interface IPluginFinder
    {
        IEnumerable<T> GetPlugins<T>(bool installedOnly = true) where T : class,IPlugin;

        IEnumerable<PluginDescriptor> GetPluginDescriptors(bool installedOnly = true);

        IEnumerable<PluginDescriptor> GetPluginDescriptors<T>(bool installedOnly = true) where T : class, IPlugin;

        PluginDescriptor GetPluginDescriptorBySystemName(string systemName, bool installedOnly = true);

        PluginDescriptor GetPluginDescriptorBySystemName<T>(string systemName, bool installedOnly = true) where T : class, IPlugin;

        /// <summary>
        /// Reload plugins
        /// </summary>
        void ReloadPlugins();
    }
}
