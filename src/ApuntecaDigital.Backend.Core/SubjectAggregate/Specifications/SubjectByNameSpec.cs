namespace ApuntecaDigital.Backend.Core.SubjectAggregate.Specifications;

public class SubjectByNameSpec : Specification<Subject>
{
  public SubjectByNameSpec(string name) =>
    Query
        .Where(subject => subject.Name == name);
}
