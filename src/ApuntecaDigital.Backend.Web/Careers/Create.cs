using ApuntecaDigital.Backend.UseCases.Careers.Create;

namespace ApuntecaDigital.Backend.Web.Careers;

/// <summary>
/// Create a new Career
/// </summary>
/// <remarks>
/// Creates a new Career given a name.
/// </remarks>
public class Create(IMediator _mediator)
  : Endpoint<CreateCareerRequest, CreateCareerResponse>
{
  public override void Configure()
  {
    Post(CreateCareerRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new CreateCareerRequest { Name = "Career Name" };
    });
  }

  public override async Task HandleAsync(
    CreateCareerRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new CreateCareerCommand(request.Name!), cancellationToken);

    if (result.IsSuccess)
    {
      Response = new CreateCareerResponse(result.Value, request.Name!);
      return;
    }
    // TODO: Handle other cases as necessary
  }
}
