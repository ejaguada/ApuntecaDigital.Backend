using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Careers.Get;

public record GetCareerQuery(int? CareerId, string? CareerName) : IRequest<Result<CareerDTO>>;
