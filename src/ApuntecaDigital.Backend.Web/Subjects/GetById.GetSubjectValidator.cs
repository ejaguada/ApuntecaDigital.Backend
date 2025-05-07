using FastEndpoints;
using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Subjects;

public class GetSubjectValidator : Validator<GetSubjectByIdRequest>
{
  public GetSubjectValidator()
  {
    RuleFor(x => x.SubjectId)
      .GreaterThan(0);
  }
}
