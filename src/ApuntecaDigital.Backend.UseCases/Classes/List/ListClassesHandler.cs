using ApuntecaDigital.Backend.UseCases.Subjects;
using ApuntecaDigital.Backend.Core.ClassAggregate;
using ApuntecaDigital.Backend.Core.ClassAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Careers;

namespace ApuntecaDigital.Backend.UseCases.Classes.List;

public class ListClassesHandler : IQueryHandler<ListClassesQuery, Result<IEnumerable<ClassDTO>>>
{
  private readonly IRepository<Class> _repository;

  public ListClassesHandler(IRepository<Class> repository)
  {
    _repository = repository;
  }

  public async Task<Result<IEnumerable<ClassDTO>>> Handle(ListClassesQuery request, CancellationToken cancellationToken)
  {
    var classes = await _repository.ListAsync(new ClassesSpec(), cancellationToken);

    if (classes == null || classes.Count == 0)
    {
      return Result<IEnumerable<ClassDTO>>.NotFound();
    }

    return Result.Success(classes.Select(c => new ClassDTO(c.Id, c.Name, c.Year, c.Career?.Id ?? 0, new SimpleCareerDTO(c.Career?.Id ?? 0, c.Career?.Name ?? string.Empty), c.Subjects.Select(s => new SimpleSubjectDTO(s.Id, s.Name, c.Id, new SimpleClassDTO(c.Id, c.Name, c.Year, new SimpleCareerDTO(c.Career?.Id ?? 0, c.Career?.Name ?? string.Empty)))).ToList())));
  }
}
