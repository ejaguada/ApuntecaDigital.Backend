using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Classes;

public class GetClassValidator : Validator<GetClassByIdRequest>
{
  public GetClassValidator()
  {
    RuleFor(x => x.ClassId)
      .GreaterThan(0);
  }
}
