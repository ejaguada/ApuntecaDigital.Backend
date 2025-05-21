using System.Security.Claims;

namespace ApuntecaDigital.Backend.Aspire.ServiceDefaults;
public static class ClaimsPrincipalExtensions
{
  public static string? GetUserId(this ClaimsPrincipal principal)
      => principal.FindFirst("sub")?.Value;

  public static string? GetUserName(this ClaimsPrincipal principal) =>
      principal.FindFirst(x => x.Type == ClaimTypes.Name)?.Value;
}
