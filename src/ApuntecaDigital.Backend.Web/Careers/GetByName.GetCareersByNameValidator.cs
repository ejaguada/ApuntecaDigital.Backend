using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Careers;

public class GetCareersByNameValidator : Validator<GetCareersByNameRequest>
{
  public GetCareersByNameValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty();
  }
}
