using System.Collections.Generic;
using RCSoft.Core.Domain.Security;
using RCSoft.Core.Domain.Customers;

namespace RCSoft.Services.Security
{
    public partial interface IPermissionService
    {
        /// <summary>
        /// 删除一个权限
        /// </summary>
        /// <param name="permission"></param>
        void DeletePermissionRecord(PermissionRecord permission);

        /// <summary>
        /// 根据编号获取一个权限
        /// </summary>
        /// <param name="permissionId">权限唯一编号</param>
        /// <returns>权限</returns>
        PermissionRecord GetPermissionRecordById(int permissionId);

        /// <summary>
        /// 根据名称获取一个权限
        /// </summary>
        /// <param name="systemName">权限名称</param>
        /// <returns>权限</returns>
        PermissionRecord GetPermissionRecordBySystemName(string systemName);
        
        /// <summary>
        /// 获取全部权限
        /// </summary>
        /// <returns>权限集合</returns>
        IList<PermissionRecord> GetAllPermissionRecords();

        /// <summary>
        /// 插入一个权限
        /// </summary>
        /// <param name="permission">权限</param>
        void InsertPermissionRecord(PermissionRecord permission);

        /// <summary>
        /// 更新一个权限
        /// </summary>
        /// <param name="permission">权限</param>
        void UpdatePermissionRecord(PermissionRecord permission);

        /// <summary>
        /// 授权许可
        /// </summary>
        /// <param name="permission">权限</param>
        /// <returns>true - 授权; otherwiswe, 未授权</returns>
        bool Authorize(PermissionRecord permission);

        /// <summary>
        /// 授权许可
        /// </summary>
        /// <param name="permission">权限</param>
        /// <param name="customer">用户</param>
        /// <returns>true - 授权; otherwiswe, 未授权</returns>
        bool Authorize(PermissionRecord permission, Customer customer);

        /// <summary>
        /// 授权许可
        /// </summary>
        /// <param name="permission">权限系统名称</param>
        /// <returns>true - 授权; otherwiswe, 未授权</returns>
        bool Authorize(string permissionRecordSystemName);


        /// <summary>
        /// 授权许可
        /// </summary>
        /// <param name="permissionRecordSystemName">权限系统名称</param>
        /// <param name="customer">用户</param>
        /// <returns>true - 授权; otherwiswe, 未授权</returns>
        bool Authorize(string permissionRecordSystemName, Customer customer);
    }
}
