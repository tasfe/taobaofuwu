using System;
using System.Configuration;
using System.Xml;

namespace RCSoft.Core.Configuration
{
    /// <summary>
    /// 重写一个RCSoftConfig
    /// </summary>
    public partial class RCSoftConfig : IConfigurationSectionHandler
    {

        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new RCSoftConfig();
            var dynamicDiscoveryNode = section.SelectSingleNode("DynamicDiscovery");
            if (dynamicDiscoveryNode != null && dynamicDiscoveryNode.Attributes != null)
            {
                var attribute = dynamicDiscoveryNode.Attributes["Enabled"];
                if (attribute != null)
                    config.DynamicDiscovery = Convert.ToBoolean(attribute.Value);
            }

            var engineNode = section.SelectSingleNode("Engine");
            if (engineNode != null && engineNode.Attributes != null)
            {
                var attribute = engineNode.Attributes["Type"];
                if (attribute != null)
                    config.EngineType = attribute.Value;
            }
            var themeNode = section.SelectSingleNode("Themes");
            if (themeNode != null && themeNode.Attributes != null)
            {
                var attribute = themeNode.Attributes["basePath"];
                if (attribute != null)
                    config.ThemeBasePath = attribute.Value;
            }
            return config;
        }

        public bool DynamicDiscovery { get; set; }
        /// <summary>
        /// 客户自定义的 <see cref="IEngine"/> 来代替默认设置管理应用.
        /// </summary>
        public string EngineType { get; set; }
        /// <summary>
        /// 指定皮肤存储位置 (~/Themes/)
        /// </summary>
        public string ThemeBasePath { get; set; }
    }
}
