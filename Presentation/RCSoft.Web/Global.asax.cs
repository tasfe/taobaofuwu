﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RCSoft.Core.Infrastructure;
using RCSoft.Core.Data;
using RCSoft.Core;
using RCSoft.Web.Framework.Mvc;
using RCSoft.Web.Framework.Mvc.Routes;
using FluentValidation.Mvc;
using RCSoft.Web.Framework;
using System.Web.Security;
using System.Security.Principal;

namespace RCSoft.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

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
            //设置默认的路由地址，Application启动时默认首页
            routes.MapRoute(
                "Default",//路由名称
                "{controller}/{action}/{id}",//带参数的URL
                new { colltroller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "RCSoft.Web.Controllers" }
                );
        }
        #region MyRegion
		
        //public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        //{
        //    filters.Add(new HandleErrorAttribute());
        //}

        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //    routes.MapRoute(
        //        "Default", // Route name
        //        "{controller}/{action}/{id}", // URL with parameters
        //        new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
        //    );

        //} 
	#endregion

        protected void Application_Start()
        {
            //初始化引擎上下文
            EngineContext.Initialize(false);
            bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();

            var dependencyResolver = new RCSoftDependencyResolver();
            DependencyResolver.SetResolver(dependencyResolver);

            ModelBinders.Binders.Add(typeof(BaseModel), new RCSoftModelBinder());
            if (databaseInstalled)
            {
                //ViewEngines.Engines.Clear();
                //ViewEngines.Engines.Add(new ThemeableRazorViewEngine());
            }
            ModelMetadataProviders.Current = new RCSoftMetadataProvider();
            AreaRegistration.RegisterAllAreas();
            //注册路由信息
            RegisterRouters(RouteTable.Routes);
            //使用FluentValidation插件
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new RCSoftValidatorFactory()));
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            EnsureDatabaseIsInstalled();
        }
        protected void Application_AuthorizeRequest(object sender,EventArgs e)
        {
            //var userId = Context.User.Identity as FormsIdentity;
            //if (userId != null && userId.IsAuthenticated)
            //{
            //    var roles = userId.Ticket.UserData.Split(',');
            //    Context.User = new GenericPrincipal(userId, roles);
            //}
            //else
            //{
            //    var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            //    string loginlUrl = string.Format("{0}Login", webHelper.GetStoreLocation());
            //    this.Response.Redirect(loginlUrl);
            //}
        }
        private void EnsureDatabaseIsInstalled()
        {
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            string installUrl = string.Format("{0}Install", webHelper.GetStoreLocation());
            if (!webHelper.IsStaticResource(this.Request) &&
                !DataSettingsHelper.DatabaseIsInstalled() &&
                !webHelper.GetThisPageUrl(false).StartsWith(installUrl, StringComparison.InvariantCultureIgnoreCase))
            {
                this.Response.Redirect(installUrl);
            }
        }
    }
}