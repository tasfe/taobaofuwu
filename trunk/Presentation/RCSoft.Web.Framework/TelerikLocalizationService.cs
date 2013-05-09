using System;
using System.Collections.Generic;
using System.Linq;

namespace RCSoft.Web.Framework
{
    public class TelerikLocalizationService:Telerik.Web.Mvc.Infrastructure.ILocalizationService
    {
        private string _resourceName;
        private RCSoft.Services.Localization.ILocalizationService _localizationService;

        public TelerikLocalizationService(string resourceName,Services.Localization.ILocalizationService localizationService)
        {
            _resourceName = resourceName;
            _localizationService = localizationService;
        }

        public IDictionary<string, string> All()
        {
            return ScopedResources();
        }

        public bool IsDefault
        {
            get { return true; }
        }

        public string One(string key)
        {
            var resourceName = "Telerik." + _resourceName + "." + key;
            return _localizationService.GetResource(resourceName, true, resourceName);
        }

        private IDictionary<string, string> ScopedResources()
        {
            var scope = "Telerik." + _resourceName;
            var result = _localizationService.GetAllResourceValues()
                .Where(x => x.Key.StartsWith(scope, StringComparison.InvariantCultureIgnoreCase))
                .ToDictionary(x => x.Key.Replace(scope, ""), x => x.Value.Value);
            return result;
        }
    }
}
