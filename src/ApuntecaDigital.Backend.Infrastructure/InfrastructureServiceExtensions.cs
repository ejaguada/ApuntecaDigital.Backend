using ApuntecaDigital.Backend.Core.Interfaces;
using ApuntecaDigital.Backend.Core.Services;
using ApuntecaDigital.Backend.Infrastructure.Data;
using ApuntecaDigital.Backend.Infrastructure.Data.Queries;
using ApuntecaDigital.Backend.UseCases.Books.List;
using ApuntecaDigital.Backend.UseCases.Careers.List;
using ApuntecaDigital.Backend.UseCases.Classes.List;
using ApuntecaDigital.Backend.UseCases.Contributors.List;
using ApuntecaDigital.Backend.UseCases.Subjects.List;


namespace ApuntecaDigital.Backend.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    string? connectionString = config.GetConnectionString("database");
    Guard.Against.Null(connectionString);
    services.AddDbContext<AppDbContext>(options =>
     options.UseSqlServer(connectionString));

    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
           .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
           .AddScoped<IListContributorsQueryService, ListContributorsQueryService>()
           .AddScoped<IListBooksQueryService, ListBooksQueryService>()
           .AddScoped<IListSubjectsQueryService, ListSubjectsQueryService>()
           .AddScoped<IListClassesQueryService, ListClassesQueryService>()
           .AddScoped<IListCareersQueryService, ListCareersQueryService>()
           .AddScoped<IDeleteContributorService, DeleteContributorService>();


    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
