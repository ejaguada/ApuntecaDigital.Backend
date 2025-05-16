FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ApuntecaDigital.Backend.IdentityServer/ApuntecaDigital.Backend.IdentityServer.csproj", "src/ApuntecaDigital.Backend.IdentityServer/"]
RUN dotnet restore "src/ApuntecaDigital.Backend.IdentityServer/ApuntecaDigital.Backend.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/ApuntecaDigital.Backend.IdentityServer"
RUN dotnet build "./ApuntecaDigital.Backend.IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ApuntecaDigital.Backend.IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApuntecaDigital.Backend.IdentityServer.dll"]
