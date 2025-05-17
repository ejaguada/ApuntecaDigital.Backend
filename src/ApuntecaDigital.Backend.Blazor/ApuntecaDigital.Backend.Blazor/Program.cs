using ApuntecaDigital.Backend.Blazor.Components;
using ApuntecaDigital.Backend.Blazor.Client.Services;
using Radzen;
using ApuntecaDigital.Backend.Blazor.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
  {
      options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
  })
  .AddCookie(options => options.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
  .AddOpenIdConnect(options =>
  {
      options.Authority = "https://localhost:7057";
      options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      options.ClientId = "blazor_client";
      options.ResponseType = "code";
      options.SaveTokens = true;
      options.Scope.Add("api1.read");
      options.Scope.Add("profile");
      options.Scope.Add("openid");
      options.GetClaimsFromUserInfoEndpoint = true;
      options.ClientSecret = "secret";
  });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Configure HttpClient with the API base URL
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:57679");
});

// Register the auth client
builder.Services.AddHttpClient("AuthClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7057");
});

builder.Services.AddRadzenComponents();

builder.Services.AddScoped<AuthenticationHeaderHandler>();

builder.Services.AddHttpClient("AuthenticatedClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:57679");
}).AddHttpMessageHandler<AuthenticationHeaderHandler>();

// Register the CareerService for server-side rendering
builder.Services.AddScoped<CareerService>();

// Register the ClassService for server-side rendering
builder.Services.AddScoped<ClassService>();

// Register the SubjectService for server-side rendering
builder.Services.AddScoped<SubjectService>();

builder.Services.AddScoped<BookService>();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ThemeService>();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Note: API is served directly from the Web project
// No need for proxy endpoints as the client will call the API directly

app.UseAntiforgery();

app.MapStaticAssets();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ApuntecaDigital.Backend.Blazor.Client._Imports).Assembly)
    .RequireAuthorization();

app.Run();
