using ApuntecaDigital.Backend.Core.SubjectAggregate;
using ApuntecaDigital.Backend.Core.SubjectAggregate.Specifications;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Subjects.Update;

public class UpdateSubjectHandler : IRequestHandler<UpdateSubjectCommand, Result<UpdateSubjectDTO>>
{
  private readonly IRepository<Subject> _repository;

  public UpdateSubjectHandler(IRepository<Subject> repository)
  {
    _repository = repository;
  }

  public async Task<Result<UpdateSubjectDTO>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
  {
    var spec = new SubjectByIdSpec(request.Id);
    var subject = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (subject == null)
    {
      return Result<UpdateSubjectDTO>.NotFound();
    }

    subject.UpdateName(request.Name);
    subject.UpdateClassId(request.ClassId);

    await _repository.SaveChangesAsync(cancellationToken);

    return Result<UpdateSubjectDTO>.Success(new UpdateSubjectDTO(subject.Id, subject.Name, subject.ClassId));
  }
}
