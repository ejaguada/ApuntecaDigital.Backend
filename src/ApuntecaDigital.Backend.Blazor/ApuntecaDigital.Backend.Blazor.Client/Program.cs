using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ApuntecaDigital.Backend.Blazor.Client.Services;

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
builder.Services.AddScoped<CareerService>();

await builder.Build().RunAsync();
