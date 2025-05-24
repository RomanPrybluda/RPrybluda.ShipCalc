using Microsoft.EntityFrameworkCore;
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
            _context = context;
        }

        public async Task<CarbonIntensityIndicatorRatingThreshold> GetThresholdsAsync(ShipType shipType, decimal deadWeight)
        {
            int deadWeightInt = (int)Math.Round(deadWeight);
            return await _context.CarbonIntensityIndicatorRatingThresholds
                .FirstOrDefaultAsync(t => t.ShipType == shipType &&
                                         (!t.LowerDeadweight.HasValue || deadWeight >= (decimal)t.LowerDeadweight) &&
                                         (!t.UpperDeadweight.HasValue || deadWeight < (decimal)t.UpperDeadweight));
        }

        public async Task<IEnumerable<CarbonIntensityIndicatorRatingThreshold>> GetAllAsync()
        {
            return await _context.CarbonIntensityIndicatorRatingThresholds
                .ToListAsync();
        }

        public async Task AddAsync(CarbonIntensityIndicatorRatingThreshold threshold)
        {
            await _context.CarbonIntensityIndicatorRatingThresholds.AddAsync(threshold);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CarbonIntensityIndicatorRatingThreshold threshold)
        {
            _context.CarbonIntensityIndicatorRatingThresholds.Update(threshold);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var threshold = await _context.CarbonIntensityIndicatorRatingThresholds
                .FirstOrDefaultAsync(t => t.Id == id);
            if (threshold != null)
            {
                _context.CarbonIntensityIndicatorRatingThresholds.Remove(threshold);
                await _context.SaveChangesAsync();
            }
        }
    }
}