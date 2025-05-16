namespace ShipCalc.Domain
{
    public class IASuperAndIAIceClassedShipCorrFactorCalculator
    {
        public double CalculateIASuperAndIAIceClassedShipCorrFactor(IceClass iceClass)
        {
            if (iceClass == IceClass.IA || iceClass == IceClass.IASuper)
            {
                return 1.05;
            }

            return 1.0;
        }
    }

}