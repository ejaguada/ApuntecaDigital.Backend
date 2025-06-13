using ApuntecaDigital.Backend.IdentityServer;
using Duende.IdentityServer.Configuration;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
var redis = builder.Configuration.GetConnectionString("redis");

if (string.IsNullOrEmpty(redis))
{
    throw new InvalidOperationException("La cadena de conexión para Redis no está configurada.");
}

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redis;
});

builder.Services.AddDataProtection()
    .SetApplicationName("YourIdentityServer")
    .PersistKeysToStackExchangeRedis(
        ConnectionMultiplexer.Connect(redis),
        "DataProtection-Keys")
        .SetDefaultKeyLifetime(TimeSpan.FromDays(90));

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer(options =>
{
  // Disable key protection in development only
  options.KeyManagement.Enabled = false;
})
  .AddInMemoryIdentityResources(Config.IdentityResources)
  .AddInMemoryApiScopes(Config.ApiScopes)
  .AddInMemoryApiResources(Config.ApiResources)
  .AddInMemoryClients(Config.Clients(builder.Configuration))
  .AddTestUsers(Config.Users)
  .AddDeveloperSigningCredential();

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();
app.Run();
