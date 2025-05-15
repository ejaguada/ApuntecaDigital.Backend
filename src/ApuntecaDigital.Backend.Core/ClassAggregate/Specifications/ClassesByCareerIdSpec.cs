namespace ApuntecaDigital.Backend.Core.ClassAggregate.Specifications;

public class ClassesByCareerIdSpec : Specification<Class>
{
    public ClassesByCareerIdSpec(int careerId) =>
      Query
          .Include(classObj => classObj.Career)
          .Include(classObj => classObj.Subjects)
          .Where(classObj => (classObj.Career != null ? classObj.Career.Id : 0) == careerId);
}
