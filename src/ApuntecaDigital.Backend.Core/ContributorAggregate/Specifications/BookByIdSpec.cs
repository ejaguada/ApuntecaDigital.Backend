using ApuntecaDigital.Backend.Core.CareerAggregate;

namespace ApuntecaDigital.Backend.Core.ContributorAggregate.Specifications;

public class BookByIdSpec : Specification<Book>
{
  public BookByIdSpec(int bookId) =>
    Query
        .Where(book => book.Id == bookId);
}
