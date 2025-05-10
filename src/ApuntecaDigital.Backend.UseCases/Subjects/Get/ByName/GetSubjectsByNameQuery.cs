using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Subjects.Get.ByName;

public record GetSubjectsByNameQuery(string Name) : IRequest<Result<IEnumerable<SubjectDTO>>>;
