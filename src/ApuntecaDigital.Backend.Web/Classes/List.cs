using ApuntecaDigital.Backend.UseCases.Classes;
using ApuntecaDigital.Backend.UseCases.Classes.List;
using ApuntecaDigital.Backend.Web.Careers;
using ApuntecaDigital.Backend.Web.Subjects;

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
        Classes = result.Value.Select(c => new ClassRecord(c.Id, c.Name, c.Year, new SimpleCareerRecord(c.Career?.Id ?? 0, c.Career?.Name ?? string.Empty), c.Subjects?.Select(s => new SimpleSubjectRecord(
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
        )).ToList() ?? new List<SimpleSubjectRecord>())).ToList()
      };
    }
  }
}
