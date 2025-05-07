using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Careers.List;

namespace ApuntecaDigital.Backend.Infrastructure.Data.Queries;

public class ListCareersQueryService(AppDbContext _db) : IListCareersQueryService
{
  // You can use EF, Dapper, SqlClient, etc. for queries -
  // this is just an example

  public async Task<IEnumerable<CareerDTO>> ListAsync()
  {
    // NOTE: This will fail if testing with EF InMemory provider!
    var result = await _db.Database.SqlQuery<CareerDTO>(
      $"SELECT Id, Name FROM Careers") // don't fetch other big columns
      .ToListAsync();

    return result;
  }
}
