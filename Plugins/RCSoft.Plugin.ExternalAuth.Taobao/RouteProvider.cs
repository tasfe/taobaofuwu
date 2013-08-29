using System.Web.Mvc;
using RCSoft.Web.Framework.Mvc.Routes;
using System.Web.Routing;

namespace RCSoft.Plugin.ExternalAuth.Taobao
{
    public partial class RouteProvider:IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Plugin.ExternalAuth.Taobao.PublicInfo",
                 "Plugins/ExternalAuthTaobao/PublicInfo",
                 new { controller = "ExternalAuthTaobao", action = "PublicInfo" },
                 new[] { "RCSoft.Plugin.ExternalAuth.Taobao.Controllers" }
            );

            routes.MapRoute("Plugin.ExternalAuth.Taobao.Login",
                 "Plugins/ExternalAuthTaobao/Login",
                 new { controller = "ExternalAuthTaobao", action = "Login" },
                 new[] { "RCSoft.Plugin.ExternalAuth.Taobao.Controllers" }
                 );
            routes.MapRoute("Plugin.ExternalAuth.Taobao.Auth",
                "Plugins/ExternalAuthTaobao/Auth",
                new { controller = "ExternalAuthTaobao", action = "Authorizer" },
                 new[] { "RCSoft.Plugin.ExternalAuth.Taobao.Controllers"}
                 );
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
