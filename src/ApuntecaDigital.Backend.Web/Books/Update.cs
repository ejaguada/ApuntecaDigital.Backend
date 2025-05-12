using ApuntecaDigital.Backend.UseCases.Books.Get;
using ApuntecaDigital.Backend.UseCases.Books.Update;
using ApuntecaDigital.Backend.Web.Careers;
using ApuntecaDigital.Backend.Web.Classes;
using ApuntecaDigital.Backend.Web.Subjects;

namespace ApuntecaDigital.Backend.Web.Books;

/// <summary>
/// Update an existing Book.
/// </summary>
/// <remarks>
/// Update an existing Book by providing a fully defined replacement set of values.
/// </remarks>
public class Update(IMediator _mediator)
  : Endpoint<UpdateBookRequest, UpdateBookResponse>
{
  public override void Configure()
  {
    Put(UpdateBookRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    UpdateBookRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new UpdateBookCommand(request.Id, request.Title!, request.Author!, request.Isbn!, request.SubjectId!), cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    var query = new GetBookQuery(request.BookId);

    var queryResult = await _mediator.Send(query, cancellationToken);

    if (queryResult.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (queryResult.IsSuccess)
    {
      var dto = queryResult.Value;
      Response = new UpdateBookResponse(new BookRecord(
        dto.Id,
        dto.Title,
        dto.Author,
        dto.Isbn,
        new SubjectForBookRecord(
          dto.Subject?.Id ?? 0,
          dto.Subject?.Name ?? string.Empty,
          new ClassForBookRecord(
            dto.Subject?.Class?.Id ?? 0,
            dto.Subject?.Class?.Name ?? string.Empty,
            dto.Subject?.Class?.Year ?? 0,
            new SimpleCareerRecord(
              dto.Subject?.Class?.Career?.Id ?? 0,
              dto.Subject?.Class?.Career?.Name ?? string.Empty
            )
          )
        )
      ));
      return;
    }
  }
}
