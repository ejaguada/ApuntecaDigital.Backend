using ApuntecaDigital.Backend.Blazor.Client;
using ApuntecaDigital.Backend.Blazor.Client.Providers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ApuntecaDigital.Backend.Blazor.Client.Services;
using Blazored.LocalStorage;
using Radzen;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
// Authentication services for the client
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Register the auth client
builder.Services.AddHttpClient("AuthClient", client =>
{
  client.BaseAddress = new Uri("https://apuntecadigital.backend.identityserver:443"); // Your IdentityServer URL
});

// Register the Blazor client
builder.Services.AddHttpClient("BlazorClient", client =>
{
  client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<AuthenticationHeaderHandler>();

builder.Services.AddHttpClient("AuthenticatedClient", client =>
{
  client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).AddHttpMessageHandler<AuthenticationHeaderHandler>();
//
// builder.Services.AddScoped<IAuthenticationService>(sp =>
// {
//   var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
//   var navigationManager = sp.GetRequiredService<NavigationManager>();
//   var authStateProvider = sp.GetRequiredService<AuthenticationStateProvider>();
//
//   return new AuthenticationService(
//     httpClientFactory,
//     navigationManager,
//     authStateProvider);
// });
//

// Register services

builder.Services.AddRadzenComponents();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<CareerService>();
builder.Services.AddScoped<ClassService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ThemeService>();
await builder.Build().RunAsync();
