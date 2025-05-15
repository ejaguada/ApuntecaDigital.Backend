using System.ComponentModel.DataAnnotations;

namespace ApuntecaDigital.Backend.Web.Books;

public class UpdateBookRequest
{
  public const string Route = "/Books/{BookId:int}";
  public static string BuildRoute(int bookId) => Route.Replace("{BookId:int}", bookId.ToString());

  public int BookId { get; set; }

  [Required]
  public int Id { get; set; }
  
  [Required]
  public string? Title { get; set; }
  
  [Required]
  public string? Author { get; set; }
  
  [Required]
  public string? Isbn { get; set; }
  [Required]
  public int SubjectId { get; set; }
}
