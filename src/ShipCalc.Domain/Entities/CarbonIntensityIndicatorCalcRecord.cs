namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorCalcRecord
    {
        public Guid Id { get; set; }

        public ShipType ShipType { get; set; }

        public IceClass? IceClass { get; set; }

        public double Deadweight { get; set; }

        public double GrossTonnage { get; set; }

        public double Capacity { get; set; }

        public double BlockCoefficient { get; set; }

        public int Year { get; set; }

        public double CarbonIntensityIndicatorRef { get; set; }

        public double RequiredCarbonIntensityIndicator { get; set; }

        public double IceClasedShipCapacityCorrFactor { get; set; }

        public double CubicCapacityCorrectionFactor { get; set; }

        public double IASuperAndIAIceClassedShipCorrFactor { get; set; }

        public DateTime CalculationDate { get; set; }

        public CarbonIntensityIndicatorCalcRecord()
        {
            CalculationDate = DateTime.UtcNow;
        }

        public Guid ShipId { get; set; }

        public Ship Ship { get; set; }
    }
}
