using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Classes;
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
    Career? career = null;
    if (request.CareerId.HasValue)
    {
      var spec = new CareerByIdSpec(request.CareerId.Value);
      career = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    }
    else if (!string.IsNullOrEmpty(request.CareerName))
    {
      var spec = new CareerByNameSpec(request.CareerName);
      career = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    }

    if (career == null)
    {
      return Result<CareerDTO>.NotFound();
    }

    return Result<CareerDTO>.Success(
      new CareerDTO(
        career.Id,
        career.Name,
        career.Classes.Select(cl => new SimpleClassDTO(
          cl.Id,
          cl.Name,
          cl.Year,
          new SimpleCareerDTO(
            cl.Career?.Id ?? 0,
            cl.Career?.Name ?? string.Empty
          )
        )).ToList()
      )
    );
  }
}

