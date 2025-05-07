namespace ApuntecaDigital.Backend.UseCases.Classes.List;

/// <summary>
/// Represents a service that will actually fetch the necessary data
/// Typically implemented in Infrastructure
/// </summary>
public interface IListClassesQueryService
{
  Task<IEnumerable<ClassDTO>> ListAsync();
}
