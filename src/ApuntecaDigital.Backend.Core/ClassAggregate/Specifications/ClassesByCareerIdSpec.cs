namespace ApuntecaDigital.Backend.Core.ClassAggregate.Specifications;

public class ClassesByCareerIdSpec : Specification<Class>
{
  public ClassesByCareerIdSpec(int careerId) =>
    Query
        .Where(classObj => classObj.CareerId == careerId)
                .Include(classObj => classObj.Career)
                .Include(classObj => classObj.Subjects);

}
