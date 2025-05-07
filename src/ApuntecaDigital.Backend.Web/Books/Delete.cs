using ApuntecaDigital.Backend.UseCases.Books.Delete;

namespace ApuntecaDigital.Backend.Web.Books;

/// <summary>
/// Delete a Book.
/// </summary>
/// <remarks>
/// Delete a Book by providing a valid integer id.
/// </remarks>
public class Delete(IMediator _mediator)
  : Endpoint<DeleteBookRequest>
{
  public override void Configure()
  {
    Delete(DeleteBookRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    DeleteBookRequest request,
    CancellationToken cancellationToken)
  {
    var command = new DeleteBookCommand(request.BookId);

    var result = await _mediator.Send(command, cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      await SendNoContentAsync(cancellationToken);
    };
    // TODO: Handle other issues as needed
  }
}
