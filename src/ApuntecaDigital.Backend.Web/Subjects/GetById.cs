using ApuntecaDigital.Backend.UseCases.Subjects;
using ApuntecaDigital.Backend.UseCases.Subjects.Get;

namespace ApuntecaDigital.Backend.Web.Subjects;

/// <summary>
/// Get a Subject by integer ID.
/// </summary>
/// <remarks>
/// Takes a positive integer ID and returns a matching Subject record.
/// </remarks>
public class GetById(IMediator _mediator)
  : Endpoint<GetSubjectByIdRequest, SubjectRecord>
{
  public override void Configure()
  {
    Get(GetSubjectByIdRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetSubjectByIdRequest request,
    CancellationToken cancellationToken)
  {
    var query = new GetSubjectQuery(request.SubjectId);

    var result = await _mediator.Send(query, cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (result.IsSuccess)
    {
      Response = new SubjectRecord(result.Value.Id, result.Value.Name, result.Value.ClassId);
    }
  }
}
