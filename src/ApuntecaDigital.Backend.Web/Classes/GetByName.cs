using ApuntecaDigital.Backend.Core.ClassAggregate;
using ApuntecaDigital.Backend.UseCases.Classes;
using ApuntecaDigital.Backend.UseCases.Classes.Get;

namespace ApuntecaDigital.Backend.Web.Classes;

/// <summary>
/// Get Classes by name.
/// </summary>
/// <remarks>
/// Takes a name and returns matching Class record.
/// </remarks>
public class GetByName(IMediator _mediator)
  : Endpoint<GetClassesByNameRequest, ClassListResponse>
{
  public override void Configure()
  {
    Get(GetClassesByNameRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetClassesByNameRequest request,
    CancellationToken cancellationToken)
  {
    Result<IEnumerable<ClassDTO>> result = await _mediator.Send(new GetClassesByNameQuery(request.Name), cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      Response = new ClassListResponse
      {
        Classes = result.Value.Select(c => new ClassRecord(c.Id, c.Name, c.Year, c.CareerId)).ToList()
      };
    }
  }
}
