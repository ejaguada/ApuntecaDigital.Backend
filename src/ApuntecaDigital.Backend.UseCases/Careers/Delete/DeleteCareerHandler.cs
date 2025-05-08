using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Careers.Delete;

public class DeleteCareerHandler : IRequestHandler<DeleteCareerCommand, Result>
{
  private readonly IRepository<Career> _repository;

  public DeleteCareerHandler(IRepository<Career> repository)
  {
    _repository = repository;
  }

  public async Task<Result> Handle(DeleteCareerCommand request, CancellationToken cancellationToken)
  {
    var spec = new CareerByIdSpec(request.CareerId);
    var career = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (career == null)
    {
      return Result.NotFound();
    }

    await _repository.DeleteAsync(career, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}
