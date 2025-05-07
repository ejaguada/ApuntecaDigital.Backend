namespace ApuntecaDigital.Backend.Web.Subjects;

public class GetSubjectByIdRequest
{
  public const string Route = "/Subjects/{SubjectId:int}";
  public static string BuildRoute(int subjectId) => Route.Replace("{SubjectId:int}", subjectId.ToString());

  public int SubjectId { get; set; }
}
