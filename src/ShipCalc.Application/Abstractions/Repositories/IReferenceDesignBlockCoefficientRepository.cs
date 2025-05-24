using ShipCalc.Domain;
using ShipCalc.Domain.Enums;

namespace ShipCalc.Application.Abstractions
{
    public interface IReferenceDesignBlockCoefficientRepository
    {
        Task<ReferenceDesignBlockCoefficient> GetByIdAsync(Guid id);

        Task<IEnumerable<ReferenceDesignBlockCoefficient>> GetAllAsync();

        Task<IEnumerable<ReferenceDesignBlockCoefficient>> GetByShipTypeAsync(ShipType shipType);

        Task<IEnumerable<ReferenceDesignBlockCoefficient>> GetByDeadweightRangeAsync(int? minDeadweight, int? maxDeadweight);

        Task AddAsync(ReferenceDesignBlockCoefficient coefficient);

        Task UpdateAsync(ReferenceDesignBlockCoefficient coefficient);

        Task DeleteAsync(Guid id);
    }
}
