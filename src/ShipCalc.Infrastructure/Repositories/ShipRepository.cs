using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions;
using ShipCalc.Domain;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories;

public class ShipRepository : IShipRepository
{
    private readonly ShipCalcDbContext _context;

    public ShipRepository(ShipCalcDbContext context)
    {
        _context = context;
    }

    public async Task<Ship?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var ship = await _context.Ships
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        return ship;
    }

    public async Task<Ship?> GetByImoNumberAsync(int imoNumber, CancellationToken cancellationToken = default)
    {
        var ship = await _context.Ships
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ImoNumber == imoNumber, cancellationToken);
        return ship;
    }

    public async Task AddAsync(Ship ship, CancellationToken cancellationToken = default)
    {
        await _context.Ships.AddAsync(ship, cancellationToken);
    }

    public async Task DeleteAsync(int imoNumber, CancellationToken cancellationToken = default)
    {
        var ship = await _context.Ships
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ImoNumber == imoNumber, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}