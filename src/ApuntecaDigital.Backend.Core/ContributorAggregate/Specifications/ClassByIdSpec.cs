using ApuntecaDigital.Backend.Core.CareerAggregate;

namespace ApuntecaDigital.Backend.Core.ContributorAggregate.Specifications;

public class ClassByIdSpec : Specification<Class>
{
  public ClassByIdSpec(int classId) =>
    Query
        .Where(classs => classs.Id == classId);
}
