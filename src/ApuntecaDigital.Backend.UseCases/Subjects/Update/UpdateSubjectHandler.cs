using Ardalis.Result;
using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Subjects.Update;

public class UpdateSubjectHandler : IRequestHandler<UpdateSubjectCommand, Result<SubjectDTO>>
{
  private readonly IRepository<Subject> _repository;

  public UpdateSubjectHandler(IRepository<Subject> repository)
  {
    _repository = repository;
  }

  public async Task<Result<SubjectDTO>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
  {
    var spec = new SubjectByIdSpec(request.Id);
    var subject = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (subject == null)
    {
      return Result<SubjectDTO>.NotFound();
    }

    subject.UpdateName(request.Name);
    subject.UpdateClassId(request.ClassId);

    await _repository.SaveChangesAsync(cancellationToken);

    return Result<SubjectDTO>.Success(new SubjectDTO(subject.Id, subject.Name, subject.ClassId));
  }
}
