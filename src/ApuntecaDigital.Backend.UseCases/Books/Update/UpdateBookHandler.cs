using ApuntecaDigital.Backend.Core.BookAggregate;
using ApuntecaDigital.Backend.Core.BookAggregate.Specifications;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Books.Update;

public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Result<BookDTO>>
{
  private readonly IRepository<Book> _repository;

  public UpdateBookHandler(IRepository<Book> repository)
  {
    _repository = repository;
  }

  public async Task<Result<BookDTO>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
  {
    var spec = new BookByIdSpec(request.Id);
    var book = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (book == null)
    {
      return Result<BookDTO>.NotFound();
    }

    book.UpdateTitle(request.Title);
    book.UpdateAuthor(request.Author);
    book.UpdateIsbn(request.Isbn);

    await _repository.SaveChangesAsync(cancellationToken);

    return Result<BookDTO>.Success(new BookDTO(book.Id, book.Title, book.Author, book.Isbn));
  }
}
