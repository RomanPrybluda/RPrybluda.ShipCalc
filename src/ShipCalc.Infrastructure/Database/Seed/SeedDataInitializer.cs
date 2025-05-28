using ShipCalc.Application.Abstractions.Data;
using ShipCalc.Application.SeedData;

namespace ShipCalc.Infrastructure.Database;

public class SeedDataInitializer : ISeedDataInitializer
{
    private readonly ShipCalcDbContext _context;

    public SeedDataInitializer(ShipCalcDbContext context)
    {
        _context = context;
    }

    public async Task InitializeCapacityIceStrengthCorrFactorAsync()
    {
        if (!_context.CapacityIceStrengthCorrFactors.Any())
        {
            var data = CapacityIceStrengthCorrFactorsSeedData.GetData();
            _context.CapacityIceStrengthCorrFactors.AddRange(data);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitializeIASuperAndIAIceCorrFactorAsync()
    {
        if (!_context.IASuperAndIAIceCorrFactors.Any())
        {
            var data = IASuperAndIAIceCorrFactorsSeedData.GetData();
            _context.IASuperAndIAIceCorrFactors.AddRange(data);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitializeRatingThresholdsAsync()
    {
        if (!_context.CIIRatingThresholds.Any())
        {
            var data = RatingThresholdsSeedData.GetData();
            _context.CIIRatingThresholds.AddRange(data);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitializeIReductionFactorAsync()
    {
        if (!_context.CIIReqReductionFactors.Any())
        {
            var data = ReductionFactorsSeedData.GetData();
            _context.CIIReqReductionFactors.AddRange(data);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitializeIRefDesignBlockCoeffAsync()
    {
        if (!_context.RefDesignBlockCoeffs.Any())
        {
            var data = RefDesignBlockCoeffSeedData.GetData();
            _context.RefDesignBlockCoeffs.AddRange(data);
            await _context.SaveChangesAsync();
        }
    }

    public async Task InitializeRefLineParamsAsync()
    {
        if (!_context.CIIRefLineParams.Any())
        {
            var data = RefLineParamsSeedData.GetData();
            _context.CIIRefLineParams.AddRange(data);
            await _context.SaveChangesAsync();
        }
    }
}
