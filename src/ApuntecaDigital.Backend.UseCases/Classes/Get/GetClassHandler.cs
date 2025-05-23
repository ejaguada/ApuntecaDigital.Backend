using ApuntecaDigital.Backend.Core.ClassAggregate;
using ApuntecaDigital.Backend.Core.ClassAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Subjects;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Get;

public class GetClassHandler : IRequestHandler<GetClassQuery, Result<ClassDTO>>
{
  private readonly IRepository<Class> _repository;

  public GetClassHandler(IRepository<Class> repository)
  {
    _repository = repository;
  }

  public async Task<Result<ClassDTO>> Handle(GetClassQuery request, CancellationToken cancellationToken)
  {
    var spec = new ClassByIdSpec(request.ClassId);
    var classObj = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (classObj == null)
    {
      return Result<ClassDTO>.NotFound();
    }

    return Result<ClassDTO>.Success(
      new ClassDTO(
        classObj.Id,
        classObj.Name,
        classObj.Year,
        classObj.CareerId,
        new SimpleCareerDTO(
          classObj.CareerId,
          classObj.Career?.Name ?? string.Empty
        ),
        classObj.Subjects.Select(s => new SimpleSubjectDTO(
          s.Id,
          s.Name,
          classObj.Id,
          new SimpleClassDTO(
            classObj.Id,
            classObj.Name,
            classObj.Year,
            new SimpleCareerDTO(
              classObj.Career?.Id ?? 0,
              classObj.Career?.Name ?? string.Empty
            )
          )
        )).ToList()
      )
    );
  }
}
