namespace ApuntecaDigital.Backend.Web.Subjects;

public class GetSubjectsByClassIdRequest
{
  public const string Route = "/subjects/filter-by/class/id/{Id:int}";
  public static string BuildRoute(int id) => Route.Replace("{Id:int}", id.ToString());

  public int Id { get; set; } = 0;
}
