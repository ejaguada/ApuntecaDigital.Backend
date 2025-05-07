using Ardalis.Result;
using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Careers.Update;

public class UpdateCareerHandler : IRequestHandler<UpdateCareerCommand, Result<CareerDTO>>
{
  private readonly IRepository<Career> _repository;

  public UpdateCareerHandler(IRepository<Career> repository)
  {
    _repository = repository;
  }

  public async Task<Result<CareerDTO>> Handle(UpdateCareerCommand request, CancellationToken cancellationToken)
  {
    var spec = new CareerByIdSpec(request.Id);
    var career = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (career == null)
    {
      return Result<CareerDTO>.NotFound();
    }

    career.UpdateName(request.Name);

    await _repository.SaveChangesAsync(cancellationToken);

    return Result<CareerDTO>.Success(new CareerDTO(career.Id, career.Name));
  }
}
