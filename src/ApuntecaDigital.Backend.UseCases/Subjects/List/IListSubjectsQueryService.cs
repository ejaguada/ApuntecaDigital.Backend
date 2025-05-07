namespace ApuntecaDigital.Backend.UseCases.Subjects.List;

/// <summary>
/// Represents a service that will actually fetch the necessary data
/// Typically implemented in Infrastructure
/// </summary>
public interface IListSubjectsQueryService
{
  Task<IEnumerable<SubjectDTO>> ListAsync();
}
