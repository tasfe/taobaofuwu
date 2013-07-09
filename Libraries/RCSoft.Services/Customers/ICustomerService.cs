using RCSoft.Core.Domain.Customers;
using System.Collections.Generic;
using RCSoft.Core;

namespace RCSoft.Services.Customers
{
    public interface ICustomerService
    {
        #region 用户
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="customer">用户</param>
        void InsertCustomer(Customer customer);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="customer">用户</param>
        void UpdateCustomer(Customer customer);

        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        /// <param name="customerId">用户Id</param>
        /// <returns>一个用户</returns>
        Customer GetCustomerById(int customerId);

        /// <summary>
        /// 根据Email查找用户
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>用户</returns>
        Customer GetCustomerByEmail(string email);

        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户</returns>
        Customer GetCustomerByUsername(string username);

        #endregion

        #region 角色
        /// <summary>
        /// 根据ID获取角色
        /// </summary>
        /// <param name="customerRoleId">角色ID</param>
        /// <returns>角色</returns>
        CustomerRole GetCustomerRoleById(int customerRoleId);

        /// <summary>
        /// 根据角色名称查找角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色</returns>
        CustomerRole GetCustomerRoleByName(string roleName);

        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <param name="showActived">是否只获取激活的</param>
        /// <returns>角色</returns>
        IList<CustomerRole> GetAllCustomerRoles(bool showActived = true);

        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="showActived">是否只获取激活的</param>
        /// <returns></returns>
        IPagedList<CustomerRole> GetAllCustomerRoles(string roleName, int pageIndex, int pageSize, bool showActived = true);

        /// <summary>
        /// 创建一个角色
        /// </summary>
        /// <param name="customerRole">角色实体</param>
        void InsertCustomerRole(CustomerRole customerRole);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="customerRole">角色实体</param>
        void UpdateCustomerRole(CustomerRole customerRole);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="customerRole">角色实体</param>
        void DeleteCustomerRole(CustomerRole customerRole);
        #endregion
    }
}
