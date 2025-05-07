using System.ComponentModel.DataAnnotations;

namespace ApuntecaDigital.Backend.Web.Careers;

public class CreateCareerRequest
{
  public const string Route = "/Careers";

  [Required]
  public string? Name { get; set; }
}
