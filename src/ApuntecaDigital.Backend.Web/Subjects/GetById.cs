using ApuntecaDigital.Backend.UseCases.Subjects.Get;
using ApuntecaDigital.Backend.Web.Books;
using ApuntecaDigital.Backend.Web.Careers;
using ApuntecaDigital.Backend.Web.Classes;

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
      Response = new SubjectRecord(
        result.Value.Id,
        result.Value.Name,
        new SimpleClassRecord(
          result.Value.Class?.Id ?? 0,
          result.Value.Class?.Name ?? string.Empty,
          result.Value.Class?.Year ?? 0,
          new SimpleCareerRecord(
            result.Value.Class?.Career?.Id ?? 0,
            result.Value.Class?.Career?.Name ?? string.Empty
          )
        ),
        result.Value.Books?.Select(b => new SimpleBookRecord(
          b.Id,
          b.Title,
          b.Author,
          b.Isbn
        )).ToList() ?? []
      );
    }
  }
}
