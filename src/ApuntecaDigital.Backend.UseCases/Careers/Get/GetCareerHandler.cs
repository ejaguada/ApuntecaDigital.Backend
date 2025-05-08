using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Careers.Get;

public class GetCareerHandler : IRequestHandler<GetCareerQuery, Result<CareerDTO>>
{
  private readonly IRepository<Career> _repository;

  public GetCareerHandler(IRepository<Career> repository)
  {
    _repository = repository;
  }

  public async Task<Result<CareerDTO>> Handle(GetCareerQuery request, CancellationToken cancellationToken)
  {
    var spec = new CareerByIdSpec(request.CareerId);
    var career = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

    if (career == null)
    {
      return Result<CareerDTO>.NotFound();
    }

    return Result<CareerDTO>.Success(new CareerDTO(career.Id, career.Name));
  }
}
