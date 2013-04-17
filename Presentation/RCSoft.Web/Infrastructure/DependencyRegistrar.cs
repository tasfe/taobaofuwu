using System;
using RCSoft.Core.Infrastructure.DependencyManagement;
using Autofac;
using Autofac.Integration.Mvc;
using RCSoft.Core.Infrastructure;
using RCSoft.Web.Infrastructure.Installation;

namespace RCSoft.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<InstallationLocalizationService>().As<IInstallationLocalizationService>().InstancePerHttpRequest();
        }

        public int Order
        {
            get { return 2; }
        }
    }
}