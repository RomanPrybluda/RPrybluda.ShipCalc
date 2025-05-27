namespace ShipCalc.Domain.Abstractions.CarbonIntensityIndicator;

public interface IAttainedCarbonIntensityIndicatorCalculator
{
    decimal IceClasedShipCapacityCorrFactor { get; }

    decimal IASuperAndIAIceClassedShipCorrFactor { get; }

    decimal AttainedCarbonIntensityIndicator { get; }

    Task CalculateAttainedCarbonIntensityIndicator(
    Ship ship,
    decimal capacity,
    decimal co2EmissionsInTons,
    decimal distanceTravelledInNMs);
}
