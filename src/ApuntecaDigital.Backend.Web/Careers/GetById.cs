using ApuntecaDigital.Backend.UseCases.Careers.Get;
using ApuntecaDigital.Backend.Web.Classes;

namespace ApuntecaDigital.Backend.Web.Careers;

/// <summary>
/// Get a Career by integer ID.
/// </summary>
/// <remarks>
/// Takes a positive integer ID and returns a matching Career record.
/// </remarks>
public class GetById(IMediator _mediator)
  : Endpoint<GetCareerByIdRequest, CareerRecord>
{
  public override void Configure()
  {
    Get(GetCareerByIdRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetCareerByIdRequest request,
    CancellationToken cancellationToken)
  {
    var query = new GetCareerQuery(request.CareerId, null);

    var result = await _mediator.Send(query, cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      Response = new CareerRecord(
        result.Value.Id,
        result.Value.Name,
        result.Value.Classes.Select(c => new SimpleClassRecord(
          c.Id,
          c.Name,
          c.Year,
          new SimpleCareerRecord(
            c.Career?.Id ?? 0,
            c.Career?.Name ?? string.Empty
          )
        )).ToList()
      );
    }
  }
}
