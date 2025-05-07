using ApuntecaDigital.Backend.UseCases.Subjects;
using ApuntecaDigital.Backend.UseCases.Subjects.List;

namespace ApuntecaDigital.Backend.Infrastructure.Data.Queries;

public class ListSubjectsQueryService(AppDbContext _db) : IListSubjectsQueryService
{
  // You can use EF, Dapper, SqlClient, etc. for queries -
  // this is just an example

  public async Task<IEnumerable<SubjectDTO>> ListAsync()
  {
    // NOTE: This will fail if testing with EF InMemory provider!
    var result = await _db.Database.SqlQuery<SubjectDTO>(
      $"SELECT Id, Name, ClassId FROM Subjects") // don't fetch other big columns
      .ToListAsync();

    return result;
  }
}
