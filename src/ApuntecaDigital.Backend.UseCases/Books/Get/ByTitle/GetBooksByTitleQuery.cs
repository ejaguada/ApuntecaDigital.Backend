using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Books.Get.ByTitle;

public record GetBooksByTitleQuery(string? BookTitle) : IRequest<Result<IEnumerable<BookDTO>>>;
