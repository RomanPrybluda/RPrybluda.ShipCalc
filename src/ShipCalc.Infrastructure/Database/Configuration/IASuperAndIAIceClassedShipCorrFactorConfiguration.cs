using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain.Calculation.CorrectionFactors;

namespace ShipCalc.Infrastructure.Database;

public class IASuperAndIAIceClassedShipCorrFactorConfiguration :
    IEntityTypeConfiguration<IASuperAndIAIceCorrFactor>
{
    public void Configure(EntityTypeBuilder<IASuperAndIAIceCorrFactor> builder)
    {
        builder
            .HasKey(cf => cf.Id);

        builder
            .Property(cf => cf.Id)
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(cf => cf.IceClass)
            .IsRequired();

        builder
            .Property(cf => cf.CorrectionFactor)
            .IsRequired()
            .HasColumnType("decimal(3,2)");

    }
}
