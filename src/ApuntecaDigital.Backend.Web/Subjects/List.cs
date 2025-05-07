using ApuntecaDigital.Backend.UseCases.Subjects;
using ApuntecaDigital.Backend.UseCases.Subjects.List;

namespace ApuntecaDigital.Backend.Web.Subjects;

/// <summary>
/// List all Subjects
/// </summary>
/// <remarks>
/// List all subjects - returns a SubjectListResponse containing the Subjects.
/// </remarks>
public class List(IMediator _mediator) : EndpointWithoutRequest<SubjectListResponse>
{
  public override void Configure()
  {
    Get("/Subjects");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    Result<IEnumerable<SubjectDTO>> result = await _mediator.Send(new ListSubjectsQuery(null, null), cancellationToken);

    if (result.IsSuccess)
    {
      Response = new SubjectListResponse
      {
        Subjects = result.Value.Select(s => new SubjectRecord(s.Id, s.Name, s.ClassId)).ToList()
      };
    }
  }
}
