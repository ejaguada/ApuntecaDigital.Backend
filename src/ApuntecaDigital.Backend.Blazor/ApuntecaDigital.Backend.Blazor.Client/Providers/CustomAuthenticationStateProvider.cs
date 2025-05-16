using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApuntecaDigital.Backend.Blazor.Client.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

namespace ApuntecaDigital.Backend.Blazor.Client.Providers;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var tokenContent = _tokenHandler.ReadJwtToken(savedToken);
            var expiry = tokenContent.ValidTo;

            if (expiry < DateTime.UtcNow)
            {
                await _localStorage.RemoveItemAsync("authToken");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claims = tokenContent.Claims.ToList();
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
        catch
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    // public async Task MarkUserAsAuthenticated(TokenResponse tokenResponse)
    // {
    //     await _localStorage.SetItemAsync("authToken", tokenResponse.AccessToken);
    //     var identity = new ClaimsIdentity(GetClaimsFromToken(tokenResponse.AccessToken), "jwt");
    //     var user = new ClaimsPrincipal(identity);
    //
    //     NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    // }

    public async Task MarkUserAsLoggedOut()
    {
        await _localStorage.RemoveItemAsync("authToken");
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private IEnumerable<Claim> GetClaimsFromToken(string jwt)
    {
        var token = _tokenHandler.ReadJwtToken(jwt);
        return token.Claims;
    }
}
