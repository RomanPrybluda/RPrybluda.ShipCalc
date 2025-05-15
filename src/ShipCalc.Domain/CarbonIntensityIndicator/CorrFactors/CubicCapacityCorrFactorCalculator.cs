namespace ShipCalc.Domain
{
    public class CubicCapacityCorrFactorCalculator
    {
        public double CalculateCubicCapacityCorrectionFactor(ShipType shipType, double deadweight, double cubicCapacity, double grossTonnage = 0)
        {
            if (deadweight <= 0 || cubicCapacity <= 0)
                throw new ArgumentException("Deadweight and cubic capacity must be greater than zero.");
            if (shipType == ShipType.RoRoPassenger && grossTonnage <= 0)
                throw new ArgumentException("Gross tonnage must be greater than zero for Ro-Ro passenger ships.");

            double r = deadweight / cubicCapacity;
            double f_c = 1.0; // Default value if no specific correction applies

            switch (shipType)
            {
                case ShipType.ChemicalTanker:
                    if (r < 0.98)
                        f_c = Math.Pow(r, 0.7) - 0.014;
                    else
                        f_c = 1.000;
                    break;

                case ShipType.GasCarrier:
                    f_c = r - 0.58;
                    break;

                case ShipType.RoRoPassenger:
                    double dwtGtRatio = deadweight / grossTonnage;
                    if (dwtGtRatio < 0.25)
                        f_c = Math.Pow(dwtGtRatio, -0.8);
                    break;

                case ShipType.BulkCarrier:
                    if (r < 0.55)
                        f_c = r - 0.15;
                    break;
            }

            return f_c < 1.0 ? 1.0 : f_c; // Ensure f_c is not less than 1.0 where applicable
        }
    }
}