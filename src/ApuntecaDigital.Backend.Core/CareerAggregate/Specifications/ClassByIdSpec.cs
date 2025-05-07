using Ardalis.Specification;

namespace ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;

public class ClassByIdSpec : Specification<Class>
{
  public ClassByIdSpec(int classId) =>
    Query
        .Where(classObj => classObj.Id == classId);
}
