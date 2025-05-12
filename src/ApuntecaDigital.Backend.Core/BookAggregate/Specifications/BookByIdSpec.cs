namespace ApuntecaDigital.Backend.Core.BookAggregate.Specifications;

public class BookByIdSpec : Specification<Book>
{
  public BookByIdSpec(int bookId) =>
    Query
        .Where(book => book.Id == bookId)
        .Include(book => book.Subject)
        .ThenInclude(s => s!.Class)
        .ThenInclude(c => c!.Career);

}
