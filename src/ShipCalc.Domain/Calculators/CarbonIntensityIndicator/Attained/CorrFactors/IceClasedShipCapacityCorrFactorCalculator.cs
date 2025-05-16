namespace ShipCalc.Domain
{
    public class IceClasedShipCapacityCorrFactorCalculator
    {
        public double CalculateIceClasedCapacityCorrectionFactor(ShipType shipType, double deadweight, IceClass iceClass, double blockCoefficient)
        {
            if (deadweight <= 0)
                throw new ArgumentException("Deadweigth must be greater than zero.");

            if (blockCoefficient <= 0 || blockCoefficient > 1)
                throw new ArgumentException("Block coefficient (C_b) must be between 0 and 1.");

            if (iceClass == IceClass.NotApplicable)
            {
                return 1.0;
            }

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
                double cbReference = GetBlockCoefficientRefDesign(shipType, deadweight);
                fIcb = cbReference / blockCoefficient;
                fIcb = Math.Max(fIcb, 1.0);
            }
            else
            {
                fIcb = 1.0;
            }

            // Step 3: Calculate final f_i

            return fIceClass * fIcb;
        }

        private double GetBlockCoefficientRefDesign(ShipType shipType, double deadweight)
        {
            double blockCoefficientRefDesign = shipType switch
            {
                ShipType.BulkCarrier => deadweight switch
                {
                    < 10_000 => 0.78,
                    >= 10_000 and < 25_000 => 0.80,
                    >= 25_000 and < 55_000 => 0.82,
                    _ => 0.86
                },

                ShipType.Tanker => deadweight switch
                {
                    < 10_000 => 0.78,
                    >= 10_000 and < 25_000 => 0.78,
                    >= 25_000 and < 55_000 => 0.80,
                    >= 55_000 and < 75_000 => 0.83,
                    _ => 0.83
                },

                ShipType.GeneralCargoShip => 0.80,
                _ => throw new ArgumentException($"Unsupported ship type for C_b reference design: {shipType}")

            };

            return blockCoefficientRefDesign;
        }
    }
}