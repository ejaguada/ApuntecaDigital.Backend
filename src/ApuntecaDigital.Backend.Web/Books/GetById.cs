using ApuntecaDigital.Backend.UseCases.Books.Get;
using ApuntecaDigital.Backend.Web.Careers;
using ApuntecaDigital.Backend.Web.Classes;
using ApuntecaDigital.Backend.Web.Subjects;

namespace ApuntecaDigital.Backend.Web.Books;

/// <summary>
/// Get a Book by integer ID.
/// </summary>
/// <remarks>
/// Takes a positive integer ID and returns a matching Book record.
/// </remarks>
public class GetById(IMediator _mediator)
  : Endpoint<GetBookByIdRequest, BookRecord>
{
  public override void Configure()
  {
    Get(GetBookByIdRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetBookByIdRequest request,
    CancellationToken cancellationToken)
  {
    var query = new GetBookQuery(request.BookId);

    var result = await _mediator.Send(query, cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      Response = new BookRecord(
        result.Value.Id,
        result.Value.Title,
        result.Value.Author,
        result.Value.Isbn,
        new SubjectForBookRecord(
          result.Value.Subject?.Id ?? 0,
          result.Value.Subject?.Name ?? string.Empty,
          new ClassForBookRecord(
            result.Value.Subject?.Class?.Id ?? 0,
            result.Value.Subject?.Class?.Name ?? string.Empty,
            result.Value.Subject?.Class?.Year ?? 0,
            new SimpleCareerRecord(
              result.Value.Subject?.Class?.Career?.Id ?? 0,
              result.Value.Subject?.Class?.Career?.Name ?? string.Empty
            )
          )
        ));
    }
  }
}
