using ApuntecaDigital.Backend.IdentityServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
  .AddInMemoryIdentityResources(Config.IdentityResources)
  .AddInMemoryApiScopes(Config.ApiScopes)
  .AddInMemoryApiResources(Config.ApiResources)
  .AddInMemoryClients(Config.Clients)
  .AddTestUsers(Config.Users)
  .AddDeveloperSigningCredential();

builder.Services.AddAuthorization();

var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages()
    .RequireAuthorization();

app.UseCors(policy =>
{
  policy.AllowAnyOrigin();
  policy.AllowAnyHeader();
  policy.AllowAnyMethod();
});
app.MapDefaultControllerRoute();
app.Run();
