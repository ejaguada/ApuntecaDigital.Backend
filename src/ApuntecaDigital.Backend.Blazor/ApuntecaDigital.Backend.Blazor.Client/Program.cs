using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ApuntecaDigital.Backend.Blazor.Client.Services;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register HttpClient with API base URL
builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:57679") };
    
    // Configure the HttpClient to use the Web API URL
    // The actual API URL will be determined by the server configuration
    return httpClient;
});

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
