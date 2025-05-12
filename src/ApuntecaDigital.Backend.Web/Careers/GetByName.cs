using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Careers.Get;
using ApuntecaDigital.Backend.Web.Classes;

namespace ApuntecaDigital.Backend.Web.Careers;

/// <summary>
/// Get Careers by name.
/// </summary>
/// <remarks>
/// Takes a name and returns matching Career record.
/// </remarks>
public class GetByName(IMediator _mediator)
  : Endpoint<GetCareersByNameRequest, CareerListResponse>
{
  public override void Configure()
  {
    Get(GetCareersByNameRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetCareersByNameRequest request,
    CancellationToken cancellationToken)
  {
    Result<IEnumerable<CareerDTO>> result = await _mediator.Send(new GetCareersByNameQuery(request.Name), cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      Response = new CareerListResponse
      {
        Careers = result.Value.Select(c => new CareerRecord(
          c.Id,
          c.Name,
          c.Classes.Select(cl => new SimpleClassRecord(
            cl.Id,
            cl.Name,
            cl.Year,
            new SimpleCareerRecord(
              cl.Career?.Id ?? 0,
              cl.Career?.Name ?? string.Empty
            )
          )).ToList()
        )).ToList()
      };
    }
  }
}
