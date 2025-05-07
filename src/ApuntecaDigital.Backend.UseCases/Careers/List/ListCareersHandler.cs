namespace ApuntecaDigital.Backend.UseCases.Careers.List;

public class ListCareersHandler(IListCareersQueryService _query)
  : IQueryHandler<ListCareersQuery, Result<IEnumerable<CareerDTO>>>
{
  public async Task<Result<IEnumerable<CareerDTO>>> Handle(ListCareersQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.ListAsync();

    return Result.Success(result);
  }
}
