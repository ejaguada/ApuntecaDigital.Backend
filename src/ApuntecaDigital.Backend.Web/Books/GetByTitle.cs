using ApuntecaDigital.Backend.UseCases.Books;
using ApuntecaDigital.Backend.UseCases.Books.Get.ByTitle;
using ApuntecaDigital.Backend.Web.Books;

namespace ApuntecaDigital.Backend.Web.Classes;

/// <summary>
/// Get Books by title.
/// </summary>
/// <remarks>
/// Takes a title and returns matching Book records.
/// </remarks>
public class GetByTitle(IMediator _mediator)
  : Endpoint<GetBooksByTitleRequest, BookListResponse>
{
  public override void Configure()
  {
    Get(GetBooksByTitleRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetBooksByTitleRequest request,
    CancellationToken cancellationToken)
  {
    Result<IEnumerable<BookDTO>> result = await _mediator.Send(new GetBooksByTitleQuery(request.Title), cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      Response = new BookListResponse
      {
        Books = result.Value.Select(b => new BookRecord(b.Id, b.Title, b.Author, b.Isbn)).ToList()
      };
    }
  }
}
