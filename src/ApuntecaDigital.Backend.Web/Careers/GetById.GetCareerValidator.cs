using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Careers;

public class GetCareerValidator : Validator<GetCareerByIdRequest>
{
  public GetCareerValidator()
  {
    RuleFor(x => x.CareerId)
      .GreaterThan(0);
  }
}
