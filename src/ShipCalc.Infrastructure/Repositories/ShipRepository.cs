using ShipCalc.Application.Abstractions;
using ShipCalc.Domain;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories
{
    public class ShipRepository : IShipRepository
    {
        private readonly ShipCalcDbContext _context;

        public ShipRepository(ShipCalcDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task AddAsync(Ship ship)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int imoNumber)
        {
            throw new NotImplementedException();
        }

        public Task<Ship> GetByImoNumberAsync(int imoNumber)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Ship ship)
        {
            throw new NotImplementedException();
        }
    }
}
