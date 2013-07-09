using System;
using System.Data.Entity.ModelConfiguration;
using RCSoft.Core.Domain.Customers;

namespace RCSoft.Data.Mapping.Customers
{
    public partial class CustomerMap:EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.ToTable("Customer");
            this.HasKey(c => c.Id);
            this.Property(u => u.Username).HasMaxLength(250);
            this.Property(u => u.Email).HasMaxLength(250);
            this.Property(u => u.Password);


            this.HasMany(c => c.CustomerRoles)
                .WithMany()
                .Map(m => m.ToTable("Customer_Role_Mapping"));

        }
    }
}
