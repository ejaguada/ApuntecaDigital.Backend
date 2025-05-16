using ApuntecaDigital.Backend.Blazor.Client.ViewModels;
using Duende.IdentityModel.Client;

namespace ApuntecaDigital.Backend.Blazor.Client.Services;

public interface IAuthenticationService
{
  Task LogoutAsync();
  Task<TokenResponse> GetTokenAsync(string scope);
}
