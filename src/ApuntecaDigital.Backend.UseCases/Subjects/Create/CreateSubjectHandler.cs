using Ardalis.Result;
using ApuntecaDigital.Backend.Core.CareerAggregate;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Subjects.Create;

public class CreateSubjectHandler : IRequestHandler<CreateSubjectCommand, Result<int>>
{
  private readonly IRepository<Subject> _repository;

  public CreateSubjectHandler(IRepository<Subject> repository)
  {
    _repository = repository;
  }

  public async Task<Result<int>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
  {
    var newSubject = new Subject(request.Name, request.ClassId);
    
    await _repository.AddAsync(newSubject, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result<int>.Success(newSubject.Id);
  }
}
