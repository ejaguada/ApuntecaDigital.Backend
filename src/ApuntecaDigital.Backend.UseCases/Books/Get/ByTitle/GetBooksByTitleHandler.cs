using ApuntecaDigital.Backend.Core.BookAggregate;
using ApuntecaDigital.Backend.Core.BookAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Classes;
using ApuntecaDigital.Backend.UseCases.Subjects;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Books.Get.ByTitle;

public class GetBooksByTitleHandler : IRequestHandler<GetBooksByTitleQuery, Result<IEnumerable<BookDTO>>>
{
  private readonly IRepository<Book> _repository;

  public GetBooksByTitleHandler(IRepository<Book> repository)
  {
    _repository = repository;
  }

  public async Task<Result<IEnumerable<BookDTO>>> Handle(GetBooksByTitleQuery request, CancellationToken cancellationToken)
  {
    var books = new List<Book>();

    if (!string.IsNullOrEmpty(request.BookTitle))
    {
      var spec = new BookByTitleSpec(request.BookTitle);
      books = await _repository.ListAsync(spec, cancellationToken);
    }


    if (books == null || books.Count == 0)
    {
      return Result<IEnumerable<BookDTO>>.NotFound();
    }

    var bookDtos = books.Select(b => new BookDTO(
      b.Id,
      b.Title,
      b.Author,
      b.Isbn,
      b.SubjectId,
      new SimpleSubjectDTO(
        b.SubjectId,
        b.Subject?.Name ?? string.Empty,
        b.Subject?.ClassId ?? 0,
        new SimpleClassDTO(
          b.Subject?.ClassId ?? 0,
          b.Subject?.Class?.Name ?? string.Empty,
          b.Subject?.Class?.Year ?? 0,
          new SimpleCareerDTO(
            b.Subject?.Class?.Career?.Id ?? 0,
            b.Subject?.Class?.Career?.Name ?? string.Empty
          )
        )
      )
    ));
    
    return Result<IEnumerable<BookDTO>>.Success(bookDtos);
  }
}
