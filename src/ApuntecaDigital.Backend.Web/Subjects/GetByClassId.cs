using ApuntecaDigital.Backend.UseCases.Subjects;
using ApuntecaDigital.Backend.Web.Classes;
using ApuntecaDigital.Backend.Web.Careers;
using ApuntecaDigital.Backend.Web.Books;
using ApuntecaDigital.Backend.UseCases.Subjects.Get.ByName;

namespace ApuntecaDigital.Backend.Web.Subjects;

/// <summary>
/// Get Subjects by name.
/// </summary>
/// <remarks>
/// Takes a name and returns matching Subject records.
/// </remarks>
public class GetByClassId(IMediator _mediator)
  : Endpoint<GetSubjectsByClassIdRequest, SubjectListResponse>
{
  public override void Configure()
  {
    Get(GetSubjectsByClassIdRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetSubjectsByClassIdRequest request,
    CancellationToken cancellationToken)
  {
    Result<IEnumerable<SubjectDTO>> result = await _mediator.Send(new GetSubjectsByClassIdQuery(request.Id), cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      Response = new SubjectListResponse
      {
        Subjects = result.Value.Select(s => new SubjectRecord(s.Id, s.Name, new SimpleClassRecord(s.ClassId, s.Class?.Name ?? string.Empty, s.Class?.Year ?? 0, new SimpleCareerRecord(s.Class?.Career?.Id ?? 0, s.Class?.Career?.Name ?? string.Empty)), s.Books?.Select(b => new SimpleBookRecord(b.Id, b.Title, b.Author, b.Isbn)).ToList() ?? new List<SimpleBookRecord>())).ToList()
      };
    }
  }
}
