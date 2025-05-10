namespace ApuntecaDigital.Backend.Core.ClassAggregate.Specifications;

public class ClassByNameSpec : Specification<Class>
{
  public ClassByNameSpec(string className) =>
    Query
        .Where(classObj => classObj.Name == className);
}
