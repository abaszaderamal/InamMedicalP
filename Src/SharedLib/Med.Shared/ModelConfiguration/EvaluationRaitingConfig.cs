using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Shared.ModelConfiguration
{
    public class EvaluationRaitingConfig : IEntityTypeConfiguration<EvaluationRating>
    {
        public void Configure(EntityTypeBuilder<EvaluationRating> builder)
        {
            builder.HasOne(p => p.Evaluation)
                .WithMany(cd => cd.EvaluationRatings)
                .HasForeignKey(ci => ci.EvaluationId);

            builder.HasOne(p => p.Raiting)
                .WithMany(cd => cd.EvaluationRatings)
                .HasForeignKey(ci => ci.RatingId);

            builder.Property(p => p.IsDeleted).HasDefaultValue(false);

        }
    }
}
