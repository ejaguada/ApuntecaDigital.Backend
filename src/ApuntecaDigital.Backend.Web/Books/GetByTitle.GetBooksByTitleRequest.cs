namespace ApuntecaDigital.Backend.Web.Books;

public class GetBooksByTitleRequest
{
  public const string Route = "/Books/title/{Title}";
  public static string BuildRoute(string title) => Route.Replace("{Title}", title);

  public string Title { get; set; } = string.Empty;
}
