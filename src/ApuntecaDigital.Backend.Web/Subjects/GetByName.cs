using ApuntecaDigital.Backend.UseCases.Subjects;
using ApuntecaDigital.Backend.UseCases.Subjects.Get.ByName;

namespace ApuntecaDigital.Backend.Web.Subjects;

/// <summary>
/// Get Subjects by name.
/// </summary>
/// <remarks>
/// Takes a name and returns matching Subject records.
/// </remarks>
public class GetSubjectsByName(IMediator _mediator)
  : Endpoint<GetSubjectsByNameRequest, SubjectListResponse>
{
  public override void Configure()
  {
    Get(GetSubjectsByNameRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetSubjectsByNameRequest request,
    CancellationToken cancellationToken)
  {
    Result<IEnumerable<SubjectDTO>> result = await _mediator.Send(new GetSubjectsByNameQuery(request.Name), cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      Response = new SubjectListResponse
      {
        Subjects = result.Value.Select(s => new SubjectRecord(s.Id, s.Name, s.ClassId)).ToList()
      };
    }
  }
}
