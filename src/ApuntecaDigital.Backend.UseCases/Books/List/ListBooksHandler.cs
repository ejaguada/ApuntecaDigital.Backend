using ApuntecaDigital.Backend.Core.BookAggregate;
using ApuntecaDigital.Backend.Core.BookAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Classes;
using ApuntecaDigital.Backend.UseCases.Subjects;

namespace ApuntecaDigital.Backend.UseCases.Books.List;

public class ListBooksHandler : IQueryHandler<ListBooksQuery, Result<IEnumerable<BookDTO>>>
{
  private readonly IRepository<Book> _repository;

  public ListBooksHandler(IRepository<Book> repository)
  {
    _repository = repository;
  }

  public async Task<Result<IEnumerable<BookDTO>>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
  {
    var books = await _repository.ListAsync(new BooksSpec(), cancellationToken);

    if (books == null || books.Count == 0)
    {
      return Result<IEnumerable<BookDTO>>.NotFound();
    }

    return Result.Success(books.Select(b => new BookDTO(b.Id, b.Title, b.Author, b.Isbn, b.SubjectId, new SimpleSubjectDTO(b.SubjectId, b.Subject?.Name ?? string.Empty, b.Subject?.ClassId ?? 0, new SimpleClassDTO(b.Subject?.ClassId ?? 0, b.Subject?.Class?.Name ?? string.Empty, b.Subject?.Class?.Year ?? 0, new SimpleCareerDTO(b.Subject?.Class?.Career?.Id ?? 0, b.Subject?.Class?.Career?.Name ?? string.Empty))))));
  }
}
