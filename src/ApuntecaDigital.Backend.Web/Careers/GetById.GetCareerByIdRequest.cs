namespace ApuntecaDigital.Backend.Web.Careers;

public class GetCareerByIdRequest
{
  public const string Route = "/Careers/id/{CareerId:int}";
  public static string BuildRoute(int careerId) => Route.Replace("{CareerId:int}", careerId.ToString());

  public int CareerId { get; set; }
}
