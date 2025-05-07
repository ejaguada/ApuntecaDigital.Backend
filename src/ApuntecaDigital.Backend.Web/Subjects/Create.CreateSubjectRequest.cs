using System.ComponentModel.DataAnnotations;

namespace ApuntecaDigital.Backend.Web.Subjects;

public class CreateSubjectRequest
{
  public const string Route = "/Subjects";

  [Required]
  public string? Name { get; set; }
  
  [Required]
  public int ClassId { get; set; }
}
