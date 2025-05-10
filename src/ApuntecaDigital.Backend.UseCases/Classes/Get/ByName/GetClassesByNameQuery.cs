using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Get;

public record GetClassesByNameQuery(string? ClassName) : IRequest<Result<IEnumerable<ClassDTO>>>;
