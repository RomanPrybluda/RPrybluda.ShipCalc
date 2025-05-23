using ShipCalc.Application.Abstractions.Repositories;
using ShipCalc.Domain;
using ShipCalc.Domain.Enums;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories
{
    public class CarbonIntensityIndicatorRatingThresholdsRepository : ICarbonIntensityIndicatorRatingThresholdsRepository
    {
        private readonly ShipCalcDbContext _context;

        public CarbonIntensityIndicatorRatingThresholdsRepository(ShipCalcDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<CarbonIntensityIndicatorRatingThresholds> GetThresholdsAsync(ShipType shipType, double deadWeight)
        {
            throw new NotImplementedException();
        }
    }
}
