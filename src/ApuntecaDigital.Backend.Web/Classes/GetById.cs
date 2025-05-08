using ApuntecaDigital.Backend.UseCases.Classes.Get;

namespace ApuntecaDigital.Backend.Web.Classes;

/// <summary>
/// Get a Class by integer ID.
/// </summary>
/// <remarks>
/// Takes a positive integer ID and returns a matching Class record.
/// </remarks>
public class GetById(IMediator _mediator)
  : Endpoint<GetClassByIdRequest, ClassRecord>
{
  public override void Configure()
  {
    Get(GetClassByIdRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetClassByIdRequest request,
    CancellationToken cancellationToken)
  {
    var query = new GetClassQuery(request.ClassId);

    var result = await _mediator.Send(query, cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      Response = new ClassRecord(result.Value.Id, result.Value.Name, result.Value.Year, result.Value.CareerId);
    }
  }
}
