using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Get.ByCareerId;

public record GetClassesByCareerIdQuery(int CareerId) : IRequest<Result<IEnumerable<ClassDTO>>>;
