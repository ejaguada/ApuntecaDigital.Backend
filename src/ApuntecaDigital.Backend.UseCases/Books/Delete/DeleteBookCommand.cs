using Ardalis.Result;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Books.Delete;

public record DeleteBookCommand(int BookId) : IRequest<Result>;
