using System.ComponentModel.DataAnnotations;

namespace ApuntecaDigital.Backend.Web.Careers;

public class UpdateCareerRequest
{
  public const string Route = "/Careers/{CareerId:int}";
  public static string BuildRoute(int careerId) => Route.Replace("{CareerId:int}", careerId.ToString());

  public int CareerId { get; set; }

  [Required]
  public int Id { get; set; }
  
  [Required]
  public string? Name { get; set; }
}
