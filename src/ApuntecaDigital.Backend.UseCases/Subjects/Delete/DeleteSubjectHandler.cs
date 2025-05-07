using Ardalis.Result;
using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Subjects.Delete;

public class DeleteSubjectHandler : IRequestHandler<DeleteSubjectCommand, Result>
{
  private readonly IRepository<Subject> _repository;

  public DeleteSubjectHandler(IRepository<Subject> repository)
  {
    _repository = repository;
  }

  public async Task<Result> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
  {
    var spec = new SubjectByIdSpec(request.SubjectId);
    var subject = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (subject == null)
    {
      return Result.NotFound();
    }

    await _repository.DeleteAsync(subject, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}
