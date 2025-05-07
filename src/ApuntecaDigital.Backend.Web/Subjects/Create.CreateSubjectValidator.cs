using ApuntecaDigital.Backend.Infrastructure.Data.Config;
using FastEndpoints;
using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Subjects;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class CreateSubjectValidator : Validator<CreateSubjectRequest>
{
  public CreateSubjectValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("Name is required.")
      .MinimumLength(2)
      .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
      
    RuleFor(x => x.ClassId)
      .NotEmpty()
      .WithMessage("ClassId is required.")
      .GreaterThan(0)
      .WithMessage("ClassId must be greater than 0.");
  }
}
