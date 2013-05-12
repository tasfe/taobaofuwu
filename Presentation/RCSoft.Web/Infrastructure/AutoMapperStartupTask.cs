using AutoMapper;
using RCSoft.Core.Infrastructure;
using RCSoft.Core.Domain.Customers;
using RCSoft.Web.Models.Customers;
using RCSoft.Core.Domain.Products;
using RCSoft.Web.Models.Products;

namespace RCSoft.Web.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            Mapper.CreateMap<CustomerRole, CustomerRoleModel>()
                .ForMember(c => c.ParentRoles, opt => opt.Ignore());
            Mapper.CreateMap<CustomerRoleModel, CustomerRole>();

            Mapper.CreateMap<Category, CategoryModel>()
                .ForMember(c => c.ParentCategories, opt => opt.Ignore());
            Mapper.CreateMap<CategoryModel, Category>();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
