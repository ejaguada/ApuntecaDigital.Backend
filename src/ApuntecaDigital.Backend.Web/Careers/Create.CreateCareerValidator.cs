using ApuntecaDigital.Backend.Infrastructure.Data.Config;
using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Careers;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class CreateCareerValidator : Validator<CreateCareerRequest>
{
  public CreateCareerValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("Name is required.")
      .MinimumLength(2)
      .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
  }
}
