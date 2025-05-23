using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions.Data;
using ShipCalc.Domain;

namespace ShipCalc.Infrastructure.Data
{
    public class ShipCalcDbContext : DbContext, IShipCalcDbContext
    {

        public ShipCalcDbContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Ship> Ships { get; set; }

        public DbSet<CarbonIntensityIndicatorRefParameters> CarbonIntensityIndicatorRefParametrs { get; set; }

        public DbSet<CarbonIntensityIndicatorCalcRecord> CarbonIntensityIndicatorCalcRecords { get; set; }

        public DbSet<CarbonIntensityIndicatorRatingThresholds> CarbonIntensityIndicatorRatingThresholds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ShipCalcDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}