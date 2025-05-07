using Ardalis.Specification;

namespace ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;

public class CareerByIdSpec : Specification<Career>
{
  public CareerByIdSpec(int careerId) =>
    Query
        .Where(career => career.Id == careerId);
}
