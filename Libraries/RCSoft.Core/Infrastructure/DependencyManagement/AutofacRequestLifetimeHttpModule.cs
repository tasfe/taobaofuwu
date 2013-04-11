using System;
using System.Web;
using Autofac;

namespace RCSoft.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// 为<see cref="IHttpModule"/> 和 <see cref="ILifetimeScopeProvider"/>实现
    /// 中每个HTTP请求创建嵌套的生命周期
    /// </summary>
    public class AutofacRequestLifetimeHttpModule : IHttpModule
    {
        public static readonly object HttpRequestTag = "httpRequest";
        /// <summary>
        /// 处理请求模块
        /// </summary>
        /// <param name="context">一个 <see cref="T:System.Web.HttpApplication"/> that provides access to the 
        /// methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.EndRequest += ContextEndRequest;
        }
        /// <summary>
        /// Gets a nested lifetime scope that services can be resolved from.
        /// </summary>
        /// <param name="container">The parent container.</param>
        /// <param name="configurationAction">Action on a <see cref="ContainerBuilder"/>
        /// that adds component registations visible only in nested lifetime scopes.</param>
        /// <returns>A new or existing nested lifetime scope.</returns>
        public static ILifetimeScope GetLifetimeScope(ILifetimeScope container, Action<ContainerBuilder> configurationAction)
        {
            //little hack here to get dependencies when HttpContext is not available
            if (HttpContext.Current != null)
            {
                return LifetimeScope ?? (LifetimeScope = InitializeLifetimeScope(configurationAction, container));
            }
            else
            {
                //throw new InvalidOperationException("HttpContextNotAvailable");
                return InitializeLifetimeScope(configurationAction, container);
            }
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
        }

        static ILifetimeScope LifetimeScope
        {
            get
            {
                return (ILifetimeScope)HttpContext.Current.Items[typeof(ILifetimeScope)];
            }
            set
            {
                HttpContext.Current.Items[typeof(ILifetimeScope)] = value;
            }
        }

        static void ContextEndRequest(object sender, EventArgs e)
        {
            ILifetimeScope lifetimeScope = LifetimeScope;
            if (lifetimeScope != null)
                lifetimeScope.Dispose();
        }

        static ILifetimeScope InitializeLifetimeScope(Action<ContainerBuilder> configurationAction, ILifetimeScope container)
        {
            return (configurationAction == null)
                ? container.BeginLifetimeScope(HttpRequestTag)
                : container.BeginLifetimeScope(HttpRequestTag, configurationAction);
        }
    }
}
