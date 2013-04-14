using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RCSoft.Web.Infrastructure.Installation
{
    public partial interface IInstallationLocalizationService
    {
        string GetResource(string resourceName);
        
    }
}