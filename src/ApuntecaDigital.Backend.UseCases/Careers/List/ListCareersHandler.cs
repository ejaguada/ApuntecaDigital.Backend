using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Classes;
using ApuntecaDigital.Backend.UseCases.Subjects;
using Microsoft.VisualBasic.FileIO;

namespace ApuntecaDigital.Backend.UseCases.Careers.List;

public class ListCareersHandler : IQueryHandler<ListCareersQuery, Result<IEnumerable<CareerDTO>>>
{
  private readonly IRepository<Career> _repository;

  public ListCareersHandler(IRepository<Career> repository)
  {
    _repository = repository;
  }

  public async Task<Result<IEnumerable<CareerDTO>>> Handle(ListCareersQuery request, CancellationToken cancellationToken)
  {
    var careers = new List<Career>();

    careers = await _repository.ListAsync(new CareersSpec(), cancellationToken);

    if (careers == null || careers.Count == 0)
    {
      return Result<IEnumerable<CareerDTO>>.NotFound();
    }

    return Result.Success(careers.Select(c => new CareerDTO(
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
    )));
  }
}
