using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions
{
    public interface IIASuperAndIAIceClassedShipCorrFactorRepository
    {
        Task<IASuperAndIAIceClassedShipCorrFactor> GetByIdAsync(Guid id);

        Task<IEnumerable<IASuperAndIAIceClassedShipCorrFactor>> GetAllAsync();

        Task<IASuperAndIAIceClassedShipCorrFactor> GetByIceClassAsync(IceClass iceClass);

        Task AddAsync(IASuperAndIAIceClassedShipCorrFactor factor);

        Task UpdateAsync(IASuperAndIAIceClassedShipCorrFactor factor);

        Task DeleteAsync(Guid id);
    }
}
