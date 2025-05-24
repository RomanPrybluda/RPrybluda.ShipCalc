using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions
{
    public interface IReferenceDesignBlockCoefficientRepository
    {
        Task<ReferenceDesignBlockCoefficient> GetByIdAsync(Guid id);

        Task<IEnumerable<ReferenceDesignBlockCoefficient>> GetAllAsync();

        Task<ReferenceDesignBlockCoefficient> GetByShipTypeAndDeadweightAsync(ShipType shipType, decimal deadWeight);

        Task AddAsync(ReferenceDesignBlockCoefficient coefficient);

        Task UpdateAsync(ReferenceDesignBlockCoefficient coefficient);

        Task DeleteAsync(Guid id);
    }
}
