namespace ShipCalc.Application.Validators;

public class CarbonIntensityIndicatorCalculationValidator
{

    private const int MIN_GROSS_TONNAGE = 5_000;

    public bool ValidateGrossTonnage(double grossTonnage)
    {
        if (grossTonnage >= MIN_GROSS_TONNAGE)
            return true;

        return false;
    }

}

// TODO add this validation, maybe change class name 
