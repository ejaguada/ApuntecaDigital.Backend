namespace ApuntecaDigital.Backend.UseCases.Classes.List;

public class ListClassesHandler(IListClassesQueryService _query)
  : IQueryHandler<ListClassesQuery, Result<IEnumerable<ClassDTO>>>
{
  public async Task<Result<IEnumerable<ClassDTO>>> Handle(ListClassesQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.ListAsync();

    return Result.Success(result);
  }
}
