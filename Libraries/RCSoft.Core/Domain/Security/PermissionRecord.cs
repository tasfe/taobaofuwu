using RCSoft.Core.Domain.Customers;
using System.Collections.Generic;

namespace RCSoft.Core.Domain.Security
{
    public class PermissionRecord : BaseEntity
    {
        private ICollection<CustomerRole> _customerRoles;

        /// <summary>
        /// 权限名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 权限系统名称
        /// </summary>
        public virtual string SystemName { get; set; }

        /// <summary>
        /// 权限类别
        /// </summary>
        public virtual string Category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRoles ?? (_customerRoles = new List<CustomerRole>()); }
            protected set { _customerRoles = value; }
        }
    }
}
