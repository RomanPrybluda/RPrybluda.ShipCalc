using ShipCalc.Application.Abstractions.Repositories;
using ShipCalc.Domain;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories
{
    public class CarbonIntensityIndicatorRefParametrsRepository : ICarbonIntensityIndicatorRefParametrsRepository
    {
        private readonly ShipCalcDbContext _context;

        public CarbonIntensityIndicatorRefParametrsRepository(ShipCalcDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task AddAsync(CarbonIntensityIndicatorRefParameters parametrs)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CarbonIntensityIndicatorRefParameters>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CarbonIntensityIndicatorRefParameters> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CarbonIntensityIndicatorRefParameters>> GetByShipIdAsync(Guid shipId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CarbonIntensityIndicatorRefParameters parametrs)
        {
            throw new NotImplementedException();
        }
    }
}
