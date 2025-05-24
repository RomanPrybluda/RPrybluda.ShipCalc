using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain.Result
{
    public class CarbonIntensityIndicatorCalcResult
    {
        public decimal Capacity { get; set; }

        public decimal ReferenceLineParameterA { get; set; }

        public decimal ReferenceLineParameterC { get; set; }

        public decimal ReferenceLine { get; set; }

        public int ReferenceLineReductionFactor { get; set; }

        public decimal RequiredCarbonIntensityIndicator { get; set; }

        public decimal IceClasedShipCapacityCorrFactor { get; set; }

        public decimal IASuperAndIAIceClassedShipCorrFactor { get; set; }

        public decimal AttainedCarbonIntensityIndicator { get; set; }

        public decimal CarbonIntensityIndicatorNumericalRating { get; set; }

        public CarbonIntensityIndicatorRating CarbonIntensityIndicatorRating { get; set; }

    }
}
