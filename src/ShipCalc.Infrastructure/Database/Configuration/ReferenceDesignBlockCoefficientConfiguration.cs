using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain.Calculation.CorrectionFactors;

namespace ShipCalc.Infrastructure.Database;

public class ReferenceDesignBlockCoefficientConfiguration :
    IEntityTypeConfiguration<RefDesignBlockCoeff>
{
    public void Configure(EntityTypeBuilder<RefDesignBlockCoeff> builder)
    {
        builder
            .HasKey(bc => bc.Id);

        builder
            .Property(bc => bc.Id)
            .IsRequired()
            .HasDefaultValueSql("gen_random_uuid()");

        builder
            .Property(bc => bc.ShipType)
            .IsRequired();

        builder
            .Property(bc => bc.MinDeadweight)
            .HasColumnType("integer");

        builder
            .Property(bc => bc.MaxDeadweight)
            .HasColumnType("integer");

        builder
            .Property(bc => bc.BlockCoefficient)
            .IsRequired()
            .HasPrecision(3, 2);

    }
}
