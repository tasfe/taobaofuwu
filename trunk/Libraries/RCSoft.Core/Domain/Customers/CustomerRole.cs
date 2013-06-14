using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCSoft.Core.Domain.Security;

namespace RCSoft.Core.Domain.Customers
{
    public partial class CustomerRole : BaseEntity
    {
        private ICollection<PermissionRecord> _permissionRecords;
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

        public virtual ICollection<PermissionRecord> PermissionRecords
        {
            get { return _permissionRecords ?? (_permissionRecords = new List<PermissionRecord>()); }
            protected set { _permissionRecords = value; }
        }
    }
}
