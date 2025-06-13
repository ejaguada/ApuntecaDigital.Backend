// Fix for CS1061: Replaced "AddDatabase" with "AddResource" and created a custom database resource.
// Ensure that the custom database resource is properly defined elsewhere in the application.

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var sql = builder.AddSqlServer("sql")
                 .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase("database");

// Services
var identityApi = builder.AddProject<Projects.ApuntecaDigital_Backend_IdentityServer>("identity")
  .WithReference(redis);

// Inicializa identityEndpoint como null, ya que EndpointReference no tiene un constructor sin parámetros
var identityEndpoint = identityApi.GetEndpoint("https");

var libraryApi = builder.AddProject<Projects.ApuntecaDigital_Backend_Web>("api")
    .WithExternalHttpEndpoints()
    .WithReference(db)
    .WithReference(identityApi);

// Apps

//var webApp = builder.AddPnpmApp("WebApp", "../ApuntecaDigital.Backend.WebApp", "dev")
//  .WithHttpsEndpoint(env: "PORT")
//  .WithExternalHttpEndpoints()
//  .WithPnpmPackageInstallation();

// Verifica si identityEndpoint no es nulo antes de usarlo

// Wire up the callback urls (self referencing)

// Identity has a reference to all of the apps for callback urls, this is a cyclic reference
identityApi.WithEnvironment("ApiClient", libraryApi.GetEndpoint("https"));

builder.Build().Run();
