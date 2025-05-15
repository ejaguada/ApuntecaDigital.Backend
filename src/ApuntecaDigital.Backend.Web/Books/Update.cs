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
      Response = new UpdateBookResponse(new UpdateBookRecord(
        dto.Id,
        dto.Title,
        dto.Author,
        dto.Isbn,
        dto.SubjectId
      ));
      return;
    }
  }
}
