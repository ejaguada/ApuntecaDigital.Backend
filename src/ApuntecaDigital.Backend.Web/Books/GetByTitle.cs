using ApuntecaDigital.Backend.UseCases.Books;
using ApuntecaDigital.Backend.UseCases.Books.Get.ByTitle;
using ApuntecaDigital.Backend.Web.Books;
using ApuntecaDigital.Backend.Web.Careers;
using ApuntecaDigital.Backend.Web.Subjects;

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
        Books = result.Value.Select(b => new BookRecord(
          b.Id,
          b.Title,
          b.Author,
          b.Isbn,
          new SubjectForBookRecord(
            b.Subject?.Id ?? 0,
            b.Subject?.Name ?? string.Empty,
            new ClassForBookRecord(
              b.Subject?.Class?.Id ?? 0,
              b.Subject?.Class?.Name ?? string.Empty,
              b.Subject?.Class?.Year ?? 0,
              new SimpleCareerRecord(
                b.Subject?.Class?.Career?.Id ?? 0,
                b.Subject?.Class?.Career?.Name ?? string.Empty
              )
            )
          )
        )).ToList()
      };
    }
  }
}
