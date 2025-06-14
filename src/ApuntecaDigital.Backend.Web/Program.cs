using ApuntecaDigital.Backend.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddSqlServerClient("sql");

builder.Services.AddAuthServices();

// Configuración de CORS: permite cualquier origen, método y cabecera
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(policy =>
  {
    policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
  });
});

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

builder.AddLoggerConfigs();

var appLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

builder.Services.AddOptionConfigs(builder.Configuration, appLogger, builder);
builder.Services.AddServiceConfigs(appLogger, builder);

builder.Services.AddFastEndpoints()
                .SwaggerDocument(o =>
                {
                  o.ShortSchemaNames = true;
                });



var app = builder.Build();

// Usa CORS después de la autenticación y antes de la autorización/endpoints
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

await app.UseAppMiddlewareAndSeedDatabase();
app.MapDefaultEndpoints();
app.Run();
