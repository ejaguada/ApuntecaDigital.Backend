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

// Verifica si identityEndpoint no es nulo antes de usarlo

// Wire up the callback urls (self referencing)

// Identity has a reference to all of the apps for callback urls, this is a cyclic reference
identityApi.WithEnvironment("ApiClient", libraryApi.GetEndpoint("https"));

builder.Build().Run();
