using Ardalis.Result;
using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
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

    return Result<SubjectDTO>.Success(new SubjectDTO(subject.Id, subject.Name, subject.ClassId));
  }
}
