using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions;
using ShipCalc.Domain.ReductionFactor;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories
{
    public class RequiredCarbonIntensityIndicatorReductionFactorRepository :
        IRequiredCarbonIntensityIndicatorReductionFactorRepository
    {
        private readonly ShipCalcDbContext _context;

        public RequiredCarbonIntensityIndicatorReductionFactorRepository(ShipCalcDbContext context)
        {
            _context = context;
        }

        public async Task<RequiredCarbonIntensityIndicatorReductionFactor> GetByIdAsync(Guid id)
        {
            return await _context.RequiredCarbonIntensityIndicatorReductionFactors
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<RequiredCarbonIntensityIndicatorReductionFactor>> GetAllAsync()
        {
            return await _context.RequiredCarbonIntensityIndicatorReductionFactors
                .ToListAsync();
        }

        public async Task<RequiredCarbonIntensityIndicatorReductionFactor> GetByYearAsync(int year)
        {
            return await _context.RequiredCarbonIntensityIndicatorReductionFactors
                .FirstOrDefaultAsync(f => f.Year == year);
        }

        public async Task<IEnumerable<RequiredCarbonIntensityIndicatorReductionFactor>> GetByYearRangeAsync(int minYear, int maxYear)
        {
            return await _context.RequiredCarbonIntensityIndicatorReductionFactors
                .Where(f => f.Year >= minYear && f.Year <= maxYear)
                .ToListAsync();
        }

        public async Task AddAsync(RequiredCarbonIntensityIndicatorReductionFactor factor)
        {
            await _context.RequiredCarbonIntensityIndicatorReductionFactors.AddAsync(factor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RequiredCarbonIntensityIndicatorReductionFactor factor)
        {
            _context.RequiredCarbonIntensityIndicatorReductionFactors.Update(factor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var factor = await _context.RequiredCarbonIntensityIndicatorReductionFactors
                .FirstOrDefaultAsync(f => f.Id == id);
            if (factor != null)
            {
                _context.RequiredCarbonIntensityIndicatorReductionFactors.Remove(factor);
                await _context.SaveChangesAsync();
            }
        }
    }
}