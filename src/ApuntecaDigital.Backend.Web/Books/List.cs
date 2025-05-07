using ApuntecaDigital.Backend.UseCases.Books;
using ApuntecaDigital.Backend.UseCases.Books.List;

namespace ApuntecaDigital.Backend.Web.Books;

/// <summary>
/// List all Books
/// </summary>
/// <remarks>
/// List all books - returns a BookListResponse containing the Books.
/// </remarks>
public class List(IMediator _mediator) : EndpointWithoutRequest<BookListResponse>
{
  public override void Configure()
  {
    Get("/Books");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    Result<IEnumerable<BookDTO>> result = await _mediator.Send(new ListBooksQuery(null, null), cancellationToken);

    if (result.IsSuccess)
    {
      Response = new BookListResponse
      {
        Books = result.Value.Select(b => new BookRecord(b.Id, b.Title, b.Author, b.Isbn)).ToList()
      };
    }
  }
}
