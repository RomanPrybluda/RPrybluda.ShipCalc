using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator.CalcResultRecord;

namespace ShipCalc.Infrastructure.Database;

public class CarbonIntensityIndicatorRatingThresholdsConfiguration :
    IEntityTypeConfiguration<RatingThreshold>
{
    public void Configure(EntityTypeBuilder<RatingThreshold> builder)
    {
        builder
            .HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(t => t.ShipType)
            .IsRequired();

        builder
            .Property(t => t.LowerDeadweight)
            .HasPrecision(18, 2);

        builder
            .Property(t => t.UpperDeadweight)
            .HasPrecision(18, 2);

        builder
            .Property(t => t.D1)
            .IsRequired()
            .HasPrecision(3, 2)
            .HasComment("Threshold D1 for CII rating");

        builder
            .Property(t => t.D2)
            .IsRequired()
            .HasPrecision(3, 2)
            .HasComment("Threshold D2 for CII rating");

        builder
            .Property(t => t.D3)
            .IsRequired()
            .HasPrecision(3, 2)
            .HasComment("Threshold D3 for CII rating");

        builder
            .Property(t => t.D4)
            .IsRequired()
            .HasPrecision(3, 2)
            .HasComment("Threshold D4 for CII rating");

        builder
            .HasIndex(t => new { t.ShipType, t.LowerDeadweight, t.UpperDeadweight })
            .IsUnique()
            .HasDatabaseName("IX_CarbonIntensityIndicatorRatingThresholds_ShipType_Deadweight");
    }
}