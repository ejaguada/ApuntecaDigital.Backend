using Ardalis.Result;
using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate.Specifications;
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

    return Result<BookDTO>.Success(new BookDTO(book.Id, book.Title, book.Author, book.Isbn));
  }
}
