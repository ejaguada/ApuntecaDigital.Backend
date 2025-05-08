namespace ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;

public class CareerByNameSpec : Specification<Career>
{
  public CareerByNameSpec(string careerName) =>
    Query
        .Where(career => career.Name != null && (career.Name.StartsWith(careerName) || career.Name.EndsWith(careerName) || career.Name.Contains(careerName)));
}
