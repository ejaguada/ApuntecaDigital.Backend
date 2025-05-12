using ApuntecaDigital.Backend.Core.ClassAggregate;
using ApuntecaDigital.Backend.Core.ClassAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Careers;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Update;
using ApuntecaDigital.Backend.UseCases.Subjects;

public class UpdateClassHandler : IRequestHandler<UpdateClassCommand, Result<UpdateClassDTO>>
{
  private readonly IRepository<Class> _repository;

  public UpdateClassHandler(IRepository<Class> repository)
  {
    _repository = repository;
  }

  public async Task<Result<UpdateClassDTO>> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
  {
    var spec = new ClassByIdSpec(request.Id);
    var classObj = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (classObj == null)
    {
      return Result<UpdateClassDTO>.NotFound();
    }

    classObj.UpdateName(request.Name);
    classObj.UpdateYear(request.Year);
    classObj.UpdateCareerId(request.CareerId);

    await _repository.SaveChangesAsync(cancellationToken);

    return Result<UpdateClassDTO>.Success(new UpdateClassDTO(classObj.Id, classObj.Name, classObj.Year, classObj.CareerId));
  }
}
