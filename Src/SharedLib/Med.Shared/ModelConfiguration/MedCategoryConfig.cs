using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Shared.ModelConfiguration
{
    public class MedCategoryConfig : IEntityTypeConfiguration<MedCategory>
    {
        public void Configure(EntityTypeBuilder<MedCategory> builder)
        {
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
