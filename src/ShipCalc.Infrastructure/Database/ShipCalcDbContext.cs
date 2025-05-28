using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions.Data;
using ShipCalc.Domain;
using ShipCalc.Domain.Calculation.CorrectionFactors;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Infrastructure.Database;

public class ShipCalcDbContext : DbContext, IShipCalcDbContext
{

    public ShipCalcDbContext(DbContextOptions options) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<Ship> Ships { get; set; }

    public DbSet<CalculationData> CalculationDatas { get; set; }

    public DbSet<RatingThreshold> CIIRatingThresholds { get; set; }

    public DbSet<RefLineParams> CIIRefLineParams { get; set; }

    public DbSet<RefLineReductionFactor> CIIReqReductionFactors { get; set; }

    public DbSet<CapacityIceStrengthCorrFactor> CapacityIceStrengthCorrFactors { get; set; }

    public DbSet<IASuperAndIAIceCorrFactor> IASuperAndIAIceCorrFactors { get; set; }

    public DbSet<RefDesignBlockCoeff> RefDesignBlockCoeffs { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ShipCalcDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}