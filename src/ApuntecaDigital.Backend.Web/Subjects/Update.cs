using ApuntecaDigital.Backend.UseCases.Subjects.Get;
using ApuntecaDigital.Backend.UseCases.Subjects.Update;
using ApuntecaDigital.Backend.Web.Books;
using ApuntecaDigital.Backend.Web.Careers;
using ApuntecaDigital.Backend.Web.Classes;

namespace ApuntecaDigital.Backend.Web.Subjects;

/// <summary>
/// Update an existing Subject.
/// </summary>
/// <remarks>
/// Update an existing Subject by providing a fully defined replacement set of values.
/// </remarks>
public class Update(IMediator _mediator)
  : Endpoint<UpdateSubjectRequest, UpdateSubjectResponse>
{
  public override void Configure()
  {
    Put(UpdateSubjectRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    UpdateSubjectRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new UpdateSubjectCommand(request.Id, request.Name!, request.ClassId), cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    var query = new GetSubjectQuery(request.SubjectId);

    var queryResult = await _mediator.Send(query, cancellationToken);

    if (queryResult.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (queryResult.IsSuccess)
    {
      var dto = queryResult.Value;
      Response = new UpdateSubjectResponse(new SubjectRecord(
        dto.Id,
        dto.Name,
        new SimpleClassRecord(
          dto.Class?.Id ?? 0,
          dto.Class?.Name ?? string.Empty,
          dto.Class?.Year ?? 0,
          new SimpleCareerRecord(
            dto.Class?.Career?.Id ?? 0,
            dto.Class?.Career?.Name ?? string.Empty
          )
        ),
        dto.Books?.Select(b => new SimpleBookRecord(b.Id, b.Title, b.Author, b.Isbn)).ToList() ?? new List<SimpleBookRecord>()
      ));
      return;
    }
  }
}
