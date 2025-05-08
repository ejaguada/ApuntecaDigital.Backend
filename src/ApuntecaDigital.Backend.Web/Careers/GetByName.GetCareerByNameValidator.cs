using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Careers;

public class GetCareerByNameValidator : Validator<GetCareerByNameRequest>
{
  public GetCareerByNameValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty();
  }
}
