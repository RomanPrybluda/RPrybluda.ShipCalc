using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain
{
    public class IceClasedShipCapacityCorrFactorCalculator
    {
        public decimal CalculateIceClasedCapacityCorrectionFactor(
            ShipType shipType,
            decimal deadweight,
            IceClass iceClass,
            decimal blockCoefficient) // TODO create constants or db table for data
        {
            if (deadweight <= 0)
                throw new ArgumentException("Deadweigth must be greater than zero.");

            if (blockCoefficient <= 0 || blockCoefficient > 1)
                throw new ArgumentException("Block coefficient (C_b) must be between 0 and 1.");

            if (iceClass == IceClass.NotApplicable)
            {
                return 1.0m;
            }

            // Step 1: Calculate f_i(ice class) from Table 2

            decimal fIceClass = iceClass switch
            {
                IceClass.IC => 1.0041m + 58.5m / deadweight,
                IceClass.IB => 1.0067m + 62.1m / deadweight,
                IceClass.IA => 1.0099m + 95.1m / deadweight,
                IceClass.IASuper => 1.0151m + 228.7m / deadweight,
                _ => throw new ArgumentException($"Unsupported ice class: {iceClass}")
            };

            // Step 2: Calculate f_icb

            decimal fIcb;
            if (shipType == ShipType.BulkCarrier || shipType == ShipType.Tanker || shipType == ShipType.GeneralCargoShip)
            {
                decimal cbReference = GetBlockCoefficientRefDesign(shipType, deadweight);
                fIcb = cbReference / blockCoefficient;
                fIcb = Math.Max(fIcb, 1.0m);
            }
            else
            {
                fIcb = 1.0m;
            }

            // Step 3: Calculate final f_i

            return fIceClass * fIcb;
        }

        private decimal GetBlockCoefficientRefDesign(
            ShipType shipType,
            decimal deadweight) // TODO create constants or db table for data
        {
            decimal blockCoefficientRefDesign = shipType switch
            {
                ShipType.BulkCarrier => deadweight switch
                {
                    < 10_000 => 0.78m,
                    >= 10_000 and < 25_000 => 0.80m,
                    >= 25_000 and < 55_000 => 0.82m,
                    _ => 0.86m
                },

                ShipType.Tanker => deadweight switch
                {
                    < 10_000 => 0.78m,
                    >= 10_000 and < 25_000 => 0.78m,
                    >= 25_000 and < 55_000 => 0.80m,
                    >= 55_000 and < 75_000 => 0.83m,
                    _ => 0.83m
                },

                ShipType.GeneralCargoShip => 0.80m,
                _ => throw new ArgumentException($"Unsupported ship type for C_b reference design: {shipType}")

            };

            return blockCoefficientRefDesign;
        }
    }
}