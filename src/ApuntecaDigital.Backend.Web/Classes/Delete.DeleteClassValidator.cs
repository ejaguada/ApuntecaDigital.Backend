using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Classes;

public class DeleteClassValidator : Validator<DeleteClassRequest>
{
  public DeleteClassValidator()
  {
    RuleFor(x => x.ClassId)
      .GreaterThan(0);
  }
}
