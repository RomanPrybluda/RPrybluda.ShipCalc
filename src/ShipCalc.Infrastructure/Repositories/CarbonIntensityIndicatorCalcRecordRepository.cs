using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions;
using ShipCalc.Domain;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories
{
    public class CarbonIntensityIndicatorCalcRecordRepository : ICarbonIntensityIndicatorCalcRecordRepository
    {
        private readonly ShipCalcDbContext _context;

        public CarbonIntensityIndicatorCalcRecordRepository(ShipCalcDbContext context)
        {
            _context = context;
        }

        public async Task<CarbonIntensityIndicatorCalcRecord> GetByIdAsync(Guid id)
        {
            return await _context.CarbonIntensityIndicatorCalcRecords
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<CarbonIntensityIndicatorCalcRecord>> GetByShipIdAsync(Guid shipId)
        {
            return await _context.CarbonIntensityIndicatorCalcRecords
                .Where(r => r.ShipId == shipId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CarbonIntensityIndicatorCalcRecord>> GetAllAsync()
        {
            return await _context.CarbonIntensityIndicatorCalcRecords
                .ToListAsync();
        }

        public async Task AddAsync(CarbonIntensityIndicatorCalcRecord record)
        {
            await _context.CarbonIntensityIndicatorCalcRecords.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CarbonIntensityIndicatorCalcRecord record)
        {
            _context.CarbonIntensityIndicatorCalcRecords.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var record = await _context.CarbonIntensityIndicatorCalcRecords
                .FirstOrDefaultAsync(r => r.Id == id);
            if (record != null)
            {
                _context.CarbonIntensityIndicatorCalcRecords.Remove(record);
                await _context.SaveChangesAsync();
            }
        }
    }
}