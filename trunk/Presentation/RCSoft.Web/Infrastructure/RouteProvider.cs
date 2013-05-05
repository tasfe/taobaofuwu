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
            #region 角色

            //角色列表
            routes.MapLocalizedRoute("CustomerRole",
                "Customer/Role",
                new { controller = "CustomerRole", action = "List" },
                new[] { "RCSoft.Web.Controllers" });
            #endregion

            //routes.m
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}