using ApuntecaDigital.Backend.UseCases.Subjects;
using ApuntecaDigital.Backend.UseCases.Subjects.List;
using ApuntecaDigital.Backend.Web.Books;
using ApuntecaDigital.Backend.Web.Careers;
using ApuntecaDigital.Backend.Web.Classes;

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
        Subjects = result.Value.Select(x => new SubjectRecord(x.Id, x.Name, new SimpleClassRecord(x.Class?.Id ?? 0, x.Class?.Name ?? string.Empty, x.Class?.Year ?? 0, new SimpleCareerRecord(x.Class?.Career?.Id ?? 0, x.Class?.Career?.Name ?? string.Empty)), x.Books?.Select(b => new SimpleBookRecord(b.Id, b.Title, b.Author, b.Isbn)).ToList() ?? new List<SimpleBookRecord>())).ToList()
      };
    }
  }
}
