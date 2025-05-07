using Ardalis.Result;
using ApuntecaDigital.Backend.Core.CareerAggregate;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Careers.Create;

public class CreateCareerHandler : IRequestHandler<CreateCareerCommand, Result<int>>
{
  private readonly IRepository<Career> _repository;

  public CreateCareerHandler(IRepository<Career> repository)
  {
    _repository = repository;
  }

  public async Task<Result<int>> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
  {
    var newCareer = new Career(request.Name);
    
    await _repository.AddAsync(newCareer, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result<int>.Success(newCareer.Id);
  }
}
