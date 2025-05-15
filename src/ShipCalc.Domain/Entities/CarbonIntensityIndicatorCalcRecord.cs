namespace ShipCalc.Domain
{
    public class CarbonIntensityIndicatorCalcRecord
    {
        public Guid Id { get; set; }

        public ShipType ShipType { get; set; } // Type of the ship

        public IceClass? IceClass { get; set; } // Optional ice class (null if not applicable)

        public double Deadweight { get; set; } // Deadweight tonnage (DWT)

        public double GrossTonnage { get; set; } // Gross tonnage (GT)

        public double CubicCapacity { get; set; } // Total cubic capacity of cargo tanks/holds (m³)

        public double BlockCoefficient { get; set; } // C_b, block coefficient of the ship

        public int Year { get; set; } // Year for RequiredCII calculation

        public double CiiRef { get; set; } // Calculated reference CII

        public double RequiredCii { get; set; } // Calculated required CII

        public double Fi { get; set; } // Capacity correction factor for ice-classed ships

        public double Fc { get; set; } // Cubic capacity correction factor

        public double Fm { get; set; } // Factor for IA/IA Super ice-classed ships

        public DateTime CalculationDate { get; set; } // Date and time of calculation

        public CarbonIntensityIndicatorCalcRecord()
        {
            CalculationDate = DateTime.UtcNow; // Set to current UTC time by default
        }

        public Guid ShipId { get; set; }

        public Ship Ship { get; set; }
    }
}
