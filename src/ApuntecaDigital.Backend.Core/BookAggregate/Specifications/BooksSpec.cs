namespace ApuntecaDigital.Backend.Core.BookAggregate.Specifications;

public class BooksSpec : Specification<Book>
{
  public BooksSpec() =>
    Query
        .Include(book => book.Subject)
        .ThenInclude(s => s!.Class)
        .ThenInclude(c => c!.Career);
}
