using RCSoft.Core.Domain.Customers;
using System.Collections.Generic;
using RCSoft.Core.Data;
using System;

namespace RCSoft.Services.Customers
{
    public partial class CustomerService : ICustomerService
    {
        #region 字段
        private readonly IRepository<CustomerRole> _customerRoleRepository;
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
                        where cr.Name = roleName
                        select cr;
            var customerRole = query.FirstOrDefault();
            return customerRole;
        }

        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <param name="showActived">是否只获取激活的</param>
        /// <returns>角色</returns>
        public IList<CustomerRole> GetAllCustomerRoles(bool showActived = true)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 创建一个角色
        /// </summary>
        /// <param name="customerRole">角色实体</param>
        public void InsertCustomerRole(CustomerRole customerRole)
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
