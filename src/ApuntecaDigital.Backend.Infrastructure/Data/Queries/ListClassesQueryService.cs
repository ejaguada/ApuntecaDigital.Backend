using ApuntecaDigital.Backend.UseCases.Classes;
using ApuntecaDigital.Backend.UseCases.Classes.List;

namespace ApuntecaDigital.Backend.Infrastructure.Data.Queries;

public class ListClassesQueryService(AppDbContext _db) : IListClassesQueryService
{
  // You can use EF, Dapper, SqlClient, etc. for queries -
  // this is just an example

  public async Task<IEnumerable<ClassDTO>> ListAsync()
  {
    // NOTE: This will fail if testing with EF InMemory provider!
    var result = await _db.Database.SqlQuery<ClassDTO>(
      $"SELECT Id, Name, Year, CareerId FROM Classes") // don't fetch other big columns
      .ToListAsync();

    return result;
  }
}
