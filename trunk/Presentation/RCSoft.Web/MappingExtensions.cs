using System;
using RCSoft.Web.Models.Customers;
using RCSoft.Core.Domain.Customers;
using RCSoft.Web.Models.Products;
using RCSoft.Core.Domain.Products;
using AutoMapper;

namespace RCSoft.Web
{
    public static class MappingExtensions
    {
        #region 产品
        #endregion
        #region 分类
        public static CategoryModel ToModel(this Category entity)
        {
            return Mapper.Map<Category, CategoryModel>(entity);
        }

        public static Category ToEntity(this CategoryModel model)
        {
            return Mapper.Map<CategoryModel, Category>(model);
        }

        #endregion

        #region 用户/用户角色
        public static CustomerRoleModel ToModel(this CustomerRole entity)
        {
            return Mapper.Map<CustomerRole, CustomerRoleModel>(entity);
        }
        public static CustomerRole ToEntity(this CustomerRoleModel model)
        {
            //Mapper.CreateMap<CustomerRoleModel, CustomerRole>().ForMember(c => c.ParentRoles, cm => cm.Ignore());
            return Mapper.Map<CustomerRoleModel, CustomerRole>(model);
        }
        public static CustomerRole ToEntity(this CustomerRoleModel model, CustomerRole destination)
        {
            return Mapper.Map(model, destination);
        }
        #endregion
    }
}