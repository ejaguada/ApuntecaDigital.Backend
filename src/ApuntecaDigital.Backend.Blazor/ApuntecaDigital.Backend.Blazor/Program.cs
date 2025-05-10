using ApuntecaDigital.Backend.Blazor.Components;
using ApuntecaDigital.Backend.Blazor.Client.Services;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Configure HttpClient with the API base URL
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:57679");
});
builder.Services.AddRadzenComponents();


// Register the CareerService for server-side rendering
builder.Services.AddScoped<CareerService>(sp => {
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("ApiClient");
    return new CareerService(httpClient);
});

// Register the ClassService for server-side rendering
builder.Services.AddScoped<ClassService>(sp => {
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("ApiClient");
    return new ClassService(httpClient);
});

// Register the SubjectService for server-side rendering
builder.Services.AddScoped<SubjectService>(sp => {
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("ApiClient");
    return new SubjectService(httpClient);
});

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ThemeService>();
var app = builder.Build();

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
    .AddAdditionalAssemblies(typeof(ApuntecaDigital.Backend.Blazor.Client._Imports).Assembly);

app.Run();
