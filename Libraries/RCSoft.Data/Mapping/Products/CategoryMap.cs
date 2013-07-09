using System.Data.Entity.ModelConfiguration;
using RCSoft.Core.Domain.Products;

namespace RCSoft.Data.Mapping.Products
{
    public partial class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            this.ToTable("Category");
            this.HasKey(c => c.Id);
            this.Property(c => c.Name).IsRequired().HasMaxLength(400);
            this.Property(c => c.Description).IsMaxLength();
            this.Property(c => c.PictureUrl).HasMaxLength(255);
        }
    }
}
