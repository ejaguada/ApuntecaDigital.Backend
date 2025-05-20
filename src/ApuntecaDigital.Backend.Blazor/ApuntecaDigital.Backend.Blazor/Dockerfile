# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["nuget.config", "."]
COPY ["src/ApuntecaDigital.Backend.Blazor/ApuntecaDigital.Backend.Blazor/ApuntecaDigital.Backend.Blazor.csproj", "src/ApuntecaDigital.Backend.Blazor/ApuntecaDigital.Backend.Blazor/"]
COPY ["src/ApuntecaDigital.Backend.Infrastructure/ApuntecaDigital.Backend.Infrastructure.csproj", "src/ApuntecaDigital.Backend.Infrastructure/"]
COPY ["src/ApuntecaDigital.Backend.Core/ApuntecaDigital.Backend.Core.csproj", "src/ApuntecaDigital.Backend.Core/"]
COPY ["src/ApuntecaDigital.Backend.UseCases/ApuntecaDigital.Backend.UseCases.csproj", "src/ApuntecaDigital.Backend.UseCases/"]
COPY ["src/ApuntecaDigital.Backend.Blazor/ApuntecaDigital.Backend.Blazor.Client/ApuntecaDigital.Backend.Blazor.Client.csproj", "src/ApuntecaDigital.Backend.Blazor/ApuntecaDigital.Backend.Blazor.Client/"]
RUN dotnet restore "./src/ApuntecaDigital.Backend.Blazor/ApuntecaDigital.Backend.Blazor/ApuntecaDigital.Backend.Blazor.csproj"
COPY . .
WORKDIR "/src/src/ApuntecaDigital.Backend.Blazor/ApuntecaDigital.Backend.Blazor"
RUN dotnet build "./ApuntecaDigital.Backend.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ApuntecaDigital.Backend.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/certs
COPY certs/localhost.pfx /app/certs/localhost.pfx

ENTRYPOINT ["dotnet", "ApuntecaDigital.Backend.Blazor.dll"]