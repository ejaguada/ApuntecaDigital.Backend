using ApuntecaDigital.Backend.Blazor.Client.Services;

namespace ApuntecaDigital.Backend.Blazor.Client;

public class AuthenticationHeaderHandler : DelegatingHandler
{
  private readonly IAuthenticationService _authService;

  public AuthenticationHeaderHandler(IAuthenticationService authService)
  {
    _authService = authService;
  }

  protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
  {
    var token = await _authService.GetTokenAsync("api1.read");
    if (!string.IsNullOrEmpty(token.AccessToken))
    {
      request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);
    }
    return await base.SendAsync(request, cancellationToken);
  }
}
