using System;
using System.Linq;
using RCSoft.Core.Domain.Customers;
using System.Collections.Generic;
using RCSoft.Core.Data;
using RCSoft.Core;

namespace RCSoft.Services.Customers
{
    public partial class CustomerService : ICustomerService
    {
        #region 定义
        private const string CUSTOMERROLES_ALL_KEY = "RCSoft.customerrole.all-{0}";
        #endregion

        #region 字段
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerRole> _customerRoleRepository;
        #endregion

        #region 构造函数
        public CustomerService(IRepository<Customer> customerRepository, IRepository<CustomerRole> customerRoleRepository)
        {
            this._customerRepository = customerRepository;
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
            return GetAllCustomerRoles(null, showActived);
        }
        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <param name="showActived">是否只获取激活的</param>
        /// <returns>角色</returns>
        public virtual IList<CustomerRole> GetAllCustomerRoles(string roleName, bool showActived = true)
        {
            string key = string.Format(CUSTOMERROLES_ALL_KEY, showActived);
            var query = _customerRoleRepository.Table;
            //是否根据角色名称查找
            if(!string.IsNullOrWhiteSpace(roleName))
                query=query.Where(cr=>cr.Name.Contains(roleName));
            query=query.Where(cr=>cr.Active);
            query = query.OrderBy(cr => cr.Name);
            var customerRoles = query.ToList();
            return customerRoles;
        }

        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="showActived">是否只获取激活的</param>
        /// <returns></returns>
        public virtual IPagedList<CustomerRole> GetAllCustomerRoles(string roleName, int pageIndex, int pageSize, bool showActived = true)
        {
            var roles = GetAllCustomerRoles(roleName, showActived);
            return new PagedList<CustomerRole>(roles, pageIndex, pageSize);
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

        #region 用户

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="customer">用户</param>
        public virtual void InsertCustomer(Customer customer)
        {
            if(customer==null)
                throw new ArgumentNullException("用户");
            _customerRepository.Insert(customer);
        }
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="customer">用户</param>
        public virtual void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("用户");
            _customerRepository.Update(customer);
        }

        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        /// <param name="customerId">用户Id</param>
        /// <returns>一个用户</returns>
        public virtual Customer GetCustomerById(int customerId)
        {
            if (customerId <= 0)
                return null;
            var customer = _customerRepository.GetById(customerId);
            return customer;
        }

        /// <summary>
        /// 根据Email查找用户
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>用户</returns>
        public virtual Customer GetCustomerByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var query = from c in _customerRepository.Table
                        orderby c.Id
                        where c.Email == email
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }
        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户</returns>
        public virtual Customer GetCustomerByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from c in _customerRepository.Table
                        orderby c.Id
                        where c.Username == username
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }
        #endregion
        #endregion
    }
}
