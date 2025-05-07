namespace ApuntecaDigital.Backend.Web.Classes;

public record DeleteClassRequest
{
  public const string Route = "/Classes/{ClassId:int}";
  public static string BuildRoute(int classId) => Route.Replace("{ClassId:int}", classId.ToString());

  public int ClassId { get; set; }
}
