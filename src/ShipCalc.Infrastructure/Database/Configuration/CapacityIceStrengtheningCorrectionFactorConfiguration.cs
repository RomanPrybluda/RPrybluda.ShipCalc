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
            .HasDefaultValueSql("gen_random_uuid()");

        builder
            .Property(cf => cf.IceClass)
            .IsRequired();

        builder
            .Property(cf => cf.ConstantA)
            .IsRequired()
            .HasPrecision(5, 4);

        builder
            .Property(cf => cf.ConstantB)
            .IsRequired()
            .HasPrecision(4, 1);

    }
}
