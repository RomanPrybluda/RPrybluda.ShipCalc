using Microsoft.EntityFrameworkCore;
using ShipCalc.Domain;
using ShipCalc.Domain.Calculation.CorrectionFactors;
using ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

namespace ShipCalc.Application.Abstractions.Data;

public interface IShipCalcDbContext
{

    /// <summary>
    /// Entity with parameters for CII calculations
    /// </summary>
    DbSet<Ship> Ships { get; }

    /// <summary>
    /// Carbon Intensity Indicator calculation Results
    /// </summary>
    DbSet<CalculationData> CalculationDatas { get; }

    /// <summary>
    /// Parameters for determining the Letter Carbon Intensity Indicator Rating
    /// </summary>
    DbSet<RatingThreshold> CIIRatingThresholds { get; }

    /// <summary>
    /// Parameters for determining the Carbon Intensity Indicator Reference Line depending on ship type and ship deadweight
    /// </summary>
    DbSet<RefLineParams> CIIRefLineParams { get; }

    /// <summary>
    /// Carbon Intensity Indicator Reference Line percentage Reduction Factor depending on year from 2023
    /// </summary>
    DbSet<RefLineReductionFactor> CIIReqReductionFactors { get; }

    /// <summary>
    /// Reference design block coefficients depending on ship type and ship deadweight
    /// </summary>
    DbSet<RefDesignBlockCoeff> RefDesignBlockCoeffs { get; }

    /// <summary>
    /// Constants for determining capacity correction factor for ice-strengthning of the hull
    /// </summary>
    DbSet<CapacityIceStrengthCorrFactor> CapacityIceStrengthCorrFactors { get; }

    /// <summary>
    /// Correction factors for ice-classed ship having IA Super and IA
    /// </summary>
    DbSet<IASuperAndIAIceCorrFactor> IASuperAndIAIceCorrFactors { get; }

}
