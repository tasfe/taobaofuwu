using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RCSoft.Core.Infrastructure;
using RCSoft.Core;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace RCSoft.Web.Infrastructure.Installation
{
    public partial class InstallationLocalizationService:IInstallationLocalizationService
    {
        private IList<InstallationResource> _installationResource;
        public string GetResource(string resourceName)
        {
            var resource = GetInstallResource();
            if (resource == null)
                return resourceName;
            var resourceValue = resource
                .Where(r => r.Name.Equals(resourceName, StringComparison.InvariantCultureIgnoreCase))
                .Select(r => r.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(resourceValue))
                return resourceName;
            return resourceValue;
        }

        public virtual IList<InstallationResource> GetInstallResource()
        {
            if (_installationResource == null)
            {
                _installationResource = new List<InstallationResource>();
                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                foreach (var filePath in Directory.EnumerateFiles(webHelper.MapPath("~/App_Data/Localization"), "Install.rcsoftres.xml"))
                {
                    var xmlDocument = new XmlDocument();
                    xmlDocument.Load(filePath);

                    foreach (XmlNode resNode in xmlDocument.SelectNodes(@"//Install/LocalResource"))
                    {
                        var resNameAttribute = resNode.Attributes["Name"];
                        var resValueNode = resNode.SelectSingleNode("Value");
                        if (resNameAttribute == null)
                            throw new RCSoftException("安装资源文件中没有name=\"Value\"的值.");
                        var resourceName = resNameAttribute.Value.Trim();
                        if (string.IsNullOrEmpty(resourceName))
                            throw new RCSoftException("安装资源文件中属性为 'Name' 没有值.'");

                        if (resValueNode == null)
                            throw new RCSoftException("安装资源文件中没有\"Value\"元素节点.");
                        var resourceValue = resValueNode.InnerText.Trim();

                        _installationResource.Add(new InstallationResource()
                        {
                            Name = resourceName,
                            Value = resourceValue
                        });
                    }
                }
                
            }
            return _installationResource;
        }
    }
    public partial class InstallationResource
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}