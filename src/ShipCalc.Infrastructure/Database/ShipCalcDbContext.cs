using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions.Data;
using ShipCalc.Domain;
using ShipCalc.Domain.ReductionFactor;

namespace ShipCalc.Infrastructure.Data
{
    public class ShipCalcDbContext : DbContext, IShipCalcDbContext
    {

        public ShipCalcDbContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Ship> Ships { get; set; }

        public DbSet<CarbonIntensityIndicatorCalcRecord> CarbonIntensityIndicatorCalcRecords { get; set; }

        public DbSet<CarbonIntensityIndicatorRatingThreshold> CarbonIntensityIndicatorRatingThresholds { get; set; }

        public DbSet<CarbonIntensityIndicatorReferenceLineParameter> CarbonIntensityIndicatorReferenceLineParameters { get; set; }

        public DbSet<RequiredCarbonIntensityIndicatorReductionFactor> RequiredCarbonIntensityIndicatorReductionFactors { get; set; }

        public DbSet<CapacityIceStrengtheningCorrectionFactor> CapacityIceStrengtheningCorrectionFactors { get; set; }

        public DbSet<IASuperAndIAIceClassedShipCorrFactor> IASuperAndIAIceClassedShipCorrFactors { get; set; }

        public DbSet<ReferenceDesignBlockCoefficient> ReferenceDesignBlockCoefficients { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ShipCalcDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}