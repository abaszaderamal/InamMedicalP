using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Shared.ModelConfiguration
{
    public class ClinicDoctorConfig : IEntityTypeConfiguration<ClinicDoctor>
    {
        public void Configure(EntityTypeBuilder<ClinicDoctor> builder)
        {
            builder.HasOne(p => p.Clinic)
                .WithMany(cd => cd.ClinicDoctors)
                .HasForeignKey(ci => ci.ClinicId);

            builder.HasOne(p => p.Doctor)
                .WithMany(cd => cd.ClinicDoctors)
                .HasForeignKey(ci => ci.DoctorId);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);

        }
    }
}
