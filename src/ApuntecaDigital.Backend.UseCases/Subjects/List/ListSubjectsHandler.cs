namespace ApuntecaDigital.Backend.UseCases.Subjects.List;

public class ListSubjectsHandler(IListSubjectsQueryService _query)
  : IQueryHandler<ListSubjectsQuery, Result<IEnumerable<SubjectDTO>>>
{
  public async Task<Result<IEnumerable<SubjectDTO>>> Handle(ListSubjectsQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.ListAsync();

    return Result.Success(result);
  }
}
