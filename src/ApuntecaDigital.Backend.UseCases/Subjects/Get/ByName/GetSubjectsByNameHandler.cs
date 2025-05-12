using ApuntecaDigital.Backend.Core.SubjectAggregate;
using ApuntecaDigital.Backend.Core.SubjectAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Classes;
using ApuntecaDigital.Backend.UseCases.Books;
using ApuntecaDigital.Backend.UseCases.Careers;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Subjects.Get.ByName;

public class GetSubjectsByNameHandler : IRequestHandler<GetSubjectsByNameQuery, Result<IEnumerable<SubjectDTO>>>
{
  private readonly IRepository<Subject> _repository;

  public GetSubjectsByNameHandler(IRepository<Subject> repository)
  {
    _repository = repository;
  }

  public async Task<Result<IEnumerable<SubjectDTO>>> Handle(GetSubjectsByNameQuery request, CancellationToken cancellationToken)
  {
    var subjects = new List<Subject>();

    if (!string.IsNullOrEmpty(request.Name))
    {
      var spec = new SubjectByNameSpec(request.Name);
      subjects = await _repository.ListAsync(spec, cancellationToken);
    }


    if (subjects == null || subjects.Count == 0)
    {
      return Result<IEnumerable<SubjectDTO>>.NotFound();
    }

    var subjectDtos = subjects.Select(subject => new SubjectDTO(
      subject.Id,
      subject.Name,
      subject.ClassId,
      new SimpleClassDTO(
          subject.ClassId,
          subject.Class?.Name ?? string.Empty,
          subject.Class?.Year ?? 0,
          new SimpleCareerDTO(
              subject.Class?.Career?.Id ?? 0,
              subject.Class?.Career?.Name ?? string.Empty
          )
      ),
      subject.Books.Select(book => new SimpleBookDTO(
          book.Id,
          book.Title,
          book.Author,
          book.Isbn
      )).ToList()
    ));
    return Result<IEnumerable<SubjectDTO>>.Success(subjectDtos);
  }
}
