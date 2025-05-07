using ApuntecaDigital.Backend.UseCases.Books;
using ApuntecaDigital.Backend.UseCases.Books.List;

namespace ApuntecaDigital.Backend.Infrastructure.Data.Queries;

public class ListBooksQueryService(AppDbContext _db) : IListBooksQueryService
{
  // You can use EF, Dapper, SqlClient, etc. for queries -
  // this is just an example

  public async Task<IEnumerable<BookDTO>> ListAsync()
  {
    // NOTE: This will fail if testing with EF InMemory provider!
    var result = await _db.Database.SqlQuery<BookDTO>(
      $"SELECT Id, Title, Author, Isbn FROM Books") // don't fetch other big columns
      .ToListAsync();

    return result;
  }
}
