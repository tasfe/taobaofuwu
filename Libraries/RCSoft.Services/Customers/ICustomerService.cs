using RCSoft.Core.Domain.Customers;
using System.Collections.Generic;

namespace RCSoft.Services.Customers
{
    public interface ICustomerService
    {

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
