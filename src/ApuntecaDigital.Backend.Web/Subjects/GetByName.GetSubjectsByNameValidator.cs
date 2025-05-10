using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Subjects;

public class GetSubjectsByNameValidator : Validator<GetSubjectsByNameRequest>
{
  public GetSubjectsByNameValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty();
  }
}
