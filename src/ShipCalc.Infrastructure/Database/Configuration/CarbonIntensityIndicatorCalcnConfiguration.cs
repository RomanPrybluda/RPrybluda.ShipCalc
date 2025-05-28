using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Infrastructure.Database;

public class CarbonIntensityIndicatorCalcnConfiguration :
    IEntityTypeConfiguration<CarbonIntensityIndicatorCalculation>
{
    public void Configure(EntityTypeBuilder<CarbonIntensityIndicatorCalculation> builder)
    {
        builder
            .HasKey(ci => ci.Id);

        builder
            .Property(ci => ci.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(ci => ci.Co2EmissionsInTons)
            .IsRequired()
            .HasPrecision(10, 3);

        builder.Property(ci => ci.DistanceTravelledInNMs)
            .IsRequired()
            .HasPrecision(10, 3);

        builder.Property(ci => ci.Capacity)
            .IsRequired()
            .HasPrecision(10, 3);

        builder.Property(ci => ci.RefLineParameterA)
            .IsRequired()
            .HasPrecision(10, 3);

        builder.Property(ci => ci.RefLineParameterC)
            .IsRequired()
            .HasPrecision(10, 3);

        builder.Property(ci => ci.RefLine)
            .IsRequired()
            .HasPrecision(10, 3);

        builder.Property(ci => ci.RequiredCarbonIntensityIndicator)
            .IsRequired()
            .HasPrecision(10, 3);

        builder.Property(ci => ci.IceClasedShipCapacityCorrFactor)
            .IsRequired()
            .HasPrecision(10, 3);

        builder.Property(ci => ci.IASuperAndIAIceCorrFactor)
            .IsRequired()
            .HasPrecision(10, 3);

        builder.Property(ci => ci.AttainedCarbonIntensityIndicator)
            .IsRequired()
            .HasPrecision(10, 3);

        builder.Property(ci => ci.CarbonIntensityIndicatorNumericalRating)
            .IsRequired()
            .HasPrecision(10, 3);

        builder.Property(ci => ci.Year)
            .IsRequired();

        builder.Property(ci => ci.RefLineReductionFactor)
            .IsRequired();

        builder.Property(ci => ci.CarbonIntensityIndicatorRating)
            .IsRequired();

        builder.Property(ci => ci.CalculationDate)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(ci => ci.ShipId)
            .IsRequired()
            .HasComment("Foreign key referencing the associated Ship");

        builder.HasOne<Ship>()
             .WithOne()
             .HasForeignKey<CarbonIntensityIndicatorCalculation>(ci => ci.ShipId)
             .OnDelete(DeleteBehavior.Cascade);
    }
}