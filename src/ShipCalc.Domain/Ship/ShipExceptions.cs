using ShipCalc.Domain.Enums;

namespace ShipCalc.Domain
{

    public class InvalidImoNumberException : Exception
    {
        public InvalidImoNumberException(int imo)
            : base($"IMO number '{imo}' is invalid. It must be a 7-digit number.") { }
    }

    public class EmptyShipNameException : Exception
    {
        public EmptyShipNameException()
            : base("Ship name must not be empty.") { }
    }

    public class InvalidGrossTonnageException : Exception
    {
        public InvalidGrossTonnageException(double value)
            : base($"Gross tonnage '{value}' is invalid. It must be a positive number.") { }
    }

    public class InvalidDeadweightException : Exception
    {
        public InvalidDeadweightException(double value)
            : base($"Deadweight '{value}' is invalid. It must be a positive number.") { }
    }

    public class InvalidBlockCoefficientException : Exception
    {
        public InvalidBlockCoefficientException(double value)
            : base($"Block coefficient '{value}' is out of range. It must be between 0 and 1.") { }
    }

    public class UnsupportedShipTypeException : Exception
    {
        public UnsupportedShipTypeException(ShipType shipType)
            : base($"Ship type '{shipType}' is not supported.") { }
    }

    public class IceClassNotApplicableException : Exception
    {
        public IceClassNotApplicableException()
            : base("Ice class is not applicable for this ship.") { }
    }
}
