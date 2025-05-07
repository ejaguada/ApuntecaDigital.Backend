using System.ComponentModel.DataAnnotations;

namespace ApuntecaDigital.Backend.Web.Subjects;

public class UpdateSubjectRequest
{
  public const string Route = "/Subjects/{SubjectId:int}";
  public static string BuildRoute(int subjectId) => Route.Replace("{SubjectId:int}", subjectId.ToString());

  public int SubjectId { get; set; }

  [Required]
  public int Id { get; set; }
  
  [Required]
  public string? Name { get; set; }
  
  [Required]
  public int ClassId { get; set; }
}
