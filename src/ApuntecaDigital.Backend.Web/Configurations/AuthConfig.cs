using Microsoft.IdentityModel.Tokens;

namespace ApuntecaDigital.Backend.Web.Configurations;

public static class AuthConfig
{
  public static IServiceCollection AddAuthServices(this IServiceCollection services)
  {
    services.AddAuthentication("Bearer")
      .AddJwtBearer("Bearer", options =>
      {
        options.Authority = "https://localhost:7057";
        options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
      });
    
    services.AddAuthorization(options =>
    {
      options.AddPolicy("ApiScope", policy =>
      {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api1");
      });
    });
    return services;
  }
}
