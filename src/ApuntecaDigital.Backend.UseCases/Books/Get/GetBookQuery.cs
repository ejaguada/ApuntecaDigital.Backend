using Ardalis.Result;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Books.Get;

public record GetBookQuery(int BookId) : IRequest<Result<BookDTO>>;
