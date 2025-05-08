#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ApuntecaDigital.Backend.Web/ApuntecaDigital.Backend.Web.csproj", "src/ApuntecaDigital.Backend.Web/"]
COPY ["src/ApuntecaDigital.Backend.Core/ApuntecaDigital.Backend.Core.csproj", "src/ApuntecaDigital.Backend.Core/"]
COPY ["src/ApuntecaDigital.Backend.Infrastructure/ApuntecaDigital.Backend.Infrastructure.csproj", "src/ApuntecaDigital.Backend.Infrastructure/"]
COPY ["src/ApuntecaDigital.Backend.UseCases/ApuntecaDigital.Backend.UseCases.csproj", "src/ApuntecaDigital.Backend.UseCases/"]
# If you have a Directory.Packages.props file, uncomment the next line
COPY ["Directory.Packages.props", "./"]
RUN dotnet restore "src/ApuntecaDigital.Backend.Web/ApuntecaDigital.Backend.Web.csproj"
COPY . .
WORKDIR "/src/src/ApuntecaDigital.Backend.Web"
RUN dotnet build "ApuntecaDigital.Backend.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ApuntecaDigital.Backend.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create data and logs directories
RUN mkdir -p /app/data/logs && chown -R app:app /app/data

ENTRYPOINT ["dotnet", "ApuntecaDigital.Backend.Web.dll"]