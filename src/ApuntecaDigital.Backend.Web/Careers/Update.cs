using ApuntecaDigital.Backend.UseCases.Careers.Get;
using ApuntecaDigital.Backend.UseCases.Careers.Update;
using ApuntecaDigital.Backend.Web.Classes;

namespace ApuntecaDigital.Backend.Web.Careers;

/// <summary>
/// Update an existing Career.
/// </summary>
/// <remarks>
/// Update an existing Career by providing a fully defined replacement set of values.
/// </remarks>
public class Update(IMediator _mediator)
  : Endpoint<UpdateCareerRequest, UpdateCareerResponse>
{
  public override void Configure()
  {
    Put(UpdateCareerRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    UpdateCareerRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new UpdateCareerCommand(request.Id, request.Name!), cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    var query = new GetCareerQuery(request.CareerId, null);

    var queryResult = await _mediator.Send(query, cancellationToken);

    if (queryResult.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (queryResult.IsSuccess)
    {
      var dto = queryResult.Value;
      Response = new UpdateCareerResponse(new CareerRecord(
        dto.Id,
        dto.Name,
        dto.Classes.Select(cl => new SimpleClassRecord(
          cl.Id,
          cl.Name,
          cl.Year,
          new SimpleCareerRecord(
            cl.Career?.Id ?? 0,
            cl.Career?.Name ?? string.Empty
          )
        )).ToList()
      ));
      return;
    }
  }
}
