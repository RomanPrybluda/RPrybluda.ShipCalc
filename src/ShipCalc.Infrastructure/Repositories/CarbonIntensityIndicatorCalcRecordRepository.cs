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
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task AddAsync(CarbonIntensityIndicatorCalcRecord record)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CarbonIntensityIndicatorCalcRecord>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CarbonIntensityIndicatorCalcRecord> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CarbonIntensityIndicatorCalcRecord>> GetByShipIdAsync(Guid shipId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CarbonIntensityIndicatorCalcRecord record)
        {
            throw new NotImplementedException();
        }
    }
}