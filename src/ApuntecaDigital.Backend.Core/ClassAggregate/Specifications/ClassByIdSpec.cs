namespace ApuntecaDigital.Backend.Core.ClassAggregate.Specifications;

public class ClassByIdSpec : Specification<Class>
{
  public ClassByIdSpec(int classId) =>
    Query
        .Include(classObj => classObj.Career)
        .Include(classObj => classObj.Subjects)
        .Where(classObj => classObj.Id == classId);


}
