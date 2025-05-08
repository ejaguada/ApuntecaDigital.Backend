using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Careers.Create;

public record CreateCareerCommand(string Name) : IRequest<Result<int>>;
