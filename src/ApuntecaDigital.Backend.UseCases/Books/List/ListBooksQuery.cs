namespace ApuntecaDigital.Backend.UseCases.Books.List;

public record ListBooksQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<BookDTO>>>;
