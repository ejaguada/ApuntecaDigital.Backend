using ApuntecaDigital.Backend.UseCases.Subjects;
using ApuntecaDigital.Backend.Core.ClassAggregate;
using ApuntecaDigital.Backend.Core.ClassAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Careers;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Get;

public class GetClassesByNameHandler : IRequestHandler<GetClassesByNameQuery, Result<IEnumerable<ClassDTO>>>
{
  private readonly IRepository<Class> _repository;

  public GetClassesByNameHandler(IRepository<Class> repository)
  {
    _repository = repository;
  }

  public async Task<Result<IEnumerable<ClassDTO>>> Handle(GetClassesByNameQuery request, CancellationToken cancellationToken)
  {
    var classes = new List<Class>();

    if (!string.IsNullOrEmpty(request.ClassName))
    {
      var spec = new ClassByNameSpec(request.ClassName);
      classes = await _repository.ListAsync(spec, cancellationToken);
    }


    if (classes == null || classes.Count == 0)
    {
      return Result<IEnumerable<ClassDTO>>.NotFound();
    }

    var classDtos = classes.Select(c => new ClassDTO(c.Id, c.Name, c.Year, c.CareerId, new SimpleCareerDTO(c.CareerId, c.Career?.Name ?? string.Empty), c.Subjects?.Select(s => new SimpleSubjectDTO(s.Id, s.Name, c.Id, new SimpleClassDTO(c.Id, c.Name, c.Year, new SimpleCareerDTO(c.CareerId, c.Career?.Name ?? string.Empty)))).ToList() ?? new List<SimpleSubjectDTO>()));
    return Result<IEnumerable<ClassDTO>>.Success(classDtos);
  }
}
