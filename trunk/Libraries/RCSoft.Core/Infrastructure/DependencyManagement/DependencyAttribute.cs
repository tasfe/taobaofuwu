using System;

namespace RCSoft.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// 在容器中自动注册一个服务
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependencyAttribute : Attribute
    {
        public DependencyAttribute(ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
        {
            LifeStyle = lifeStyle;
        }
        public DependencyAttribute(Type serviceType, ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton)
        {
            LifeStyle = lifeStyle;
            ServiceType = serviceType;
        }
        /// <summary>
        /// 
        /// </summary>
        public Type ServiceType { get; protected set; }

        public ComponentLifeStyle LifeStyle { get; protected set; }

        public string Key { get; set; }

        public string Configuration { get; set; }

        public virtual void RegisterService(AttributeInfo<DependencyAttribute> attributeInfo, ContainerManager container)
        {
            Type serviceType = attributeInfo.Attribute.ServiceType ?? attributeInfo.DecoratedType;
            container.AddComponent(serviceType, attributeInfo.DecoratedType, attributeInfo.Attribute.Key ?? attributeInfo.DecoratedType.FullName, attributeInfo.Attribute.LifeStyle);
        }
    }
}
