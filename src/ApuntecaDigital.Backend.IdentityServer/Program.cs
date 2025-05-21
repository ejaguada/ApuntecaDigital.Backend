using ApuntecaDigital.Backend.IdentityServer;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
  .AddInMemoryIdentityResources(Config.IdentityResources)
  .AddInMemoryApiScopes(Config.ApiScopes)
  .AddInMemoryApiResources(Config.ApiResources)
  .AddInMemoryClients(Config.Clients(builder.Configuration)) // Fix: Pass the configuration to the method to resolve the group method issue
  .AddTestUsers(Config.Users)
  .AddDeveloperSigningCredential();

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseStaticFiles();

// This cookie policy fixes login issues with Chrome 80+ using HTTP
app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
