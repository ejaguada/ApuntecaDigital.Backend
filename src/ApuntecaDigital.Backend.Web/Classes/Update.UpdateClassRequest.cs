using System.ComponentModel.DataAnnotations;

namespace ApuntecaDigital.Backend.Web.Classes;

public class UpdateClassRequest
{
  public const string Route = "/Classes/{ClassId:int}";
  public static string BuildRoute(int classId) => Route.Replace("{ClassId:int}", classId.ToString());

  public int ClassId { get; set; }

  [Required]
  public int Id { get; set; }
  
  [Required]
  public string? Name { get; set; }
  
  [Required]
  public int Year { get; set; }
  
  [Required]
  public int CareerId { get; set; }
}
