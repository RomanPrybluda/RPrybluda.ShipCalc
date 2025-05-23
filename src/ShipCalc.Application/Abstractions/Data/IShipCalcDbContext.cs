using Microsoft.EntityFrameworkCore;
using ShipCalc.Domain;

namespace ShipCalc.Application.Abstractions.Data
{
    public interface IShipCalcDbContext
    {
        DbSet<Ship> Ships { get; }

        DbSet<CarbonIntensityIndicatorCalcRecord> CarbonIntensityIndicatorCalcRecords { get; }

        DbSet<CarbonIntensityIndicatorRefParameters> CarbonIntensityIndicatorRefParametrs { get; }

        DbSet<CarbonIntensityIndicatorRatingThresholds> CarbonIntensityIndicatorRatingThresholds { get; }
    }
}
