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
        throw new NotImplementedException();

        //if (!_context.CapacityIceStrengthCorrFactors.Any())
        //{
        //    var data = CapacityIceStrengthCorrFactorsSeedData.GetData();
        //    _context.CapacityIceStrengthCorrFactors.AddRange(data);
        //    await _context.SaveChangesAsync();
        //}
    }

    public async Task InitializeIASuperAndIAIceCorrFactorAsync()
    {
        throw new NotImplementedException();

        //if (!_context.IASuperAndIAIceCorrFactors.Any())
        //{
        //    var data = IASuperAndIAIceCorrFactorsSeedData.GetData();
        //    _context.IASuperAndIAIceCorrFactors.AddRange(data);
        //    await _context.SaveChangesAsync();
        //}
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
        throw new NotImplementedException();

        //if (!_context.ReductionFactors.Any())
        //{
        //    var data = ReductionFactorsSeedData.GetData();
        //    _context.ReductionFactors.AddRange(data);
        //    await _context.SaveChangesAsync();
        //}
    }

    public async Task InitializeIRefDesignBlockCoeffAsync()
    {
        throw new NotImplementedException();

        //if (!_context.RefDesignBlockCoefficients.Any())
        //{
        //    var data = RefDesignBlockCoeffSeedData.GetData();
        //    _context.RefDesignBlockCoefficients.AddRange(data);
        //    await _context.SaveChangesAsync();
        //}
    }

    public async Task InitializeRefLineParamsAsync()
    {
        throw new NotImplementedException();

        //if (!_context.RefLineParams.Any())
        //{
        //    var data = RefLineParamsSeedData.GetData();
        //    _context.RefLineParams.AddRange(data);
        //    await _context.SaveChangesAsync();
        //}
    }
}
