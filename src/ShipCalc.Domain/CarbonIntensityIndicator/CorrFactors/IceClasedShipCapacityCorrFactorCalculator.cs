namespace ShipCalc.Domain
{
    public class IceClasedShipCapacityCorrFactorCalculator
    {
        public double CalculateCapacityCorrectionFactor(IceClass iceClass, double deadweight, double cb, ShipType shipType)
        {
            if (deadweight <= 0)
                throw new ArgumentException("DWT must be greater than zero.");
            if (cb <= 0 || cb > 1)
                throw new ArgumentException("Block coefficient (C_b) must be between 0 and 1.");

            // Step 1: Calculate f_i(ice class) from Table 2
            double fIceClass = iceClass switch
            {
                IceClass.IC => 1.0041 + 58.5 / deadweight,
                IceClass.IB => 1.0067 + 62.1 / deadweight,
                IceClass.IA => 1.0099 + 95.1 / deadweight,
                IceClass.IASuper => 1.0151 + 228.7 / deadweight,
                _ => throw new ArgumentException($"Unsupported ice class: {iceClass}")
            };

            // Step 2: Calculate f_icb
            double fIcb;
            if (shipType == ShipType.BulkCarrier || shipType == ShipType.Tanker || shipType == ShipType.GeneralCargoShip)
            {
                double cbReference = GetCbReferenceDesign(shipType, deadweight);
                fIcb = cbReference / cb;
                fIcb = Math.Max(fIcb, 1.0); // Ensure f_icb is not less than 1.0
            }
            else
            {
                fIcb = 1.0; // For other ship types
            }

            // Step 3: Calculate final f_i
            return fIceClass * fIcb;
        }

        private double GetCbReferenceDesign(ShipType shipType, double dwt)
        {
            if (shipType == ShipType.BulkCarrier)
            {
                if (dwt < 10_000) return 0.78;
                if (dwt >= 10_000 && dwt < 25_000) return 0.80;
                if (dwt >= 25_000 && dwt < 55_000) return 0.82;
                if (dwt >= 55_000 && dwt < 75_000) return 0.86;
                return 0.86; // Above 75,000 DWT
            }
            else if (shipType == ShipType.Tanker)
            {
                if (dwt < 10_000) return 0.78;
                if (dwt >= 10_000 && dwt < 25_000) return 0.78;
                if (dwt >= 25_000 && dwt < 55_000) return 0.80;
                if (dwt >= 55_000 && dwt < 75_000) return 0.83;
                return 0.83; // Above 75,000 DWT
            }
            else if (shipType == ShipType.GeneralCargoShip)
            {
                return 0.80; // Single value for all sizes
            }

            throw new ArgumentException($"Unsupported ship type for C_b reference design: {shipType}");
        }
    }
}