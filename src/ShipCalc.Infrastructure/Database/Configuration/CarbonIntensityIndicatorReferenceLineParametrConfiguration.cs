using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain;

namespace ShipCalc.Infrastructure.Database
{
    public class CarbonIntensityIndicatorReferenceLineParametrConfiguration :
        IEntityTypeConfiguration<CarbonIntensityIndicatorReferenceLineParameter>
    {
        public void Configure(EntityTypeBuilder<CarbonIntensityIndicatorReferenceLineParameter> builder)
        {
            builder
                .HasKey(ci => ci.Id);

            builder
                .Property(ci => ci.Id)
                .HasDefaultValueSql("NEWID()");

            builder
                .Property(ci => ci.LowerBound)
                .HasPrecision(18, 6);

            builder
                .Property(ci => ci.UpperBound)
                .HasPrecision(18, 6);

            builder
                .Property(ci => ci.ParameterA)
                .IsRequired()
                .HasPrecision(18, 6);

            builder
                .Property(ci => ci.ParameterC)
                .IsRequired()
                .HasPrecision(18, 6);
        }
    }
}
