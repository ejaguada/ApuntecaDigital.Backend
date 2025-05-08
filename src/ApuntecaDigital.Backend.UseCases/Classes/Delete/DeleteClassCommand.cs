using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Delete;

public record DeleteClassCommand(int ClassId) : IRequest<Result>;
