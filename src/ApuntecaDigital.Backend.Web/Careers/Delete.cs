using ApuntecaDigital.Backend.UseCases.Careers.Delete;

namespace ApuntecaDigital.Backend.Web.Careers;

/// <summary>
/// Delete a Career.
/// </summary>
/// <remarks>
/// Delete a Career by providing a valid integer id.
/// </remarks>
public class Delete(IMediator _mediator)
  : Endpoint<DeleteCareerRequest>
{
  public override void Configure()
  {
    Delete(DeleteCareerRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    DeleteCareerRequest request,
    CancellationToken cancellationToken)
  {
    var command = new DeleteCareerCommand(request.CareerId);

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
