namespace ApuntecaDigital.Backend.Web.Books;

public class UpdateBookResponse(BookRecord book)
{
  public BookRecord Book { get; set; } = book;
}
