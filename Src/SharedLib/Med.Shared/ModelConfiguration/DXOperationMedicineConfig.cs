using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Shared.ModelConfiguration
{
    public class DXOperationMedicineConfig : IEntityTypeConfiguration<DXOperationMedicine>
    {
        public void Configure(EntityTypeBuilder<DXOperationMedicine> builder)
        {
            builder.HasOne(p => p.DXOperation)
                .WithMany(cd => cd.DXOperationMedicines)
                .HasForeignKey(ci => ci.DXOperationId);

            builder.HasOne(p => p.Medicine)
                .WithMany(cd => cd.DXOperationMedicine)
                .HasForeignKey(ci => ci.MedicineId);

            builder.Property(p=>p.IsDeleted).HasDefaultValue(false);
        }
    }
}
