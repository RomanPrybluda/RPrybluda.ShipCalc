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
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(bc => bc.ShipType)
            .IsRequired();

        builder
            .Property(bc => bc.MinDeadweight)
            .HasColumnType("int");

        builder
            .Property(bc => bc.MaxDeadweight)
            .HasColumnType("int");

        builder
            .Property(bc => bc.BlockCoefficient)
            .IsRequired()
            .HasColumnType("decimal(3,2)");

    }
}
