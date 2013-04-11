using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCSoft.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// 标记一个属性的装饰类型
    /// </summary>
    /// <typeparam name="T">属性类型</typeparam>
    public  class AttributeInfo<T>
    {
        /// <summary>
        /// 属性检索所得类型描述符
        /// </summary>
        public T Attribute { get; set; }
        /// <summary>
        /// 特定类型的属性描述
        /// </summary>
        public Type DecoratedType { get; set; }
    }
}
