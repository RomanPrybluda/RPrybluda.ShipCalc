using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions;
using ShipCalc.Domain;
using ShipCalc.Domain.Enums;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories
{
    public class IASuperAndIAIceClassedShipCorrFactorRepository : IIASuperAndIAIceClassedShipCorrFactorRepository
    {
        private readonly ShipCalcDbContext _context;

        public IASuperAndIAIceClassedShipCorrFactorRepository(ShipCalcDbContext context)
        {
            _context = context;
        }

        public async Task<IASuperAndIAIceClassedShipCorrFactor> GetByIdAsync(Guid id)
        {
            return await _context.IASuperAndIAIceClassedShipCorrFactors
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<IASuperAndIAIceClassedShipCorrFactor>> GetAllAsync()
        {
            return await _context.IASuperAndIAIceClassedShipCorrFactors
                .ToListAsync();
        }

        public async Task<IEnumerable<IASuperAndIAIceClassedShipCorrFactor>> GetByIceClassAsync(IceClass iceClass)
        {
            return await _context.IASuperAndIAIceClassedShipCorrFactors
                .Where(f => f.IceClass == iceClass)
                .ToListAsync();
        }

        public async Task AddAsync(IASuperAndIAIceClassedShipCorrFactor factor)
        {
            await _context.IASuperAndIAIceClassedShipCorrFactors.AddAsync(factor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IASuperAndIAIceClassedShipCorrFactor factor)
        {
            _context.IASuperAndIAIceClassedShipCorrFactors.Update(factor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var factor = await _context.IASuperAndIAIceClassedShipCorrFactors
                .FirstOrDefaultAsync(f => f.Id == id);
            if (factor != null)
            {
                _context.IASuperAndIAIceClassedShipCorrFactors.Remove(factor);
                await _context.SaveChangesAsync();
            }
        }
    }
}