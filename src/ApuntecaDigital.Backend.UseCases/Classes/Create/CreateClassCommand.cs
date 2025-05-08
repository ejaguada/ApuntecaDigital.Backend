using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Create;

public record CreateClassCommand(string Name, int Year, int CareerId) : IRequest<Result<int>>;
