using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain.Calculation.CorrectionFactors;

namespace ShipCalc.Infrastructure.Database;

public class CapacityIceStrengtheningCorrectionFactorConfiguration :
    IEntityTypeConfiguration<CapacityIceStrengthCorrFactor>
{
    public void Configure(EntityTypeBuilder<CapacityIceStrengthCorrFactor> builder)
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
            .Property(cf => cf.ConstantA)
            .IsRequired()
            .HasColumnType("decimal(5,4)");

        builder
            .Property(cf => cf.ConstantB)
            .IsRequired()
            .HasColumnType("decimal(4,1)");

    }
}
