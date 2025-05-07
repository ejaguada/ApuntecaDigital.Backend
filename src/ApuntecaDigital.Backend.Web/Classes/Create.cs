using ApuntecaDigital.Backend.UseCases.Classes.Create;

namespace ApuntecaDigital.Backend.Web.Classes;

/// <summary>
/// Create a new Class
/// </summary>
/// <remarks>
/// Creates a new Class given a name, year, and career ID.
/// </remarks>
public class Create(IMediator _mediator)
  : Endpoint<CreateClassRequest, CreateClassResponse>
{
  public override void Configure()
  {
    Post(CreateClassRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new CreateClassRequest { Name = "Class Name", Year = 2025, CareerId = 1 };
    });
  }

  public override async Task HandleAsync(
    CreateClassRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new CreateClassCommand(request.Name!, request.Year, request.CareerId), cancellationToken);

    if (result.IsSuccess)
    {
      Response = new CreateClassResponse(result.Value, request.Name!, request.Year, request.CareerId);
      return;
    }
    // TODO: Handle other cases as necessary
  }
}
