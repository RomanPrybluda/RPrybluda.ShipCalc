using ShipCalc.Domain;

namespace ShipCalc.Application.Abstractions;

public interface IShipRepository
{
    Task<Ship> GetByImoNumberAsync(int imoNumber);

    Task AddAsync(Ship ship);

    Task UpdateAsync(Ship ship);

    Task DeleteAsync(int imoNumber);
}
