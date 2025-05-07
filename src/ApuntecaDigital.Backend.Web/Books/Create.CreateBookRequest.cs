using System.ComponentModel.DataAnnotations;

namespace ApuntecaDigital.Backend.Web.Books;

public class CreateBookRequest
{
  public const string Route = "/Books";

  [Required]
  public string? Title { get; set; }
  
  [Required]
  public string? Author { get; set; }
  
  [Required]
  public string? Isbn { get; set; }
}
