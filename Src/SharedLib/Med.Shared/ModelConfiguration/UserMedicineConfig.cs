using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Shared.ModelConfiguration
{
    public class UserMedicineConfig : IEntityTypeConfiguration<UserMedicine>
    {
        public void Configure(EntityTypeBuilder<UserMedicine> builder)
        {
            builder.HasOne(p => p.Medicine)
                .WithMany(cd => cd.UserMedicines)
                .HasForeignKey(ci => ci.MedicineId);

            builder.HasOne(p => p.AppUser)
                .WithMany(cd => cd.UserMedicines)
                .HasForeignKey(ci => ci.AppUserId);

            builder.Property(p => p.IsDeleted).HasDefaultValue(false);

        }
    }
}
