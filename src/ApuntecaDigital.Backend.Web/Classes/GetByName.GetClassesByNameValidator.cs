using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Classes;

public class GetClassesByNameValidator : Validator<GetClassesByNameRequest>
{
  public GetClassesByNameValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty();
  }
}
