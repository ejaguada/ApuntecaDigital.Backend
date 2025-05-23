using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Books.Update;

public record UpdateBookCommand(int Id, string Title, string Author, string Isbn, int SubjectId) : IRequest<Result<UpdateBookDTO>>;
