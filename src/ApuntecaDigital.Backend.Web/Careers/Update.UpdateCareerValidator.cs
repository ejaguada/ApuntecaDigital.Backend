using ApuntecaDigital.Backend.Infrastructure.Data.Config;
using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Careers;

public class UpdateCareerValidator : Validator<UpdateCareerRequest>
{
  public UpdateCareerValidator()
  {
    RuleFor(x => x.Id)
      .NotEmpty()
      .WithMessage("Id is required.");

    RuleFor(x => x.CareerId)
      .Equal(x => x.Id)
      .WithMessage("CareerId must match Id.");

    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("Name is required.")
      .MinimumLength(2)
      .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
  }
}
