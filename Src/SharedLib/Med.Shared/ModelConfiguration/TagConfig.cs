using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Shared.ModelConfiguration
{
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.RaitingName).IsRequired();

        }
    }
}
