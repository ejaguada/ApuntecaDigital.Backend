using ApuntecaDigital.Backend.Core.CareerAggregate;

namespace ApuntecaDigital.Backend.Core.ContributorAggregate.Specifications;

public class CareerByIdSpec : Specification<Career>
{
  public CareerByIdSpec(int careerId) =>
    Query
        .Where(career => career.Id == careerId);
}
