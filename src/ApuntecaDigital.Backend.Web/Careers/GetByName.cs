using ApuntecaDigital.Backend.UseCases.Careers.Get;

namespace ApuntecaDigital.Backend.Web.Careers;

/// <summary>
/// Get a Career by name.
/// </summary>
/// <remarks>
/// Takes a name and returns a matching Career record.
/// </remarks>
public class GetByName(IMediator _mediator)
  : Endpoint<GetCareerByNameRequest, CareerRecord>
{
  public override void Configure()
  {
    Get(GetCareerByNameRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetCareerByNameRequest request,
    CancellationToken cancellationToken)
  {
    var query = new GetCareerQuery(null, request.Name);

    var result = await _mediator.Send(query, cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      Response = new CareerRecord(result.Value.Id, result.Value.Name);
    }
  }
}
