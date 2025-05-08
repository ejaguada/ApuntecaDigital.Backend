using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Subjects.Delete;

public record DeleteSubjectCommand(int SubjectId) : IRequest<Result>;
