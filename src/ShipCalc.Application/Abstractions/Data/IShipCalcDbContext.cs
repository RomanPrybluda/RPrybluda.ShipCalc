using Microsoft.EntityFrameworkCore;
using ShipCalc.Domain;
using ShipCalc.Domain.ReductionFactor;

namespace ShipCalc.Application.Abstractions.Data
{
    public interface IShipCalcDbContext
    {

        /// <summary>
        /// Entity with parameters for CII calculations
        /// </summary>
        DbSet<Ship> Ships { get; }

        /// <summary>
        /// CII calculation Results
        /// </summary>
        DbSet<CarbonIntensityIndicatorCalcRecord> CarbonIntensityIndicatorCalcRecords { get; }

        /// <summary>
        /// Parameters for determining the Letter CII Rating
        /// </summary>
        DbSet<CarbonIntensityIndicatorRatingThreshold> CarbonIntensityIndicatorRatingThresholds { get; }

        /// <summary>
        /// Parameters for determining the CII Reference Line depending on ship type and ship deadweight
        /// </summary>
        DbSet<CarbonIntensityIndicatorReferenceLineParameter> CarbonIntensityIndicatorReferenceLineParameters { get; }

        /// <summary>
        /// CII Reference Line percentage Reduction Factor depending on year from 2023
        /// </summary>
        DbSet<RequiredCarbonIntensityIndicatorReductionFactor> RequiredCarbonIntensityIndicatorReductionFactors { get; }

        /// <summary>
        /// Reference design block coefficients depending on ship type and ship deadweight
        /// </summary>
        DbSet<ReferenceDesignBlockCoefficient> ReferenceDesignBlockCoefficients { get; }

        /// <summary>
        /// Constants for determining capacity correction factor for ice-strengthning of the hull
        /// </summary>
        DbSet<CapacityIceStrengtheningCorrectionFactor> CapacityIceStrengtheningCorrectionFactors { get; }

        /// <summary>
        /// Correction factors for ice-classed ship having IA Super and IA
        /// </summary>
        DbSet<IASuperAndIAIceClassedShipCorrFactor> IASuperAndIAIceClassedShipCorrFactors { get; }

    }
}
