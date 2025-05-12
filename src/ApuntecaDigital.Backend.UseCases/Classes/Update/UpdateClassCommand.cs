using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Update;

public record UpdateClassCommand(int Id, string Name, int Year, int CareerId) : IRequest<Result<UpdateClassDTO>>;
