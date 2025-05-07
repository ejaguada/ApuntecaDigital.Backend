using ApuntecaDigital.Backend.UseCases.Subjects.Delete;

namespace ApuntecaDigital.Backend.Web.Subjects;

/// <summary>
/// Delete a Subject.
/// </summary>
/// <remarks>
/// Delete a Subject by providing a valid integer id.
/// </remarks>
public class Delete(IMediator _mediator)
  : Endpoint<DeleteSubjectRequest>
{
  public override void Configure()
  {
    Delete(DeleteSubjectRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    DeleteSubjectRequest request,
    CancellationToken cancellationToken)
  {
    var command = new DeleteSubjectCommand(request.SubjectId);

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
