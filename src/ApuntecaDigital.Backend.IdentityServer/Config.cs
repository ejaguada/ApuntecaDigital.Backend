using System.Security.Claims;
using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

namespace ApuntecaDigital.Backend.IdentityServer;

// Config.cs
public static class Config
{
  public static IEnumerable<IdentityResource> IdentityResources =>
    new IdentityResource[] { new IdentityResources.OpenId(), new IdentityResources.Profile() };

  public static IEnumerable<ApiScope> ApiScopes =>
    new ApiScope[] { new ApiScope("api1.read"), new ApiScope("api1.write") };

  public static IEnumerable<ApiResource> ApiResources =>
    new ApiResource[]
    {
      new ApiResource("api1", "My API")
      {
        Scopes = { "api1.read", "api1.write" }, ApiSecrets = new List<Secret> { new Secret("secret".Sha256()) }
      }
    };

  public static IEnumerable<Client> Clients =>
    new Client[]
    {
      // Web API client
      new Client
      {
        ClientId = "api_client",
        ClientSecrets = { new Secret("secret".Sha256()) },
        AllowedGrantTypes = GrantTypes.ClientCredentials,
        AllowedScopes = { "api1.read", "api1.write" }
      },
      new Client
      {
        ClientId = "swagger_client",
        ClientName = "Swagger UI",
        AllowedGrantTypes = GrantTypes.Implicit,
        AllowAccessTokensViaBrowser = true,
        RedirectUris = { "https://localhost:57679/swagger/oauth2-redirect.html" },
        PostLogoutRedirectUris = { "https://localhost:57679/swagger/" },
        AllowedScopes = { "api1" }
      },
      // Blazor client
      new Client
      {
        ClientId = "blazor_client",
        ClientName = "Blazor Client",
        AllowedGrantTypes = GrantTypes.Code,
        ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
        RequirePkce = true,
        RequireClientSecret = true,
        RedirectUris = { 
          "https://localhost:7214/signin-oidc"
        },
        PostLogoutRedirectUris = { "https://localhost:7214/logout-callback" },
        RequireConsent = false,
        AllowOfflineAccess = true,
        AllowPlainTextPkce = false,
        AlwaysIncludeUserClaimsInIdToken = true,
        AllowedScopes =
        {
          IdentityServerConstants.StandardScopes.OpenId,
          IdentityServerConstants.StandardScopes.Profile,
          "api1.read"
        }
      }
    };

  public static List<TestUser> Users =>
    new List<TestUser>
    {
      new TestUser
      {
        SubjectId = "818727",
        Username = "admin",
        Password = "Pass123$",
        Claims =
        {
          new Claim(JwtClaimTypes.Name, "Admin User"),
          new Claim(JwtClaimTypes.GivenName, "Admin"),
          new Claim(JwtClaimTypes.FamilyName, "User"),
          new Claim(JwtClaimTypes.Email, "admin@example.com"),
          new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
          new Claim(JwtClaimTypes.Role, "admin")
        }
      }
    };
}
