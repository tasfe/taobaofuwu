using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace RCSoft.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// 在解决方案逆反控制开始时注册服务
    /// </summary>
    public class DependencyAttributeRegistrator
    {
        private readonly ITypeFinder _finder;
        private readonly IEngine _engine;

        public DependencyAttributeRegistrator(ITypeFinder finder, IEngine engine)
        {
            this._finder = finder;
            this._engine = engine;
        }

        public virtual IEnumerable<AttributeInfo<DependencyAttribute>> FindServices()
        {
            foreach (Type type in _finder.FindClassesOfType<object>())
            {
                var attributes = type.GetCustomAttributes(typeof(DependencyAttribute), false);
                foreach (DependencyAttribute attribute in attributes)
                {
                    yield return new AttributeInfo<DependencyAttribute> { Attribute = attribute, DecoratedType = type };
                }
            }
        }

        public virtual void RegisterServices(IEnumerable<AttributeInfo<DependencyAttribute>> services)
        {
            foreach (var info in services)
            {
                info.Attribute.RegisterService(info, _engine.ContainerManager);
            }
        }

        public virtual IEnumerable<AttributeInfo<DependencyAttribute>> FilterServices(IEnumerable<AttributeInfo<DependencyAttribute>> services, params string[] configruationKeys)
        {
            return services.Where(s => s.Attribute.Configuration == null || configruationKeys.Contains(s.Attribute.Configuration));
        }
    }
}
