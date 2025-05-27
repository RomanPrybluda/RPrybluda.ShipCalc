using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain;

namespace ShipCalc.Infrastructure.Database;

public class ShipConfiguration :
    IEntityTypeConfiguration<Ship>
{
    public void Configure(EntityTypeBuilder<Ship> builder)
    {
        builder
            .HasKey(s => s.Id);

        builder
            .Property(s => s.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.ImoNumber)
            .IsRequired()
            .HasComment("Unique IMO number for the ship");

        builder
            .HasIndex(s => s.ImoNumber)
            .IsUnique();

        builder
            .Property(s => s.ShipName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(s => s.GrossTonnage)
            .IsRequired()
            .HasPrecision(18, 2);

        builder
            .Property(s => s.SummerDeadweight)
            .IsRequired()
            .HasPrecision(18, 2);

        builder
            .Property(s => s.BlockCoefficient)
            .IsRequired()
            .HasPrecision(4, 3);

        builder
            .Property(s => s.BlockCoefficient)
            .HasColumnName("BlockCoefficient");

        builder
            .ToTable("Ships", tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "CK_Ship_BlockCoefficient_Range",
                    "\"BlockCoefficient\" >= 0 AND \"BlockCoefficient\" <= 1");
            });

        builder
            .Property(s => s.CargoCompartmentCubicCapacity)
            .IsRequired()
            .HasPrecision(18, 2);

        builder
            .Property(s => s.ShipType)
            .IsRequired();

        builder
            .Property(s => s.IceClass)
            .IsRequired();
    }
}
