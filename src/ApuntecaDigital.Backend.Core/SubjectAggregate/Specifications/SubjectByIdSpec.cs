namespace ApuntecaDigital.Backend.Core.SubjectAggregate.Specifications;

public class SubjectByIdSpec : Specification<Subject>
{
  public SubjectByIdSpec(int subjectId) =>
    Query
        .Where(subject => subject.Id == subjectId)
        .Include(subject => subject.Class)
        .Include(subject => subject.Class != null ? subject.Class.Career : null)
        .Include(subject => subject.Books);
}
