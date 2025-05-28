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
            .HasColumnName("block_coefficient");

        builder
            .ToTable("ships", tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "ck_ship_block_coefficient_range",
                    "\"block_coefficient\" >= 0 AND \"block_coefficient\" <= 1");
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
