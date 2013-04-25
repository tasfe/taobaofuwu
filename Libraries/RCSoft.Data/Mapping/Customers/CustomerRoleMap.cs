using System.Data.Entity.ModelConfiguration;
using RCSoft.Core.Domain.Customers;

namespace RCSoft.Data
{
    public partial class CustomerRoleMap : EntityTypeConfiguration<CustomerRole>
    {
        public CustomerRoleMap()
        {
            this.ToTable("CustomerRole");
            this.HasKey(cr => cr.Id);
            this.Property(cr => cr.Name).IsRequired().HasMaxLength(255);
        }
    }
}
