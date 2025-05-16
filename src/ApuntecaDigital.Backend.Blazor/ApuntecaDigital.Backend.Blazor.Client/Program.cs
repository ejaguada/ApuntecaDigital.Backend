using ApuntecaDigital.Backend.Blazor.Client;
using ApuntecaDigital.Backend.Blazor.Client.Providers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ApuntecaDigital.Backend.Blazor.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
// Authentication services for the client
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Register the auth client
builder.Services.AddHttpClient("AuthClient", client =>
{
  client.BaseAddress = new Uri("https://localhost:7057"); // Your IdentityServer URL
});

// Register the Blazor client
builder.Services.AddHttpClient("BlazorClient", client =>
{
  client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

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



builder.Services.AddScoped<AuthenticationHeaderHandler>();

builder.Services.AddHttpClient("AuthenticatedClient", client =>
{
  client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).AddHttpMessageHandler<AuthenticationHeaderHandler>();

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
