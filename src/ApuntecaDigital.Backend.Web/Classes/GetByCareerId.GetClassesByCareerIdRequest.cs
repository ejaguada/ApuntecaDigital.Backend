namespace ApuntecaDigital.Backend.Web.Classes;

public class GetClassesByCareerIdRequest
{
  public const string Route = "/Classes/filter-by/career/id/{CareerId:int}";
  public static string BuildRoute(int careerId) => Route.Replace("{CareerId:int}", careerId.ToString());

  public int CareerId { get; set; }
}
