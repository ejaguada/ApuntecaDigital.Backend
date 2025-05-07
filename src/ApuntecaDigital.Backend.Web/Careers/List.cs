using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Careers.List;

namespace ApuntecaDigital.Backend.Web.Careers;

/// <summary>
/// List all Careers
/// </summary>
/// <remarks>
/// List all careers - returns a CareerListResponse containing the Careers.
/// </remarks>
public class List(IMediator _mediator) : EndpointWithoutRequest<CareerListResponse>
{
  public override void Configure()
  {
    Get("/Careers");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    Result<IEnumerable<CareerDTO>> result = await _mediator.Send(new ListCareersQuery(null, null), cancellationToken);

    if (result.IsSuccess)
    {
      Response = new CareerListResponse
      {
        Careers = result.Value.Select(c => new CareerRecord(c.Id, c.Name)).ToList()
      };
    }
  }
}
