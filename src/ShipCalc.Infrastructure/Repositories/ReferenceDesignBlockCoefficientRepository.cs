using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions;
using ShipCalc.Domain;
using ShipCalc.Domain.Enums;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories
{
    public class ReferenceDesignBlockCoefficientRepository : IReferenceDesignBlockCoefficientRepository
    {
        private readonly ShipCalcDbContext _context;

        public ReferenceDesignBlockCoefficientRepository(ShipCalcDbContext context)
        {
            _context = context;
        }

        public async Task<ReferenceDesignBlockCoefficient> GetByIdAsync(Guid id)
        {
            return await _context.ReferenceDesignBlockCoefficients
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<ReferenceDesignBlockCoefficient>> GetAllAsync()
        {
            return await _context.ReferenceDesignBlockCoefficients
                .ToListAsync();
        }

        public async Task<IEnumerable<ReferenceDesignBlockCoefficient>> GetByShipTypeAsync(ShipType shipType)
        {
            return await _context.ReferenceDesignBlockCoefficients
                .Where(c => c.ShipType == shipType)
                .ToListAsync();
        }

        public async Task<IEnumerable<ReferenceDesignBlockCoefficient>> GetByDeadweightRangeAsync(int? minDeadweight, int? maxDeadweight)
        {
            return await _context.ReferenceDesignBlockCoefficients
                .Where(c => (!minDeadweight.HasValue || c.MinDeadweight >= minDeadweight) &&
                            (!maxDeadweight.HasValue || c.MaxDeadweight <= maxDeadweight))
                .ToListAsync();
        }

        public async Task AddAsync(ReferenceDesignBlockCoefficient coefficient)
        {
            await _context.ReferenceDesignBlockCoefficients.AddAsync(coefficient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReferenceDesignBlockCoefficient coefficient)
        {
            _context.ReferenceDesignBlockCoefficients.Update(coefficient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var coefficient = await _context.ReferenceDesignBlockCoefficients
                .FirstOrDefaultAsync(c => c.Id == id);
            if (coefficient != null)
            {
                _context.ReferenceDesignBlockCoefficients.Remove(coefficient);
                await _context.SaveChangesAsync();
            }
        }
    }
}