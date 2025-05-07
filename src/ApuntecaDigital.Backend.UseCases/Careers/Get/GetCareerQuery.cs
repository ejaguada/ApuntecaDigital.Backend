using Ardalis.Result;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Careers.Get;

public record GetCareerQuery(int CareerId) : IRequest<Result<CareerDTO>>;
