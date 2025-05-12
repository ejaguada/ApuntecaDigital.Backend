using ApuntecaDigital.Backend.Core.SubjectAggregate;
using ApuntecaDigital.Backend.Core.SubjectAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Books;
using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Classes;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Subjects.Get;

public class GetSubjectHandler : IRequestHandler<GetSubjectQuery, Result<SubjectDTO>>
{
  private readonly IRepository<Subject> _repository;

  public GetSubjectHandler(IRepository<Subject> repository)
  {
    _repository = repository;
  }

  public async Task<Result<SubjectDTO>> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
  {
    var spec = new SubjectByIdSpec(request.SubjectId);
    var subject = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

    if (subject == null)
    {
      return Result<SubjectDTO>.NotFound();
    }

    return Result<SubjectDTO>.Success(new SubjectDTO(
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
      )).ToList()));
  }
}
