using ApuntecaDigital.Backend.Core.CareerAggregate;

namespace ApuntecaDigital.Backend.Core.ContributorAggregate.Specifications;

public class SubjectByIdSpec : Specification<Subject>
{
  public SubjectByIdSpec(int subjectId) =>
    Query
        .Where(subject => subject.Id == subjectId);
}
