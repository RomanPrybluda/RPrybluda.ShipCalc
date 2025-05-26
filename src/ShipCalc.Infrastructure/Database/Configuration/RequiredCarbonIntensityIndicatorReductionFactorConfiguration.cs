using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain.ReductionFactor;

namespace ShipCalc.Infrastructure.Database;

public class RequiredCarbonIntensityIndicatorReductionFactorConfiguration :
    IEntityTypeConfiguration<RequiredCarbonIntensityIndicatorReductionFactor>
{
    public void Configure(EntityTypeBuilder<RequiredCarbonIntensityIndicatorReductionFactor> builder)
    {
        builder
            .HasKey(rf => rf.Id);

        builder
            .Property(rf => rf.Id)
            .IsRequired()
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(rf => rf.Year)
            .IsRequired()
            .HasComment("The year for which the reduction factor applies (2023-2030)");

        builder
            .Property(rf => rf.ReductionFactorPercentage)
            .IsRequired()
            .HasColumnType("int(2,0)")
            .HasComment("The reduction factor percentage (Z%) for the CII relative to the 2019 reference line");

    }
}
