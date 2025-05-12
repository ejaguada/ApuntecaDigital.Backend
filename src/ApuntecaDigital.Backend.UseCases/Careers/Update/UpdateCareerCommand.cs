using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Careers.Update;

public record UpdateCareerCommand(int Id, string Name) : IRequest<Result<UpdateCareerDTO>>;
