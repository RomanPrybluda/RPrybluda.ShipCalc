namespace ShipCalc.Domain;

public class InvalidCalculationYearException : Exception
{
    public InvalidCalculationYearException(int year)
        : base($"Calculation year '{year}' is invalid. It must be greater than 2000 and not in the future.") { }
}

public class MissingShipReferenceException : Exception
{
    public MissingShipReferenceException()
        : base("Carbon Intensity Indicator calculation must be associated with a Ship.") { }
}

public class InvalidCapacityException : Exception
{
    public InvalidCapacityException(double capacity)
        : base($"Capacity '{capacity}' is invalid. It must be a positive number.") { }
}

public class InvalidCorrectionFactorException : Exception
{
    public InvalidCorrectionFactorException(string factorName, double value)
        : base($"Correction factor '{factorName}' has an invalid value: {value}. It must be between 0 and 1.") { }
}

public class InvalidCarbonIntensityIndicatorValueException : Exception
{
    public InvalidCarbonIntensityIndicatorValueException(string indicatorName, double value)
        : base($"'{indicatorName}' value '{value}' is invalid. It must be a non-negative number.") { }
}

public class CarbonIntensityCalculationFailedException : Exception
{
    public CarbonIntensityCalculationFailedException(string reason)
        : base($"Carbon Intensity Indicator calculation failed: {reason}") { }
}
