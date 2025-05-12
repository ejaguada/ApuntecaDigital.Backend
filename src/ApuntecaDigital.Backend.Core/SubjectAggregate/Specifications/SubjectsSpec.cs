namespace ApuntecaDigital.Backend.Core.SubjectAggregate.Specifications;

public class SubjectsSpec : Specification<Subject>
{
  public SubjectsSpec() =>
    Query
        .Include(subject => subject.Class)
        .Include(subject => subject.Class != null ? subject.Class.Career : null)
        .Include(subject => subject.Books);
}
