using AutoMapper;
using RCSoft.Core.Infrastructure;
using RCSoft.Core.Domain.Customers;
using RCSoft.Web.Models.Customers;

namespace RCSoft.Web.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            Mapper.CreateMap<CustomerRole, CustomerRoleModel>()
                .ForMember(c => c.ParentRoles, opt => opt.Ignore());
            Mapper.CreateMap<CustomerRoleModel, CustomerRole>();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
