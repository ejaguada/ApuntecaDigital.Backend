using Ardalis.Result;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Classes.Get;

public record GetClassQuery(int ClassId) : IRequest<Result<ClassDTO>>;
