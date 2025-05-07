namespace ApuntecaDigital.Backend.UseCases.Books.List;

public class ListBooksHandler(IListBooksQueryService _query)
  : IQueryHandler<ListBooksQuery, Result<IEnumerable<BookDTO>>>
{
  public async Task<Result<IEnumerable<BookDTO>>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.ListAsync();

    return Result.Success(result);
  }
}
