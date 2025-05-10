namespace ApuntecaDigital.Backend.Core.BookAggregate.Specifications;

public class BookByTitleSpec : Specification<Book>
{
  public BookByTitleSpec(string bookTitle) =>
    Query
        .Where(book => book.Title == bookTitle);
}
