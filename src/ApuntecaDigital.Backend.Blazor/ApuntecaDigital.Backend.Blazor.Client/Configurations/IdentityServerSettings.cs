namespace ApuntecaDigital.Backend.Blazor.Client.Configurations;

public class IdentityServerSettings
{
  public string DiscoveryUrl { get; set; } = string.Empty;
  public string ClientName { get; set; } = string.Empty;
  public string ClientPassword { get; set; } = string.Empty;
  public bool UseHttps { get; set; }
}
