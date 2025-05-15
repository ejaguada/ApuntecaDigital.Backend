namespace ApuntecaDigital.Backend.Web.Books;

public class UpdateBookResponse(UpdateBookRecord book)
{
  public UpdateBookRecord Book { get; set; } = book;
}
