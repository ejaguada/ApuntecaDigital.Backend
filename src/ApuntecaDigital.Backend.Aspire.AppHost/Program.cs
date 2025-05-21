var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("cache");

// Services
var identityApi = builder.AddProject<Projects.ApuntecaDigital_Backend_IdentityServer>("identity");

// Inicializa identityEndpoint como null, ya que EndpointReference no tiene un constructor sin parámetros
var identityEndpoint = identityApi.GetEndpoint("https");


var libraryApi = builder.AddProject<Projects.ApuntecaDigital_Backend_Web>("api")
    .WithExternalHttpEndpoints()
    .WithReference(identityApi);

// Apps
var blazorApp = builder.AddProject<Projects.ApuntecaDigital_Backend_Blazor>("blazor")
    .WithExternalHttpEndpoints()
    .WithReference(libraryApi)
    .WithReference(redis);

// Verifica si identityEndpoint no es nulo antes de usarlo
if (identityEndpoint != null)
{
    blazorApp.WithEnvironment("IdentityUrl", identityEndpoint);
}
else
{
    throw new InvalidOperationException("El endpoint de identidad no se pudo inicializar.");
}

// Wire up the callback urls (self referencing)
blazorApp.WithEnvironment("CallBackUrl", blazorApp.GetEndpoint("https"));

// Identity has a reference to all of the apps for callback urls, this is a cyclic reference
identityApi.WithEnvironment("LibraryApiClient", libraryApi.GetEndpoint("https"))
    .WithEnvironment("BlazorAppClient", blazorApp.GetEndpoint("https"));


builder.Build().Run();
