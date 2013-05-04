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
using RCSoft.Core.Data;
using RCSoft.Data;
using RCSoft.Services.Localization;
using Autofac.Core;

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
            //web辅助类
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerHttpRequest();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //数据层
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            builder.Register(x => new EfDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();

            builder.Register(x => (IEfDataProvider)x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();
            builder.Register(x => (IEfDataProvider)x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IEfDataProvider>().InstancePerDependency();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dataSettingsManager.LoadSettings());
                var dataProvider = (IEfDataProvider)efDataProviderManager.LoadDataProvider();
                dataProvider.InitConnectionFactory();

                builder.Register<IDbContext>(c => new RCSoftObjectContext(dataProviderSettings.DataConnectionString)).InstancePerHttpRequest();
            }
            else
            {
                builder.Register<IDbContext>(c => new RCSoftObjectContext(dataSettingsManager.LoadSettings().DataConnectionString)).InstancePerHttpRequest();
            }

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();


            //工作上下文
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerHttpRequest();

            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();
            //builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerHttpRequest();

            builder.RegisterType<LocalizationService>().As<ILocalizationService>()
                .InstancePerHttpRequest();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
