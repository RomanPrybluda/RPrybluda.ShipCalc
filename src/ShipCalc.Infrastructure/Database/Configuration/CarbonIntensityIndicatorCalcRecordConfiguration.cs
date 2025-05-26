using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain; // Assuming Ship entity is in this namespace
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Infrastructure.Database;

public class CarbonIntensityIndicatorCalcRecordConfiguration :
    IEntityTypeConfiguration<CalculationData>
{
    public void Configure(EntityTypeBuilder<CalculationData> builder)
    {
        builder
            .HasKey(ci => ci.Id);

        builder
            .Property(ci => ci.Id)
            .HasDefaultValueSql("NEWID()");

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
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(ci => ci.ShipId)
            .IsRequired()
            .HasComment("Foreign key referencing the associated Ship");

        builder.HasOne<Ship>()
             .WithOne()
             .HasForeignKey<CalculationData>(ci => ci.ShipId)
             .OnDelete(DeleteBehavior.Cascade);
    }
}