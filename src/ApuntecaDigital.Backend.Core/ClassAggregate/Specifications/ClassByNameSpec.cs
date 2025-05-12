namespace ApuntecaDigital.Backend.Core.ClassAggregate.Specifications;

public class ClassByNameSpec : Specification<Class>
{
  public ClassByNameSpec(string className) =>
    Query
        .Where(classObj => classObj.Name != null && (classObj.Name.StartsWith(className) || classObj.Name.EndsWith(className) || classObj.Name.Contains(className)))
        .Include(classObj => classObj.Career)
        .Include(classObj => classObj.Subjects);

}
