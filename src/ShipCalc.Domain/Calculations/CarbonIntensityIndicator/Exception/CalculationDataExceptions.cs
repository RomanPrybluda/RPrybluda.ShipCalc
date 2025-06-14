using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain.Calculations.CarbonIntensityIndicator;

#region Rating Calculator

public class InvalidShipException : Exception
{
    public InvalidShipException()
        : base("Ship cannot be null.") { }
}

public class ShipNotFound : Exception
{
    public ShipNotFound(Guid id)
        : base($"Ship with id:{id} not found.") { }
}

public class ShipNotFoundByIMO : Exception
{
    public ShipNotFoundByIMO(int id)
        : base($"Ship with IMO No.{id} not found.") { }
}

public class CreateCalculationFailed : Exception
{
    public CreateCalculationFailed()
        : base("Creation of calculation is failed.") { }
}

public class NegativeCo2EmissionsException : Exception
{
    public NegativeCo2EmissionsException()
        : base("CO2 emissions cannot be negative.") { }
}

public class NegativeDistanceTravelledException : Exception
{
    public NegativeDistanceTravelledException()
        : base("Distance travelled cannot be negative.") { }
}

public class NegativeYearException : Exception
{
    public NegativeYearException()
        : base("Year cannot be negative.") { }
}

public class CalculationNotFound : Exception
{
    public CalculationNotFound(Guid id) : base($"Calculation with ID {id} not found.") { }
}

public class UpdateCalculationFailed : Exception
{
    public UpdateCalculationFailed() : base("Failed to update CII calculation.") { }
}

#endregion

#region Capacity Calculator

public class InvalidDeadweightException : Exception
{
    public InvalidDeadweightException()
        : base("Deadweight cannot be negative.") { }
}

public class InvalidGrossTonnageException : Exception
{
    public InvalidGrossTonnageException()
        : base("GrossTonnage cannot be negative.") { }
}

public class UnsupportedShipTypeException : Exception
{
    public UnsupportedShipTypeException(ShipType shipType)
        : base($"Unsupported ship type: {shipType}.") { }
}
#endregion

#region Reference Line Calculator

public class InvalidCapacityException : Exception
{
    public InvalidCapacityException(decimal capacity)
            : base($"Capacity must be greater than zero. Actual: {capacity}") { }
}

public class InvalidRefLineParameterAException : Exception
{
    public InvalidRefLineParameterAException(decimal parameterA)
        : base($"RefLine parameter A must be greater than zero. Actual: {parameterA}") { }
}

public class InvalidRefLineParameterCException : Exception
{
    public InvalidRefLineParameterCException(decimal parameterC)
        : base($"RefLine parameter C cannot be negative. Actual: {parameterC}") { }
}
#endregion

#region Attained Carbon Intensity Indicator Calculator

public class ShipNotProvidedException : Exception
{
    public ShipNotProvidedException()
        : base("Ship data must be provided.") { }
}

public class InvalidCO2EmissionsException : Exception
{
    public InvalidCO2EmissionsException(decimal emissions)
        : base($"CO2 emissions cannot be negative. Actual: {emissions}.") { }
}

public class InvalidDistanceTravelledException : Exception
{
    public InvalidDistanceTravelledException(decimal distance)
        : base($"Distance travelled must be greater than zero. Actual: {distance}.") { }
}

public class InvalidIceClassedCapacityCorrFactorException : Exception
{
    public InvalidIceClassedCapacityCorrFactorException(decimal factor)
        : base($"Ice-classed capacity correction factor must be greater than zero. Actual: {factor}.") { }
}

public class InvalidIASuperAndIAIceCorrFactorException : Exception
{
    public InvalidIASuperAndIAIceCorrFactorException(decimal factor)
        : base($"IA Super or IA ice-classed correction factor must be greater than zero. Actual: {factor}.") { }
}
#endregion

#region Required Carbon Intensity Indicator Calculator

public class InvalidYearException : Exception
{
    public InvalidYearException(int year)
        : base($"Year cannot be negative. Received: {year}.") { }
}

public class RefLineParametersNotFoundException : Exception
{
    public RefLineParametersNotFoundException(ShipType shipType, decimal capacity)
        : base($"No reference line parameters found for ship type '{shipType}' and capacity '{capacity}'.") { }
}

public class InvalidRefLineCalculationException : Exception
{
    public InvalidRefLineCalculationException(decimal refLine)
        : base($"Carbon Intensity Indicator Reference Line must be greater than zero. Calculated: {refLine}.") { }
}

public class ReductionFactorNotFoundException : Exception
{
    public ReductionFactorNotFoundException(int year)
        : base($"Reduction factor for year '{year}' is not defined.") { }
}
#endregion

#region IceC lased Ship Capacity Corr Factor Calculator

public class InvalidBlockCoefficientException : Exception
{
    public InvalidBlockCoefficientException(decimal cb)
        : base($"Block coefficient (C_b) must be between 0 and 1. Provided: {cb}") { }
}

public class IceClassCorrectionFactorNotFoundException : Exception
{
    public IceClassCorrectionFactorNotFoundException(IceClass iceClass)
        : base($"No ice strengthening correction factor found for ice class: {iceClass}") { }
}

public class InvalidCalculatedIceClassFactorException : Exception
{
    public InvalidCalculatedIceClassFactorException(IceClass iceClass, decimal calculatedValue)
        : base($"Calculated f_i(ice class) must be greater than zero. Ice class: {iceClass}, Calculated: {calculatedValue}") { }
}

public class ReferenceBlockCoefficientNotFoundException : Exception
{
    public ReferenceBlockCoefficientNotFoundException(ShipType shipType, decimal deadweight)
        : base($"No reference block coefficient found for ship type {shipType} and deadweight {deadweight}") { }
}

#endregion