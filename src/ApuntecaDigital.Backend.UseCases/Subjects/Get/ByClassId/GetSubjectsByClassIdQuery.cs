using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Subjects.Get.ByName;

public record GetSubjectsByClassIdQuery(int ClassId) : IRequest<Result<IEnumerable<SubjectDTO>>>;
