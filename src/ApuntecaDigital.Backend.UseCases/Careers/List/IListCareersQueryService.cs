namespace ApuntecaDigital.Backend.UseCases.Careers.List;

/// <summary>
/// Represents a service that will actually fetch the necessary data
/// Typically implemented in Infrastructure
/// </summary>
public interface IListCareersQueryService
{
  Task<IEnumerable<CareerDTO>> ListAsync();
}
