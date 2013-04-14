using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RCSoft.Web.Infrastructure.Installation
{
    public partial class InstallationLocalizationService:IInstallationLocalizationService
    {
        private IList<I
        public string GetResource(string resourceName)
        {
            var resource = GetInstallResource();
        }

        private object GetInstallResource()
        {
            throw new NotImplementedException();
        }
    }
}