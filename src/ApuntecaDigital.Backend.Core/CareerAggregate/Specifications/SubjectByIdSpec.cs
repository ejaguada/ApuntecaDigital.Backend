using Ardalis.Specification;

namespace ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;

public class SubjectByIdSpec : Specification<Subject>
{
  public SubjectByIdSpec(int subjectId) =>
    Query
        .Where(subject => subject.Id == subjectId);
}
