namespace ApuntecaDigital.Backend.Web.Classes;

public class GetClassByIdRequest
{
  public const string Route = "/Classes/{ClassId:int}";
  public static string BuildRoute(int classId) => Route.Replace("{ClassId:int}", classId.ToString());

  public int ClassId { get; set; }
}
