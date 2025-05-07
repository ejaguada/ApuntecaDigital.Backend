using ApuntecaDigital.Backend.UseCases.Subjects.Create;

namespace ApuntecaDigital.Backend.Web.Subjects;

/// <summary>
/// Create a new Subject
/// </summary>
/// <remarks>
/// Creates a new Subject given a name and class ID.
/// </remarks>
public class Create(IMediator _mediator)
  : Endpoint<CreateSubjectRequest, CreateSubjectResponse>
{
  public override void Configure()
  {
    Post(CreateSubjectRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new CreateSubjectRequest { Name = "Subject Name", ClassId = 1 };
    });
  }

  public override async Task HandleAsync(
    CreateSubjectRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new CreateSubjectCommand(request.Name!, request.ClassId), cancellationToken);

    if (result.IsSuccess)
    {
      Response = new CreateSubjectResponse(result.Value, request.Name!, request.ClassId);
      return;
    }
    // TODO: Handle other cases as necessary
  }
}
