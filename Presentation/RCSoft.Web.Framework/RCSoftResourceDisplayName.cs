using System.ComponentModel;
using RCSoft.Web.Framework.Mvc;
using RCSoft.Core.Infrastructure;
using RCSoft.Core;
using RCSoft.Services.Localization;

namespace RCSoft.Web.Framework
{
    public class RCSoftResourceDisplayName:DisplayNameAttribute,IModelAttribute
    {

        private string _resourceValue = string.Empty;

        public RCSoftResourceDisplayName(string resourceKey)
            : base(resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public string ResourceKey { get; set; }

        public override string DisplayName
        {
            get
            {
                _resourceValue = EngineContext.Current.Resolve<ILocalizationService>().GetResource(ResourceKey, true, ResourceKey);
                return _resourceValue;
            }
        }
        public string Name
        {
            get { return "RCSoftResourceDisplayName"; }
        }
    }
}
