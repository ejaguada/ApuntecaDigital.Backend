using Ardalis.Specification;

namespace ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;

public class BookByIdSpec : Specification<Book>
{
  public BookByIdSpec(int bookId) =>
    Query
        .Where(book => book.Id == bookId);
}
