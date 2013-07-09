using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using RCSoft.Core.Infrastructure;

namespace RCSoft.Core.Plugins
{
    public class PluginDescriptor : IComparable<PluginDescriptor>
    {
        public PluginDescriptor()
        {
            this.SupportedVersions = new List<string>();
        }

        public PluginDescriptor(Assembly referencedAssembly, FileInfo originalAssemblyFile, Type pluginType)
            : this()
        {
            this.ReferencedAssembly = referencedAssembly;
            this.OriginalAssemblyFile = originalAssemblyFile;
            this.PluginType = pluginType;
        }

        public virtual string PluginFileName { get; set; }

        public virtual Type PluginType { get; set; }

        public virtual Assembly ReferencedAssembly { get; internal set; }

        public virtual FileInfo OriginalAssemblyFile { get; internal set; }

        public virtual string Group { get; set; }
        /// <summary>
        /// Gets or sets the friendly name
        /// </summary>
        public virtual string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the system name
        /// </summary>
        public virtual string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the version
        /// </summary>
        public virtual string Version { get; set; }

        /// <summary>
        /// Gets or sets the supported versions of nopCommerce
        /// </summary>
        public virtual IList<string> SupportedVersions { get; set; }

        /// <summary>
        /// Gets or sets the author
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether plugin is installed
        /// </summary>
        public virtual bool Installed { get; set; }

        public virtual T Instance<T>() where T : class,IPlugin
        {
            object instance;

            if (!EngineContext.Current.ContainerManager.TryResolve(PluginType, out instance))
            {
                instance = EngineContext.Current.ContainerManager.ResolveUnregistered(PluginType);
            }
            var typedInstance = instance as T;
            if (typedInstance != null)
                typedInstance.PluginDescriptor = this;
            return typedInstance;
        }

        public IPlugin Instance()
        {
            return Instance<IPlugin>();
        }

        public int CompareTo(PluginDescriptor other)
        {
            if (DisplayOrder != other.DisplayOrder)
                return DisplayOrder.CompareTo(other.DisplayOrder);
            else
                return FriendlyName.CompareTo(other.FriendlyName);
        }

        public override string ToString()
        {
            return FriendlyName;
        }

        public override bool Equals(object obj)
        {
            var other = obj as PluginDescriptor;
            return other != null &&
                SystemName != null &&
                SystemName.Equals(other.SystemName);
        }

        public override int GetHashCode()
        {
            return SystemName.GetHashCode();
        }
    }
}
