using ApuntecaDigital.Backend.Core.ClassAggregate;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Create;

public class CreateClassHandler : IRequestHandler<CreateClassCommand, Result<int>>
{
  private readonly IRepository<Class> _repository;

  public CreateClassHandler(IRepository<Class> repository)
  {
    _repository = repository;
  }

  public async Task<Result<int>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
  {
    var newClass = new Class(request.Name, request.Year, request.CareerId);
    
    await _repository.AddAsync(newClass, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result<int>.Success(newClass.Id);
  }
}
