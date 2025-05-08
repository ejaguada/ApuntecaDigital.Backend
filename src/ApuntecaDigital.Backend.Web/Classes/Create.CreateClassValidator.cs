using ApuntecaDigital.Backend.Infrastructure.Data.Config;
using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Classes;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class CreateClassValidator : Validator<CreateClassRequest>
{
  public CreateClassValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("Name is required.")
      .MinimumLength(2)
      .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
      
    RuleFor(x => x.Year)
      .NotEmpty()
      .WithMessage("Year is required.")
      .GreaterThan(0)
      .WithMessage("Year must be greater than 0.");
      
    RuleFor(x => x.CareerId)
      .NotEmpty()
      .WithMessage("CareerId is required.")
      .GreaterThan(0)
      .WithMessage("CareerId must be greater than 0.");
  }
}
