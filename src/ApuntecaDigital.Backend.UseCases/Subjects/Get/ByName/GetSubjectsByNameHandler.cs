using ApuntecaDigital.Backend.Core.SubjectAggregate;
using ApuntecaDigital.Backend.Core.SubjectAggregate.Specifications;
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

    var subjectDtos = subjects.Select(s => new SubjectDTO(s.Id, s.Name, s.ClassId)).ToList();
    return Result<IEnumerable<SubjectDTO>>.Success(subjectDtos);
  }
}
