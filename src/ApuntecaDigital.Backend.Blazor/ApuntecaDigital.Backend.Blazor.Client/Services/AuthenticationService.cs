using System.Net.Http.Json;
using ApuntecaDigital.Backend.Blazor.Client.Configurations;
using ApuntecaDigital.Backend.Blazor.Client.Providers;
using ApuntecaDigital.Backend.Blazor.Client.ViewModels;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;

namespace ApuntecaDigital.Backend.Blazor.Client.Services;

public class AuthenticationService : IAuthenticationService
{
  private readonly HttpClient _authClient;
  private readonly HttpClient _blazorClient;
  private readonly NavigationManager _navigationManager;
  private readonly AuthenticationStateProvider _authenticationStateProvider;
  private readonly IOptions<IdentityServerSettings> _identityServerSettings;
  private readonly DiscoveryDocumentResponse _discoveryDocument;

  public AuthenticationService(
    IHttpClientFactory httpClientFactory,
    NavigationManager navigationManager,
    AuthenticationStateProvider authenticationStateProvider,
    IOptions<IdentityServerSettings> identityServerSettings)
  {
    _identityServerSettings = identityServerSettings;
    _authClient = httpClientFactory.CreateClient("AuthClient");
    _blazorClient = httpClientFactory.CreateClient("ApiClient");
    _discoveryDocument = _authClient.GetDiscoveryDocumentAsync(_identityServerSettings.Value.DiscoveryUrl).Result;

    if (_discoveryDocument.IsError)
    {
      throw new Exception("Unable to get discovery document " + _discoveryDocument.Exception);
    }

    _navigationManager = navigationManager;
    _authenticationStateProvider = authenticationStateProvider;
  }
  
  public async Task LogoutAsync()
  {
    await ((CustomAuthenticationStateProvider)_authenticationStateProvider)
      .MarkUserAsLoggedOut();
    _navigationManager.NavigateTo("/");
  }

  public async Task<TokenResponse> GetTokenAsync(string scope)
  {
    var tokenResponse = await _authClient.RequestClientCredentialsTokenAsync(
        new ClientCredentialsTokenRequest
        {
          Address = _discoveryDocument.TokenEndpoint,
          ClientId = "api_client",
          ClientSecret = "secret",
          Scope = scope,
        }
      );

    if (tokenResponse.IsError)
    {
      throw new Exception("unable to get token", tokenResponse.Exception);
    }

    return tokenResponse;
  }
}
