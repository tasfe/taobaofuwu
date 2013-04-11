using System.Web.Mvc;
using System.Web.Routing;
using RCSoft.Web.Framework.Localization;
using RCSoft.Web.Framework.Mvc.Routes;

namespace RCSoft.Web.Infrastructure
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //首页
            routes.MapLocalizedRoute("HomePage",
                "",
                new { controller = "Home", action = "Index" },
                new[] { "RCSoft.Web.Controllers" });
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}