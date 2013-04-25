using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCSoft.Core.Domain.Customers
{
    public partial class CustomerRole : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual string Name { get; set; }
        
        /// <summary>
        /// 父角色编号
        /// </summary>
        public virtual int ParentRoleId { get; set; }

        /// <summary>
        /// 设置用户角色是否激活
        /// </summary>
        public virtual bool Active { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public virtual string Description { get; set; }
    }
}
