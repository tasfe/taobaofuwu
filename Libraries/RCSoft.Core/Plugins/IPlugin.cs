﻿
namespace RCSoft.Core.Plugins
{
    public interface IPlugin
    {
        PluginDescriptor PluginDescriptor { get; set; }

        void Install();

        void Uninstall();
    }
}
