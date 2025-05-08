using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Careers;

public class DeleteCareerValidator : Validator<DeleteCareerRequest>
{
  public DeleteCareerValidator()
  {
    RuleFor(x => x.CareerId)
      .GreaterThan(0);
  }
}
