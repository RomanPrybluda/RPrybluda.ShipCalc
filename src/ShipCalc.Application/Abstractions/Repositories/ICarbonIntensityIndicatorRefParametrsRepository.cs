using ShipCalc.Domain;

namespace ShipCalc.Application.Abstractions.Repositories
{
    public interface ICarbonIntensityIndicatorRefParametrsRepository
    {
        Task<CarbonIntensityIndicatorRefParameters> GetByIdAsync(Guid id);

        Task<IEnumerable<CarbonIntensityIndicatorRefParameters>> GetByShipIdAsync(Guid shipId);

        Task<IEnumerable<CarbonIntensityIndicatorRefParameters>> GetAllAsync();

        Task AddAsync(CarbonIntensityIndicatorRefParameters parametrs);

        Task UpdateAsync(CarbonIntensityIndicatorRefParameters parametrs);

        Task DeleteAsync(Guid id);
    }
}
