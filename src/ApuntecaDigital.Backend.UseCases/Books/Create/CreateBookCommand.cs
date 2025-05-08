using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Books.Create;

public record CreateBookCommand(string Title, string Author, string Isbn) : IRequest<Result<int>>;
