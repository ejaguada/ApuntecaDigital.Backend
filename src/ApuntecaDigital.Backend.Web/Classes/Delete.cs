using ApuntecaDigital.Backend.UseCases.Classes.Delete;

namespace ApuntecaDigital.Backend.Web.Classes;

/// <summary>
/// Delete a Class.
/// </summary>
/// <remarks>
/// Delete a Class by providing a valid integer id.
/// </remarks>
public class Delete(IMediator _mediator)
  : Endpoint<DeleteClassRequest>
{
  public override void Configure()
  {
    Delete(DeleteClassRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    DeleteClassRequest request,
    CancellationToken cancellationToken)
  {
    var command = new DeleteClassCommand(request.ClassId);

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
