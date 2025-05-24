using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions;
using ShipCalc.Domain;
using ShipCalc.Domain.Enums;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories
{
    public class CapacityIceStrengtheningCorrectionFactorRepository :
        ICapacityIceStrengtheningCorrectionFactorRepository
    {
        private readonly ShipCalcDbContext _context;

        public CapacityIceStrengtheningCorrectionFactorRepository(ShipCalcDbContext context)
        {
            _context = context;
        }

        public async Task<CapacityIceStrengtheningCorrectionFactor> GetByIdAsync(Guid id)
        {
            var correctionFactor = await _context.CapacityIceStrengtheningCorrectionFactors
                .FirstOrDefaultAsync(f => f.Id == id);

            if (correctionFactor == null)
                throw new ArgumentNullException(nameof(correctionFactor));

            return correctionFactor;
        }

        public async Task<IEnumerable<CapacityIceStrengtheningCorrectionFactor>> GetAllAsync()
        {
            return await _context.CapacityIceStrengtheningCorrectionFactors
                .ToListAsync();
        }

        public async Task<IEnumerable<CapacityIceStrengtheningCorrectionFactor>> GetByIceClassAsync(IceClass iceClass)
        {
            return await _context.CapacityIceStrengtheningCorrectionFactors
                .Where(f => f.IceClass == iceClass)
                .ToListAsync();
        }

        public async Task AddAsync(CapacityIceStrengtheningCorrectionFactor factor)
        {
            await _context.CapacityIceStrengtheningCorrectionFactors.AddAsync(factor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CapacityIceStrengtheningCorrectionFactor factor)
        {
            _context.CapacityIceStrengtheningCorrectionFactors.Update(factor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var factor = await _context.CapacityIceStrengtheningCorrectionFactors
                .FirstOrDefaultAsync(f => f.Id == id);
            if (factor != null)
            {
                _context.CapacityIceStrengtheningCorrectionFactors.Remove(factor);
                await _context.SaveChangesAsync();
            }
        }
    }
}