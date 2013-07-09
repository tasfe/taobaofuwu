using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCSoft.Core.Domain.Customers
{
    public partial class Customer : BaseEntity
    {
        private ICollection<CustomerRole> _customerRoles;
        private ICollection<ExternalAuthenticationRecord> _externalAuthenticationRecords;

        public Customer()
        {
            this.CustomerGuid = Guid.NewGuid();
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual Guid CustomerGuid { get; set; }

        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        /// <summary>
        /// 设置用户是否被激活
        /// </summary>
        public virtual bool Active { get; set; }
        /// <summary>
        /// 设置用户是否已经被删除
        /// </summary>
        public virtual bool Deleted { get; set; }

        /// <summary>
        /// 标识用户是否是系统用户
        /// </summary>
        public virtual bool IsSystemAccount { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public virtual string SystemName { get; set; }
        /// <summary>
        /// 记录用户最后一次登录的IP地址
        /// </summary>
        public virtual string LastIpAddress { get; set; }
        /// <summary>
        /// 用户创建时间
        /// </summary>
        public virtual DateTime CreateOnDate { get; set; }
        /// <summary>
        /// 记录用户最后一次登录时间
        /// </summary>
        public virtual DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// 用户使用第三方登录
        /// </summary>
        public virtual ICollection<ExternalAuthenticationRecord> ExternalAuthenticationRecords
        {
            get { return _externalAuthenticationRecords ?? (_externalAuthenticationRecords = new List<ExternalAuthenticationRecord>()); }
            protected set { _externalAuthenticationRecords = value; }
        }

        /// <summary>
        /// 用户角色
        /// </summary>
        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRoles ?? (_customerRoles = new List<CustomerRole>()); }
            protected set { _customerRoles = value; }
        }
    }
}
