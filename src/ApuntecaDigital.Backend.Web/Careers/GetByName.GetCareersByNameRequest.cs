namespace ApuntecaDigital.Backend.Web.Careers;

public class GetCareersByNameRequest
{
  public const string Route = "/Careers/name/{Name}";
  public static string BuildRoute(string name) => Route.Replace("{Name}", name);

  public string Name { get; set; } = string.Empty;
}
