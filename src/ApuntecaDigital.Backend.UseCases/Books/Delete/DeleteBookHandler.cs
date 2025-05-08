using MediatR;
using ApuntecaDigital.Backend.Core.BookAggregate;
using ApuntecaDigital.Backend.Core.BookAggregate.Specifications;

namespace ApuntecaDigital.Backend.UseCases.Books.Delete;

public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Result>
{
  private readonly IRepository<Book> _repository;

  public DeleteBookHandler(IRepository<Book> repository)
  {
    _repository = repository;
  }

  public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
  {
    var spec = new BookByIdSpec(request.BookId);
    var book = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    
    if (book == null)
    {
      return Result.NotFound();
    }

    await _repository.DeleteAsync(book, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}
