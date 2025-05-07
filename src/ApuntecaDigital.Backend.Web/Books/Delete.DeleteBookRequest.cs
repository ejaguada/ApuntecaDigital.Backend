namespace ApuntecaDigital.Backend.Web.Books;

public record DeleteBookRequest
{
  public const string Route = "/Books/{BookId:int}";
  public static string BuildRoute(int bookId) => Route.Replace("{BookId:int}", bookId.ToString());

  public int BookId { get; set; }
}
