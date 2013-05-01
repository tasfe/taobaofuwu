using System;
using RCSoft.Web.Models.Customers;
using RCSoft.Core.Domain.Customers;
using AutoMapper;

namespace RCSoft.Web
{
    public static class MappingExtensions
    {
        #region 用户/用户角色
        public static CustomerRoleModel ToModel(this CustomerRole entity)
        {
            return Mapper.Map<CustomerRole, CustomerRoleModel>(entity);
        }
        #endregion
    }
}