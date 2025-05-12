namespace ApuntecaDigital.Backend.Core.SubjectAggregate.Specifications;

public class SubjectByNameSpec : Specification<Subject>
{
  public SubjectByNameSpec(string name) =>
    Query
        .Where(subject => subject.Name != null && (subject.Name.StartsWith(name) || subject.Name.EndsWith(name) || subject.Name.Contains(name)))
        .Include(subject => subject.Books)
        .Include(subject => subject.Class)
        .ThenInclude(@class => @class != null ? @class.Career : null);
}
