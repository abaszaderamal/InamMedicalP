using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Shared.ModelConfiguration
{
    public class DXOperationConfig : IEntityTypeConfiguration<DXOperation>
    {
        public void Configure(EntityTypeBuilder<DXOperation> builder)
        {
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Note).IsRequired();
        }
    }
}
