using System.Data.Entity.ModelConfiguration;
using RCSoft.Core.Domain.Localization;

namespace RCSoft.Data.Mapping.Localization
{
    public partial class LocaleStringResourceMap:EntityTypeConfiguration<LocaleStringResource>
    {
        public LocaleStringResourceMap()
        {
            this.ToTable("LocaleStringResource");
            this.HasKey(lsr => lsr.Id);
            this.Property(lsr => lsr.ResourceName).IsRequired().HasMaxLength(200);
            this.Property(lsr => lsr.ResourceName).IsRequired().IsMaxLength();
            
        }
    }
}
