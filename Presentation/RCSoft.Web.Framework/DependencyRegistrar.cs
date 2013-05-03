using System;
using System.Collections.Generic;
using System.Linq;
using RCSoft.Core.Infrastructure.DependencyManagement;
using Autofac;
using RCSoft.Core.Infrastructure;
using System.Web;
using RCSoft.Web.Framework.Mvc.Routes;
using RCSoft.Core;
using Autofac.Integration.Mvc;
using RCSoft.Core.Fakes;
using RCSoft.Services.Customers;

namespace RCSoft.Web.Framework
{
    public class DependencyRegistrar:IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.Register(c => HttpContext.Current != null ? (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) : (new FakeHttpContext("~/") as HttpContextBase)).As<HttpContextBase>().InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
               .As<HttpRequestBase>()
               .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerHttpRequest();

            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerHttpRequest();
            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerHttpRequest();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
