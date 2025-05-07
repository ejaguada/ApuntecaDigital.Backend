using ApuntecaDigital.Backend.Infrastructure.Data.Config;
using FastEndpoints;
using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Subjects;

public class UpdateSubjectValidator : Validator<UpdateSubjectRequest>
{
  public UpdateSubjectValidator()
  {
    RuleFor(x => x.Id)
      .NotEmpty()
      .WithMessage("Id is required.");

    RuleFor(x => x.SubjectId)
      .Equal(x => x.Id)
      .WithMessage("SubjectId must match Id.");

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
