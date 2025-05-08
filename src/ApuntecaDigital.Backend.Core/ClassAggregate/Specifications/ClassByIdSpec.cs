namespace ApuntecaDigital.Backend.Core.ClassAggregate.Specifications;

public class ClassByIdSpec : Specification<Class>
{
  public ClassByIdSpec(int classId) =>
    Query
        .Where(classObj => classObj.Id == classId);
}
