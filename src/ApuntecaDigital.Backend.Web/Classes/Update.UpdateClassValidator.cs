using ApuntecaDigital.Backend.Infrastructure.Data.Config;
using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Classes;

public class UpdateClassValidator : Validator<UpdateClassRequest>
{
  public UpdateClassValidator()
  {
    RuleFor(x => x.Id)
      .NotEmpty()
      .WithMessage("Id is required.");

    RuleFor(x => x.ClassId)
      .Equal(x => x.Id)
      .WithMessage("ClassId must match Id.");

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
