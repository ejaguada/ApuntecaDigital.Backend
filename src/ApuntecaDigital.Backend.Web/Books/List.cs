using ApuntecaDigital.Backend.UseCases.Books;
using ApuntecaDigital.Backend.UseCases.Books.List;
using ApuntecaDigital.Backend.Web.Careers;
using ApuntecaDigital.Backend.Web.Classes;
using ApuntecaDigital.Backend.Web.Subjects;

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
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    Result<IEnumerable<BookDTO>> result = await _mediator.Send(new ListBooksQuery(null, null), cancellationToken);

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
