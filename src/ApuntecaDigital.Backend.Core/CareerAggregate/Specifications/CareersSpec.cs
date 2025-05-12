namespace ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;

public class CareersSpec : Specification<Career>
{
  public CareersSpec() =>
    Query.Include(career => career.Classes);
}
