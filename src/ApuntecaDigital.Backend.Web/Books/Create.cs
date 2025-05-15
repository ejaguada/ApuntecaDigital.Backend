using ApuntecaDigital.Backend.UseCases.Books.Create;

namespace ApuntecaDigital.Backend.Web.Books;

/// <summary>
/// Create a new Book
/// </summary>
/// <remarks>
/// Creates a new Book given a title, author, and ISBN.
/// </remarks>
public class Create(IMediator _mediator)
  : Endpoint<CreateBookRequest, CreateBookResponse>
{
  public override void Configure()
  {
    Post(CreateBookRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.ExampleRequest = new CreateBookRequest { Title = "Book Title", Author = "Book Author", Isbn = "1234567890", SubjectId = 2 };
    });
  }

  public override async Task HandleAsync(
    CreateBookRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new CreateBookCommand(request.Title!,
      request.Author!, request.Isbn!, request.SubjectId!), cancellationToken);

    if (result.IsSuccess)
    {
      Response = new CreateBookResponse(result.Value, request.Title!, request.Author!, request.Isbn!, request.SubjectId!);
      return;
    }
    // TODO: Handle other cases as necessary
  }
}
