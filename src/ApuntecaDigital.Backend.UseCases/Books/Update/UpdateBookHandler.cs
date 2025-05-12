using ApuntecaDigital.Backend.Core.BookAggregate;
using ApuntecaDigital.Backend.Core.BookAggregate.Specifications;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Books.Update;

public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Result<UpdateBookDTO>>
{
  private readonly IRepository<Book> _repository;

  public UpdateBookHandler(IRepository<Book> repository)
  {
    _repository = repository;
  }

  public async Task<Result<UpdateBookDTO>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
  {
    var spec = new BookByIdSpec(request.Id);
    var book = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (book == null)
    {
      return Result<UpdateBookDTO>.NotFound();
    }

    book.UpdateTitle(request.Title);
    book.UpdateAuthor(request.Author);
    book.UpdateIsbn(request.Isbn);

    await _repository.SaveChangesAsync(cancellationToken);

    if (book.Subject == null) {
      return Result<UpdateBookDTO>.NotFound();
    }

    if (book.Subject.Class == null) {
      return Result<UpdateBookDTO>.NotFound();
    }

    if (book.Subject.Class.Career == null) {
      return Result<UpdateBookDTO>.NotFound();
    }

    return Result<UpdateBookDTO>.Success(new UpdateBookDTO(book.Id, book.Title, book.Author, book.Isbn, book.SubjectId));
  }
}
