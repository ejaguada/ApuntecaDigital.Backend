using ApuntecaDigital.Backend.Core.SubjectAggregate;
using ApuntecaDigital.Backend.Core.SubjectAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Books;
using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Classes;

namespace ApuntecaDigital.Backend.UseCases.Subjects.List;

public class ListSubjectsHandler : IQueryHandler<ListSubjectsQuery, Result<IEnumerable<SubjectDTO>>>
{
  private readonly IRepository<Subject> _repository;

  public ListSubjectsHandler(IRepository<Subject> repository)
  {
    _repository = repository;
  }

  public async Task<Result<IEnumerable<SubjectDTO>>> Handle(ListSubjectsQuery request, CancellationToken cancellationToken)
  {
    var subjects = await _repository.ListAsync(new SubjectsSpec(), cancellationToken);

    if (subjects == null || subjects.Count == 0)
    {
      return Result<IEnumerable<SubjectDTO>>.NotFound();
    }
    return Result.Success(subjects.Select(s => new SubjectDTO(s.Id, s.Name, s.ClassId, new SimpleClassDTO(s.ClassId, s.Class?.Name ?? string.Empty, s.Class?.Year ?? 0, new SimpleCareerDTO(s.Class?.Career?.Id ?? 0, s.Class?.Career?.Name ?? string.Empty)), s.Books.Select(b => new SimpleBookDTO(b.Id, b.Title, b.Author, b.Isbn)).ToList())));
  }
}
