using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Books.Create;

public record CreateBookCommand(string Title, string Author, string Isbn, int SubjectId) : IRequest<Result<int>>;
