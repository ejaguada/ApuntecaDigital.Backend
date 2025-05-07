using ApuntecaDigital.Backend.UseCases.Classes;
using ApuntecaDigital.Backend.UseCases.Classes.List;

namespace ApuntecaDigital.Backend.Web.Classes;

/// <summary>
/// List all Classes
/// </summary>
/// <remarks>
/// List all classes - returns a ClassListResponse containing the Classes.
/// </remarks>
public class List(IMediator _mediator) : EndpointWithoutRequest<ClassListResponse>
{
  public override void Configure()
  {
    Get("/Classes");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    Result<IEnumerable<ClassDTO>> result = await _mediator.Send(new ListClassesQuery(null, null), cancellationToken);

    if (result.IsSuccess)
    {
      Response = new ClassListResponse
      {
        Classes = result.Value.Select(c => new ClassRecord(c.Id, c.Name, c.Year, c.CareerId)).ToList()
      };
    }
  }
}
