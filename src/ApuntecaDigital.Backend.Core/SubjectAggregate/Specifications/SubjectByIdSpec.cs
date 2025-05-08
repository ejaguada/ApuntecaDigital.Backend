namespace ApuntecaDigital.Backend.Core.SubjectAggregate.Specifications;

public class SubjectByIdSpec : Specification<Subject>
{
  public SubjectByIdSpec(int subjectId) =>
    Query
        .Where(subject => subject.Id == subjectId);
}
