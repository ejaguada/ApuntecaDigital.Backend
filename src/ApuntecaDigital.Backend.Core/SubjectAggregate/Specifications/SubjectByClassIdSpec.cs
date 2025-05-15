namespace ApuntecaDigital.Backend.Core.SubjectAggregate.Specifications;

public class SubjectByClassIdSpec : Specification<Subject>
{
  public SubjectByClassIdSpec(int classId) =>
    Query
        .Where(subject => subject.ClassId == classId)
        .Include(subject => subject.Books)
        .Include(subject => subject.Class)
        .ThenInclude(@class => @class != null ? @class.Career : null);
}
