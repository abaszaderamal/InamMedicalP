using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Shared.ModelConfiguration
{
    public class TodoConfig : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Note).IsRequired();



        }
    }
}
