using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Shared.ModelConfiguration
{
    public class ClinicConfig : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.Address).IsRequired();
            builder.Property(p => p.Number).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
