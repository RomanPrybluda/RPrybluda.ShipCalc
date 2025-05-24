using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipCalc.Domain;

namespace ShipCalc.Infrastructure.Database
{
    public class IASuperAndIAIceClassedShipCorrFactorConfiguration :
        IEntityTypeConfiguration<IASuperAndIAIceClassedShipCorrFactor>
    {
        public void Configure(EntityTypeBuilder<IASuperAndIAIceClassedShipCorrFactor> builder)
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
}
