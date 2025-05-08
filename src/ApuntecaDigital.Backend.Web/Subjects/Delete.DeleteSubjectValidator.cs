using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Subjects;

public class DeleteSubjectValidator : Validator<DeleteSubjectRequest>
{
  public DeleteSubjectValidator()
  {
    RuleFor(x => x.SubjectId)
      .GreaterThan(0);
  }
}
