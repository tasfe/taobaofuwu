using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCSoft.Core.Plugins;
using System.Web.Routing;

namespace RCSoft.Services.Authentication.External
{
    public partial interface IExternalAuthenticationMethod : IPlugin
    {
        void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues);

        void GetPublicInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues);
    }
}
