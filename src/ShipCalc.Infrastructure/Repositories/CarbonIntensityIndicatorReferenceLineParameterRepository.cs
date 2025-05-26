using Microsoft.EntityFrameworkCore;
using ShipCalc.Application.Abstractions.Repositories;
using ShipCalc.Domain;
using ShipCalc.Domain.Enums;
using ShipCalc.Infrastructure.Data;

namespace ShipCalc.Infrastructure.Repositories;

public class CarbonIntensityIndicatorReferenceLineParameterRepository :
    ICarbonIntensityIndicatorReferenceLineParameterRepository
{
    private readonly ShipCalcDbContext _context;

    public CarbonIntensityIndicatorReferenceLineParameterRepository(ShipCalcDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CarbonIntensityIndicatorReferenceLineParameter?> GetByIdAsync(Guid id)
    {
        return await _context.CarbonIntensityIndicatorReferenceLineParameters
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<CarbonIntensityIndicatorReferenceLineParameter>> GetAllAsync()
    {
        return await _context.CarbonIntensityIndicatorReferenceLineParameters
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<CarbonIntensityIndicatorReferenceLineParameter> GetParametersByShipTypeAndCapacityAsync(
        ShipType shipType,
        decimal capacity)
    {
        if (shipType == null)
            throw new ArgumentNullException(nameof(shipType));

        if (capacity == null)
            throw new ArgumentNullException(nameof(capacity));

        var parameters = await _context.CarbonIntensityIndicatorReferenceLineParameters
            .AsNoTracking()
            .Where(p => p.ShipType == shipType &&
                        (!p.LowerBound.HasValue || capacity >= p.LowerBound.Value) &&
                        (!p.UpperBound.HasValue || capacity < p.UpperBound.Value))
            .FirstOrDefaultAsync();

        if (parameters == null)
            throw new ArgumentNullException("Parameters not found.");

        return parameters;
    }

    public async Task AddAsync(CarbonIntensityIndicatorReferenceLineParameter parameter)
    {
        if (parameter == null)
            throw new ArgumentNullException(nameof(parameter));

        parameter.Id = Guid.NewGuid();
        await _context.CarbonIntensityIndicatorReferenceLineParameters.AddAsync(parameter);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CarbonIntensityIndicatorReferenceLineParameter parameter)
    {
        if (parameter == null)
            throw new ArgumentNullException(nameof(parameter));

        var existing = await _context.CarbonIntensityIndicatorReferenceLineParameters
            .FirstOrDefaultAsync(p => p.Id == parameter.Id);

        if (existing == null)
            throw new InvalidOperationException($"Parameter with ID {parameter.Id} not found.");

        _context.Entry(existing).CurrentValues.SetValues(parameter);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var parameter = await _context.CarbonIntensityIndicatorReferenceLineParameters
            .FirstOrDefaultAsync(p => p.Id == id);

        if (parameter == null)
            throw new InvalidOperationException($"Parameter with ID {id} not found.");

        _context.CarbonIntensityIndicatorReferenceLineParameters.Remove(parameter);
        await _context.SaveChangesAsync();
    }
}