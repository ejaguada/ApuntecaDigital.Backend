using System.ComponentModel.DataAnnotations;

namespace ApuntecaDigital.Backend.Web.Classes;

public class CreateClassRequest
{
  public const string Route = "/Classes";

  [Required]
  public string? Name { get; set; }
  
  [Required]
  public int Year { get; set; }
  
  [Required]
  public int CareerId { get; set; }
}
