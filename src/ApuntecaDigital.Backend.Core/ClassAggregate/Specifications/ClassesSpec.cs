namespace ApuntecaDigital.Backend.Core.ClassAggregate.Specifications;

public class ClassesSpec : Specification<Class>
{
  public ClassesSpec() =>
    Query.Include(classObj => classObj.Career)
         .Include(classObj => classObj.Subjects);

}
