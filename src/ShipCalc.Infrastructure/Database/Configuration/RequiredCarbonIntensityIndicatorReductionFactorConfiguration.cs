using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Infrastructure.Database;

public class RequiredCarbonIntensityIndicatorReductionFactorConfiguration :
    IEntityTypeConfiguration<RefLineReductionFactor>
{
    public void Configure(EntityTypeBuilder<RefLineReductionFactor> builder)
    {
        builder
            .HasKey(rf => rf.Id);

        builder
            .Property(rf => rf.Id)
            .IsRequired()
            .HasDefaultValueSql("gen_random_uuid()");

        builder
            .Property(rf => rf.Year)
            .IsRequired()
            .HasComment("The year for which the reduction factor applies (2023-2030)");

        builder
            .Property(rf => rf.ReductionFactorPercentage)
            .IsRequired()
            .HasColumnType("integer")
            .HasComment("The reduction factor percentage (Z%) for the CII relative to the 2019 reference line");

    }
}
