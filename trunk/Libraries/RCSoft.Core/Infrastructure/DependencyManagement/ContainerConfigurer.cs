using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCSoft.Core.Configuration;
using System.Web;

namespace RCSoft.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// 配置控制反转容器与所使用的服务
    /// </summary>
    public class ContainerConfigurer
    {
        /// <summary>
        /// 用已知关键字配置服务
        /// </summary>
        public static class ConfigurationKeys
        {
            /// <summary>
            /// 配置中等信任服务关键字
            /// </summary>
            public const string MediumTrust = "MediumTrust";
            /// <summary>
            /// 配置完全信任服务关键字
            /// </summary>
            public const string FullTrust = "FullTrust";
        }

        public virtual void Configure(IEngine engine, ContainerManager containerManager, EventBroker broker, RCSoftConfig configuration)
        { 
            //其它依赖项
            containerManager.AddComponentInstance<RCSoftConfig>(configuration, "rcsoft.configuration");
            containerManager.AddComponentInstance<IEngine>(engine, "rcsoft.engine");
            containerManager.AddComponentInstance<ContainerConfigurer>(this, "rcsoft.containerConfigurer");

            //查找到的依赖项
            containerManager.AddComponent<ITypeFinder, WebAppTypeFinder>("rcsoft.typeFinder");
            //其他集合注册依赖项
            var typeFinder = containerManager.Resolve<ITypeFinder>();
            containerManager.UpdateContainer(x =>
                {
                    var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
                    var drInstances = new List<IDependencyRegistrar>();
                    foreach (var drType in drTypes)
                        drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));

                    drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
                    foreach (var dependencyRegistrar in drInstances)
                        dependencyRegistrar.Register(x, typeFinder);
                });

            //事件代理注册
            containerManager.AddComponentInstance(broker);

            //服务注册
            containerManager.AddComponent<DependencyAttributeRegistrator>("rcsoft.serviceRegistrator");
            var registrator = containerManager.Resolve<DependencyAttributeRegistrator>();
            var services = registrator.FindServices();
            var configurations = GetComponentConfigurations(configuration);
            services = registrator.FilterServices(services, configurations);
            registrator.RegisterServices(services);
        }

        protected virtual string[] GetComponentConfigurations(RCSoftConfig configuration)
        {
            var configurations = new List<string>();
            string trustConfiguration = (CommonHelper.GetTrustLevel() > AspNetHostingPermissionLevel.Medium)
                ? ConfigurationKeys.FullTrust : ConfigurationKeys.MediumTrust;
            configurations.Add(trustConfiguration);
            return configurations.ToArray();
        }
    }
}
