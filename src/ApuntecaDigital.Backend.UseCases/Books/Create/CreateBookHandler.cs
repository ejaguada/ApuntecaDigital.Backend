using ApuntecaDigital.Backend.Core.BookAggregate;
using MediatR;

namespace ApuntecaDigital.Backend.UseCases.Books.Create;

public class CreateBookHandler : IRequestHandler<CreateBookCommand, Result<int>>
{
  private readonly IRepository<Book> _repository;

  public CreateBookHandler(IRepository<Book> repository)
  {
    _repository = repository;
  }

  public async Task<Result<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
  {
    // Since the Book entity requires description and subjectId, we'll use default values for now
    // In a real application, these would be provided in the request
    var newBook = new Book(request.Title, request.Author, request.Isbn, "Default description", request.SubjectId);

    await _repository.AddAsync(newBook, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result<int>.Success(newBook.Id);
  }
}
