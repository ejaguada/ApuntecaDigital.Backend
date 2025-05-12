namespace ApuntecaDigital.Backend.Core.BookAggregate.Specifications;

public class BookByTitleSpec : Specification<Book>
{
  public BookByTitleSpec(string bookTitle) =>
    Query
        .Where(book => book.Title != null && (book.Title.StartsWith(bookTitle) || book.Title.EndsWith(bookTitle) || book.Title.Contains(bookTitle)))
        .Include(book => book.Subject)
        .ThenInclude(s => s!.Class)
        .ThenInclude(c => c!.Career);

}
