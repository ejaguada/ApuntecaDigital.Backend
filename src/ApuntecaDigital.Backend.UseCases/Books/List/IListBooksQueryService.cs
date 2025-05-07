namespace ApuntecaDigital.Backend.UseCases.Books.List;

/// <summary>
/// Represents a service that will actually fetch the necessary data
/// Typically implemented in Infrastructure
/// </summary>
public interface IListBooksQueryService
{
  Task<IEnumerable<BookDTO>> ListAsync();
}
