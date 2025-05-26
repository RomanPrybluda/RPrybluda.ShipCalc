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
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Ship> GetByImoNumberAsync(int imoNumber)
    {
        var ships = await _context.Ships
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ImoNumber == imoNumber);

        if (ships == null)
            throw new ArgumentNullException(nameof(ships));

        return ships;
    }

    public async Task AddAsync(Ship ship)
    {
        if (ship == null)
            throw new ArgumentNullException(nameof(ship));

        var existing = await _context.Ships.AnyAsync(s => s.ImoNumber == ship.ImoNumber);
        if (existing)
            throw new InvalidOperationException($"Ship with IMO number {ship.ImoNumber} already exists.");

        await _context.Ships.AddAsync(ship);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Ship ship)
    {
        if (ship == null)
            throw new ArgumentNullException(nameof(ship));

        var existing = await _context.Ships
            .FirstOrDefaultAsync(s => s.ImoNumber == ship.ImoNumber);

        if (existing == null)
            throw new InvalidOperationException($"Ship with IMO number {ship.ImoNumber} not found.");

        _context.Entry(existing).CurrentValues.SetValues(ship);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int imoNumber)
    {
        var ship = await _context.Ships
            .FirstOrDefaultAsync(s => s.ImoNumber == imoNumber);

        if (ship == null)
            throw new InvalidOperationException($"Ship with IMO number {imoNumber} not found.");

        _context.Ships.Remove(ship);
        await _context.SaveChangesAsync();
    }
}