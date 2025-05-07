using Ardalis.Result;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Careers.Delete;

public record DeleteCareerCommand(int CareerId) : IRequest<Result>;
