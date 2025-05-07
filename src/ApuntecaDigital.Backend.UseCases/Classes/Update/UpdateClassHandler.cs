using Ardalis.Result;
using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Update;

public class UpdateClassHandler : IRequestHandler<UpdateClassCommand, Result<ClassDTO>>
{
  private readonly IRepository<Class> _repository;

  public UpdateClassHandler(IRepository<Class> repository)
  {
    _repository = repository;
  }

  public async Task<Result<ClassDTO>> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
  {
    var spec = new ClassByIdSpec(request.Id);
    var classObj = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (classObj == null)
    {
      return Result<ClassDTO>.NotFound();
    }

    classObj.UpdateName(request.Name);
    classObj.UpdateYear(request.Year);
    classObj.UpdateCareerId(request.CareerId);

    await _repository.SaveChangesAsync(cancellationToken);

    return Result<ClassDTO>.Success(new ClassDTO(classObj.Id, classObj.Name, classObj.Year, classObj.CareerId));
  }
}
