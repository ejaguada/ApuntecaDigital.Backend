namespace ApuntecaDigital.Backend.Web.Careers;

public record DeleteCareerRequest
{
  public const string Route = "/Careers/{CareerId:int}";
  public static string BuildRoute(int careerId) => Route.Replace("{CareerId:int}", careerId.ToString());

  public int CareerId { get; set; }
}
