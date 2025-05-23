using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain
{
    public class IASuperAndIAIceClassedShipCorrFactorCalculator
    {
        public decimal CalculateIASuperAndIAIceClassedShipCorrFactor(IceClass iceClass)
        {
            if (iceClass == IceClass.IA || iceClass == IceClass.IASuper)
            {
                return 1.05m;
            }

            return 1.0m; // TODO create constants
        }
    }

}