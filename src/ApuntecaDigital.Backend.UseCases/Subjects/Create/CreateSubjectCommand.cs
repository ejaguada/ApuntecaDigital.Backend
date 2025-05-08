using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Subjects.Create;

public record CreateSubjectCommand(string Name, int ClassId) : IRequest<Result<int>>;
