using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain;

namespace ShipCalc.Infrastructure.Data
{
    public class CarbonIntensityIndicatorRefParametrConfiguration :
        IEntityTypeConfiguration<CarbonIntensityIndicatorRefParameters>
    {
        public void Configure(EntityTypeBuilder<CarbonIntensityIndicatorRefParameters> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.LowerBound)
                .HasPrecision(18, 6);

            builder.Property(ci => ci.UpperBound)
                .HasPrecision(18, 6);

            builder.Property(ci => ci.A)
                .IsRequired()
                .HasPrecision(18, 6);

            builder.Property(ci => ci.C)
                .IsRequired()
                .HasPrecision(18, 6);
        }
    }
}
