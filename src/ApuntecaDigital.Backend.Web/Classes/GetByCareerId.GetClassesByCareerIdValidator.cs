using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Classes;

public class GetClassesByCareerIdValidator : Validator<GetClassesByCareerIdRequest>
{
  public GetClassesByCareerIdValidator()
  {
    RuleFor(x => x.CareerId)
      .NotEmpty();
  }
}
