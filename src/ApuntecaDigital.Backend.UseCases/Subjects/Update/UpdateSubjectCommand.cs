using Ardalis.Result;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Subjects.Update;

public record UpdateSubjectCommand(int Id, string Name, int ClassId) : IRequest<Result<SubjectDTO>>;
