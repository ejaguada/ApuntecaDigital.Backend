using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Classes;
using ApuntecaDigital.Backend.UseCases.Subjects;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Careers.Update;

public class UpdateCareerHandler : IRequestHandler<UpdateCareerCommand, Result<UpdateCareerDTO>>
{
  private readonly IRepository<Career> _repository;

  public UpdateCareerHandler(IRepository<Career> repository)
  {
    _repository = repository;
  }

  public async Task<Result<UpdateCareerDTO>> Handle(UpdateCareerCommand request, CancellationToken cancellationToken)
  {
    var spec = new CareerByIdSpec(request.Id);
    var career = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (career == null)
    {
      return Result<UpdateCareerDTO>.NotFound();
    }

    career.UpdateName(request.Name);

    await _repository.SaveChangesAsync(cancellationToken);

    return Result<UpdateCareerDTO>.Success(new UpdateCareerDTO(career.Id, career.Name));
  }
}
