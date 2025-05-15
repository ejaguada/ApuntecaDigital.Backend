using ApuntecaDigital.Backend.Core.ClassAggregate;
using ApuntecaDigital.Backend.Core.ClassAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Classes.Get.ByCareerId;
using ApuntecaDigital.Backend.UseCases.Subjects;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Get;

public class GetClassesByCareerIdHandler : IRequestHandler<GetClassesByCareerIdQuery, Result<IEnumerable<ClassDTO>>>
{
  private readonly IRepository<Class> _repository;

  public GetClassesByCareerIdHandler(IRepository<Class> repository)
  {
    _repository = repository;
  }

  public async Task<Result<IEnumerable<ClassDTO>>> Handle(GetClassesByCareerIdQuery request, CancellationToken cancellationToken)
  {
    var spec = new ClassesByCareerIdSpec(request.CareerId);
    var classObj = await _repository.ListAsync(spec, cancellationToken);

    if (classObj == null)
    {
      return Result<IEnumerable<ClassDTO>>.NotFound();
    }

    return Result<IEnumerable<ClassDTO>>.Success(classObj.Select(
        c => new ClassDTO(c.Id, c.Name, c.Year, c.CareerId, new SimpleCareerDTO(c.CareerId, c.Career?.Name ?? string.Empty), c.Subjects.Select(s => new SimpleSubjectDTO(s.Id, s.Name, c.Id, new SimpleClassDTO(c.Id, c.Name, c.Year, new SimpleCareerDTO(c.Career?.Id ?? 0, c.Career?.Name ?? string.Empty)))).ToList())).ToList(
      ));
  }
}
