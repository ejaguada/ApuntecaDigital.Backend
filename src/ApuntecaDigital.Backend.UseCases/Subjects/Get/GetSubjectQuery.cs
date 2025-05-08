using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Subjects.Get;

public record GetSubjectQuery(int SubjectId) : IRequest<Result<SubjectDTO>>;
