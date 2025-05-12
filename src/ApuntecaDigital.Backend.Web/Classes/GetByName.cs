using ApuntecaDigital.Backend.Core.ClassAggregate;
using ApuntecaDigital.Backend.UseCases.Classes;
using ApuntecaDigital.Backend.UseCases.Classes.Get;
using ApuntecaDigital.Backend.Web.Careers;
using ApuntecaDigital.Backend.Web.Subjects;

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
          Classes = result.Value.Select(c => new ClassRecord(
            c.Id,
            c.Name,
            c.Year,
            new SimpleCareerRecord(
              c.Career?.Id ?? 0,
              c.Career?.Name ?? string.Empty
            ),
            c.Subjects?.Select(s => new SimpleSubjectRecord(
              s.Id,
              s.Name,
              new SimpleClassRecord(
                s.Class?.Id ?? 0,
                s.Class?.Name ?? string.Empty,
                s.Class?.Year ?? 0,
                new SimpleCareerRecord(
                  s.Class?.Career?.Id ?? 0,
                  s.Class?.Career?.Name ?? string.Empty
                )
              )
            )).ToList() ?? new List<SimpleSubjectRecord>()
          )).ToList()
      };
    }
  }
}
