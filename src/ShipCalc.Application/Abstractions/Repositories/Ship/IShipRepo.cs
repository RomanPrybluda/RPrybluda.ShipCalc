using ShipCalc.Domain;

namespace ShipCalc.Application.Abstractions;

public interface IShipRepo
{
    Task<Ship?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task<Ship?> GetByImoNumberAsync(int imoNumber, CancellationToken cancellationToken = default);

    Task AddAsync(Ship ship, CancellationToken cancellationToken = default);

    Task DeleteAsync(int imoNumber, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
