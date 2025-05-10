namespace ApuntecaDigital.Backend.Web.Classes;

public class GetClassesByNameRequest
{
  public const string Route = "/Classes/name/{Name}";
  public static string BuildRoute(string name) => Route.Replace("{Name}", name);

  public string Name { get; set; } = string.Empty;
}
