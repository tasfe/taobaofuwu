using System;
using System.Collections.Generic;
using System.Linq;
using RCSoft.Core.Domain.Security;
using RCSoft.Core.Domain;
using RCSoft.Core.Data;
using RCSoft.Core.Domain.Customers;
using RCSoft.Services.Customers;
using RCSoft.Core;

namespace RCSoft.Services.Security
{
    public partial class PermissionService:IPermissionService
    {
        #region 字段
        private readonly IRepository<PermissionRecord> _permissionRecordRepository;
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;
        #endregion

        #region 构造函数
        public PermissionService(IRepository<PermissionRecord> permissionRecordRepository, ICustomerService customerService, IWorkContext workContext)
        {
            this._permissionRecordRepository = permissionRecordRepository;
            this._customerService = customerService;
            this._workContext = workContext;
        }
        #endregion

        #region 方法
        protected virtual bool Authorize(string permissionRecordSystemName, CustomerRole customerRole)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
                return false;
            foreach (var permission in customerRole.PermissionRecords)
                if (permission.SystemName.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            return false;
        }

        /// <summary>
        /// 删除一个权限
        /// </summary>
        /// <param name="permission"></param>
        public virtual void DeletePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException("权限");
            _permissionRecordRepository.Delete(permission);
        }
        /// <summary>
        /// 根据编号获取一个权限
        /// </summary>
        /// <param name="permissionId">权限唯一编号</param>
        /// <returns>权限</returns>
        public virtual PermissionRecord GetPermissionRecordById(int permissionId)
        {
            if (permissionId == 0)
                return null;

            return _permissionRecordRepository.GetById(permissionId);
        }
        /// <summary>
        /// 根据名称获取权限
        /// </summary>
        /// <param name="systemName">权限名称</param>
        /// <returns>权限</returns>
        public virtual PermissionRecord GetPermissionRecordBySystemName(string systemName)
        {
            if (String.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from pr in _permissionRecordRepository.Table
                        where pr.SystemName == systemName
                        orderby pr.Id
                        select pr;

            var permissionRecord = query.FirstOrDefault();
            return permissionRecord;
        }
        /// <summary>
        /// 获取全部权限
        /// </summary>
        /// <returns>权限集合</returns>
        public virtual IList<PermissionRecord> GetAllPermissionRecords()
        {
            var query = from pr in _permissionRecordRepository.Table
                        orderby pr.Name
                        select pr;
            var permissions = query.ToList();
            return permissions;
        }
        /// <summary>
        /// 插入一个权限
        /// </summary>
        /// <param name="permission">权限</param>
        public virtual void InsertPermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException("权限");

            _permissionRecordRepository.Insert(permission);
        }
        /// <summary>
        /// 更新一个权限
        /// </summary>
        /// <param name="permission">权限</param>
        public virtual void UpdatePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException("权限");

            _permissionRecordRepository.Update(permission);
        }
        /// <summary>
        /// 授权许可
        /// </summary>
        /// <param name="permission">权限</param>
        /// <returns>true - 授权; otherwiswe, 未授权</returns>
        public virtual bool Authorize(PermissionRecord permission)
        {
            return Authorize(permission, _workContext.CurrentCustomer);
        }
        /// <summary>
        /// 授权许可
        /// </summary>
        /// <param name="permission">权限</param>
        /// <param name="customer">用户</param>
        /// <returns>true - 授权; otherwiswe, 未授权</returns>
        public bool Authorize(PermissionRecord permission, Customer customer)
        {
            if (permission == null)
                return false;
            if (customer == null)
                return false;
            return Authorize(permission.SystemName, customer);
        }
        /// <summary>
        /// 授权许可
        /// </summary>
        /// <param name="permission">权限系统名称</param>
        /// <returns>true - 授权; otherwiswe, 未授权</returns>
        public bool Authorize(string permissionRecordSystemName)
        {
            return Authorize(permissionRecordSystemName, _workContext.CurrentCustomer);
        }
        /// <summary>
        /// 授权许可
        /// </summary>
        /// <param name="permissionRecordSystemName">权限系统名称</param>
        /// <param name="customer">用户</param>
        /// <returns>true - 授权; otherwiswe, 未授权</returns>
        public bool Authorize(string permissionRecordSystemName, Customer customer)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
                return false;
            var customerRoles = customer.CustomerRoles.Where(cr => cr.Active);
            foreach (var role in customerRoles)
                if (Authorize(permissionRecordSystemName, role))
                    return true;
            return false;
        }
        #endregion
    }
}
