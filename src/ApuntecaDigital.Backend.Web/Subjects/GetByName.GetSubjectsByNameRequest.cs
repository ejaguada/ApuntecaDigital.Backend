namespace ApuntecaDigital.Backend.Web.Subjects;

public class GetSubjectsByNameRequest
{
  public const string Route = "/Subjects/name/{Name}";
  public static string BuildRoute(string name) => Route.Replace("{Name}", name);

  public string Name { get; set; } = string.Empty;
}
