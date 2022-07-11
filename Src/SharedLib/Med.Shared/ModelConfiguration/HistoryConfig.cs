using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Shared.ModelConfiguration
{
    public class HistoryConfig : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
        }
    }
}
