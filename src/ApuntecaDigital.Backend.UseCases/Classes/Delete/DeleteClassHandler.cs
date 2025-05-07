using Ardalis.Result;
using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Delete;

public class DeleteClassHandler : IRequestHandler<DeleteClassCommand, Result>
{
  private readonly IRepository<Class> _repository;

  public DeleteClassHandler(IRepository<Class> repository)
  {
    _repository = repository;
  }

  public async Task<Result> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
  {
    var spec = new ClassByIdSpec(request.ClassId);
    var classObj = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (classObj == null)
    {
      return Result.NotFound();
    }

    await _repository.DeleteAsync(classObj, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}
