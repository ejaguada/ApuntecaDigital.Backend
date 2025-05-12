using ApuntecaDigital.Backend.Core.BookAggregate;
using ApuntecaDigital.Backend.Core.BookAggregate.Specifications;
using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Classes;
using ApuntecaDigital.Backend.UseCases.Subjects;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Books.Get;

public class GetBookHandler : IRequestHandler<GetBookQuery, Result<BookDTO>>
{
  private readonly IRepository<Book> _repository;

  public GetBookHandler(IRepository<Book> repository)
  {
    _repository = repository;
  }

  public async Task<Result<BookDTO>> Handle(GetBookQuery request, CancellationToken cancellationToken)
  {
    var spec = new BookByIdSpec(request.BookId);
    var book = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (book == null)
    {
      return Result<BookDTO>.NotFound();
    }

    return Result<BookDTO>.Success(new BookDTO(
      book.Id,
      book.Title,
      book.Author,
      book.Isbn,
      book.SubjectId,
      new SimpleSubjectDTO(
        book.SubjectId,
        book.Subject?.Name ?? string.Empty,
        book.Subject?.ClassId ?? 0,
        new SimpleClassDTO(
          book.Subject?.ClassId ?? 0,
          book.Subject?.Class?.Name ?? string.Empty,
          book.Subject?.Class?.Year ?? 0,
          new SimpleCareerDTO(
            book.Subject?.Class?.Career?.Id ?? 0,
            book.Subject?.Class?.Career?.Name ?? string.Empty
          )
      )
    )));
  }
}
