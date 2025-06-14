using FluentValidation;

namespace ShipCalc.Application.Calculation.CarbonIntensityIndicator;

public class UpdateCalcnCommandValidator : AbstractValidator<UpdateCalcnCommand>
{
    public UpdateCalcnCommandValidator()
    {

        RuleFor(x => x.ImoNumber)
            .NotEmpty().WithMessage("IMO number is required.")
            .InclusiveBetween(1000000, 9999999).WithMessage("IMO number must be a 7-digit number.");

        RuleFor(x => x.ShipName)
            .NotEmpty().WithMessage("Ship name is required.")
            .MaximumLength(100).WithMessage("Ship name cannot exceed 100 characters.");

        RuleFor(x => x.GrossTonnage)
            .GreaterThan(0).WithMessage("Gross tonnage must be greater than zero.")
            .Must(HaveValidPrecisionAndScale).WithMessage("Gross tonnage must have up to 7 digits before and 3 digits after the decimal point.");

        RuleFor(x => x.SummerDeadweight)
            .GreaterThanOrEqualTo(0).WithMessage("Summer deadweight cannot be negative.")
            .Must(HaveValidPrecisionAndScale).WithMessage("Summer deadweight must have up to 7 digits before and 3 digits after the decimal point.");

        RuleFor(x => x.BlockCoefficient)
            .InclusiveBetween(0.1m, 1.0m).WithMessage("Block coefficient must be between 0.1 and 1.0.")
            .Must(HaveValidPrecisionAndScale).WithMessage("Block coefficient must have up to 7 digits before and 3 digits after the decimal point.");

        RuleFor(x => x.CargoCompartmentCubicCapacity)
            .GreaterThanOrEqualTo(0).WithMessage("Cargo compartment cubic capacity cannot be negative.")
            .Must(HaveValidPrecisionAndScale).WithMessage("Cargo compartment cubic capacity must have up to 7 digits before and 3 digits after the decimal point.");

        RuleFor(x => x.ShipType)
            .IsInEnum().WithMessage("Invalid ship type.");

        RuleFor(x => x.IceClass)
            .IsInEnum().WithMessage("Invalid ice class.");

        RuleFor(x => x.Co2EmissionsInTons)
            .GreaterThanOrEqualTo(0).WithMessage("CO2 emissions cannot be negative.")
            .Must(HaveValidPrecisionAndScale).WithMessage("CO2 emissions must have up to 7 digits before and 3 digits after the decimal point.");

        RuleFor(x => x.DistanceTravelledInNMs)
            .GreaterThan(0).WithMessage("Distance travelled must be greater than zero.")
            .Must(HaveValidPrecisionAndScale).WithMessage("Distance travelled must have up to 7 digits before and 3 digits after the decimal point.");

        RuleFor(x => x.Year)
            .InclusiveBetween(2000, DateTime.UtcNow.Year)
            .WithMessage($"Year must be between 2000 and {DateTime.UtcNow.Year}.");
    }

    private bool HaveValidPrecisionAndScale(decimal value)
    {
        string[] parts = value.ToString(System.Globalization.CultureInfo.InvariantCulture).Split('.');
        string integerPart = parts[0].TrimStart('-');
        string decimalPart = parts.Length > 1 ? parts[1] : "";

        int integerDigits = integerPart.Length;
        int decimalDigits = decimalPart.Length;

        return integerDigits <= 7 && decimalDigits <= 3;
    }
}
