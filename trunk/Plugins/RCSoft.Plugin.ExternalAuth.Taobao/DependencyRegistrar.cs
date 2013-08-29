using Autofac;
using Autofac.Integration.Mvc;
using RCSoft.Core.Infrastructure.DependencyManagement;
using RCSoft.Core.Infrastructure;
using RCSoft.Plugin.ExternalAuth.Taobao.Core;

namespace RCSoft.Plugin.ExternalAuth.Taobao
{
    public class DependencyRegistrar:IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<TaobaoProviderAuthorizer>().As<IOAuthProviderTaobaoAuthorizer>().InstancePerHttpRequest();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
