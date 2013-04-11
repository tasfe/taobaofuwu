using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using RCSoft.Core.Infrastructure;
using RCSoft.Core.Data;
using RCSoft.Web.Framework.Mvc.Routes;
using RCSoft.Web.Framework.Mvc;
using RCSoft.Core;

namespace RCSoft.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        { 
            
        }

        public static void RegisterRouters(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var routePublisher = EngineContext.Current.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
                "Default",//路由名称
                "{controller}/{action}/{id}",//带参数的URL
                new { colltroller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "RCSoft.Web.Controllers" }
                );
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            //初始化引擎上下文
            EngineContext.Initialize(false);
            bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();

            var dependencyResolver = new RCSoftDependencyResolver();
            DependencyResolver.SetResolver(dependencyResolver);

            if (databaseInstalled)
            {
                ViewEngines.Engines.Clear();
                //ViewEngines.Engines.Add(new ThemeableRazorViewEngine());
            }

            RegisterRouters(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            EnsureDatabaseIsInstalled();
        }

        private void EnsureDatabaseIsInstalled()
        {
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            string installUrl = string.Format("{0}install", webHelper.GetStoreLocation());
            if (!webHelper.IsStaticResource(this.Request) &&
                !DataSettingsHelper.DatabaseIsInstalled() &&
                !webHelper.GetThisPageUrl(false).StartsWith(installUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                this.Response.Redirect(installUrl);
            }
        }
        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
