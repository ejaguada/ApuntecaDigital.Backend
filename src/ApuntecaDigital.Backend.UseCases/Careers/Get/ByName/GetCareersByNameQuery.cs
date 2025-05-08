using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Careers.Get;

public record GetCareersByNameQuery(string? CareerName) : IRequest<Result<IEnumerable<CareerDTO>>>;
