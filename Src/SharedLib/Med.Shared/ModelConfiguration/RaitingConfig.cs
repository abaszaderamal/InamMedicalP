using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Shared.ModelConfiguration
{
    public class RaitingConfig : IEntityTypeConfiguration<Raiting>
    {
        public void Configure(EntityTypeBuilder<Raiting> builder)
        {
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.Title).IsRequired();

        }
    }
}
