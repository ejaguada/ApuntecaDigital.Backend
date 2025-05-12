using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Classes;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Careers.Get;

public class GetCareersByNameHandler : IRequestHandler<GetCareersByNameQuery, Result<IEnumerable<CareerDTO>>>
{
  private readonly IRepository<Career> _repository;

  public GetCareersByNameHandler(IRepository<Career> repository)
  {
    _repository = repository;
  }

  public async Task<Result<IEnumerable<CareerDTO>>> Handle(GetCareersByNameQuery request, CancellationToken cancellationToken)
  {
    var careers = new List<Career>();

    if (!string.IsNullOrEmpty(request.CareerName))
    {
      var spec = new CareerByNameSpec(request.CareerName);
      careers = await _repository.ListAsync(spec, cancellationToken);
    }


    if (careers == null || careers.Count == 0)
    {
      return Result<IEnumerable<CareerDTO>>.NotFound();
    }

    var careerDtos = careers.Select(
      c => new CareerDTO(
        c.Id,
        c.Name,
        c.Classes.Select(cl => new SimpleClassDTO(
          cl.Id,
          cl.Name,
          cl.Year,
          new SimpleCareerDTO(
            c.Id,
            c.Name
          )
        )).ToList()
      )
    );
    return Result<IEnumerable<CareerDTO>>.Success(careerDtos);
  }
}
