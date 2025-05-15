using System.ComponentModel.DataAnnotations;

namespace ApuntecaDigital.Backend.Web.Books;

public class CreateBookRequest
{
  public const string Route = "/Books";

  [Required]
  public string? Title { get; set; }

  public string Author { get; set; } = string.Empty;

  public string Isbn { get; set; } = string.Empty;
  public int SubjectId { get; set; } = 0;
}
