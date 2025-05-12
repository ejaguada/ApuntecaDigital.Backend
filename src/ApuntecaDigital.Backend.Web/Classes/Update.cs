using ApuntecaDigital.Backend.UseCases.Classes.Get;
using ApuntecaDigital.Backend.UseCases.Classes.Update;
using ApuntecaDigital.Backend.Web.Careers;
using ApuntecaDigital.Backend.Web.Subjects;

namespace ApuntecaDigital.Backend.Web.Classes;

/// <summary>
/// Update an existing Class.
/// </summary>
/// <remarks>
/// Update an existing Class by providing a fully defined replacement set of values.
/// </remarks>
public class Update(IMediator _mediator)
  : Endpoint<UpdateClassRequest, UpdateClassResponse>
{
  public override void Configure()
  {
    Put(UpdateClassRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    UpdateClassRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new UpdateClassCommand(request.Id, request.Name!, request.Year, request.CareerId), cancellationToken);

    if (result.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    var query = new GetClassQuery(request.ClassId);

    var queryResult = await _mediator.Send(query, cancellationToken);

    if (queryResult.Status == ResultStatus.NotFound)
    {
      await SendNotFoundAsync(cancellationToken);
      return;
    }

    if (queryResult.IsSuccess)
    {
      var dto = queryResult.Value;
      Response = new UpdateClassResponse(new ClassRecord(
        dto.Id,
        dto.Name,
        dto.Year,
        new SimpleCareerRecord(
          dto.Career?.Id ?? 0,
          dto.Career?.Name ?? string.Empty
        ),
        dto.Subjects?.Select(s => new SimpleSubjectRecord(
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
      ));
      return;
    }
  }
}
