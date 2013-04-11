using System;
using RCSoft.Core.Infrastructure.DependencyManagement;
using RCSoft.Core.Configuration;

namespace RCSoft.Core.Infrastructure
{
    public interface IEngine
    {
        ContainerManager ContainerManager { get; }

        /// <summary>
        /// 初始化环境中的组成和插件
        /// </summary>
        /// <param name="config"></param>
        void Initialize(RCSoftConfig config);

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        T[] ResolveAll<T>();
    }
}
