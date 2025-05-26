using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions;

public interface ICapacityIceStrengtheningCorrectionFactorRepository
{
    Task<CapacityIceStrengtheningCorrectionFactor> GetByIdAsync(Guid id);

    Task<IEnumerable<CapacityIceStrengtheningCorrectionFactor>> GetAllAsync();

    Task<CapacityIceStrengtheningCorrectionFactor> GetByIceClassAsync(IceClass iceClass);

    Task AddAsync(CapacityIceStrengtheningCorrectionFactor factor);

    Task UpdateAsync(CapacityIceStrengtheningCorrectionFactor factor);

    Task DeleteAsync(Guid id);
}
