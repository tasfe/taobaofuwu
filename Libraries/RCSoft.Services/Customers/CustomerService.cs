using System;
using System.Linq;
using RCSoft.Core.Domain.Customers;
using System.Collections.Generic;
using RCSoft.Core.Data;

namespace RCSoft.Services.Customers
{
    public partial class CustomerService : ICustomerService
    {
        #region 定义
        private const string CUSTOMERROLES_ALL_KEY = "RCSoft.customerrole.all-{0}";
        #endregion

        #region 字段
        private readonly IRepository<CustomerRole> _customerRoleRepository;
        #endregion

        #region 构造函数
        public CustomerService(IRepository<CustomerRole> customerRoleRepository)
        {
            this._customerRoleRepository = customerRoleRepository;
        }
        #endregion

        #region 方法

        #region 角色

        /// <summary>
        /// 根据ID获取角色
        /// </summary>
        /// <param name="customerRoleId">角色ID</param>
        /// <returns>角色</returns>
        public CustomerRole GetCustomerRoleById(int customerRoleId)
        {
            if (customerRoleId == 0)
                return null;

            //string key=string.Format()

            var customerRole = _customerRoleRepository.GetById(customerRoleId);
            return customerRole;
        }

        /// <summary>
        /// 根据角色名称查找角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色</returns>
        public CustomerRole GetCustomerRoleByName(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return null;

            var query = from cr in _customerRoleRepository.Table
                        orderby cr.Id
                        where cr.Name == roleName
                        select cr;
            var customerRole = query.FirstOrDefault();
            return customerRole;
        }

        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <param name="showActived">是否只获取激活的</param>
        /// <returns>角色</returns>
        public virtual IList<CustomerRole> GetAllCustomerRoles(bool showActived = true)
        {
            string key = string.Format(CUSTOMERROLES_ALL_KEY, showActived);
            var query = from cr in _customerRoleRepository.Table
                        orderby cr.Name
                        where (cr.Active)
                        select cr;
            var customerRoles = query.ToList();
            return customerRoles;
        }

        /// <summary>
        /// 创建一个角色
        /// </summary>
        /// <param name="customerRole">角色实体</param>
        public virtual void InsertCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException("角色");
            _customerRoleRepository.Insert(customerRole);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="customerRole">角色实体</param>
        public void UpdateCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException("角色");
            _customerRoleRepository.Update(customerRole);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="customerRole">角色实体</param>
        public void DeleteCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException("角色");
            _customerRoleRepository.Delete(customerRole);
        }
        #endregion 
        #endregion
    }
}
