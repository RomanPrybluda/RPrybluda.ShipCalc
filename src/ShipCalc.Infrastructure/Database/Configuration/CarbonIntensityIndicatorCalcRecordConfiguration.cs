using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain;

namespace ShipCalc.Infrastructure.Data
{
    public class CarbonIntensityIndicatorCalcRecordConfiguration : IEntityTypeConfiguration<CarbonIntensityIndicatorCalcRecord>
    {
        public void Configure(EntityTypeBuilder<CarbonIntensityIndicatorCalcRecord> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.CarbonIntensityIndicatorRef)
                .IsRequired()
                .HasPrecision(18, 3);

            builder.Property(ci => ci.RequiredCarbonIntensityIndicator)
                .IsRequired()
                .HasPrecision(18, 3);

            builder.Property(ci => ci.IceClasedShipCapacityCorrFactor)
                .IsRequired()
                .HasPrecision(18, 3);

            builder.Property(ci => ci.CubicCapacityCorrectionFactor)
                .IsRequired()
                .HasPrecision(18, 3);

            builder.Property(ci => ci.IASuperAndIAIceClassedShipCorrFactor)
                .IsRequired()
                .HasPrecision(18, 3);

            builder.Property(ci => ci.CalculationDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(ci => ci.ShipId)
                .IsRequired()
                .HasComment("Foreign key referencing the associated Ship");

            builder.HasOne<Ship>()
                .WithOne()
                .HasForeignKey<CarbonIntensityIndicatorCalcRecord>(ci => ci.ShipId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
