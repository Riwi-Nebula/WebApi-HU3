FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApi-HU3.Api/WebApi-HU3.Api.csproj", "WebApi-HU3.Api/"]
COPY ["WebApi-HU3.Application/WebApi-HU3.Application.csproj", "WebApi-HU3.Application/"]
COPY ["WebApi-HU3.Domain/WebApi-HU3.Domain.csproj", "WebApi-HU3.Domain/"]
COPY ["WebApi-HU3.Infraestructure/WebApi-HU3.Infraestructure.csproj", "WebApi-HU3.Infraestructure/"]
RUN dotnet restore "WebApi-HU3.Api/WebApi-HU3.Api.csproj"
COPY . .
WORKDIR "/src/WebApi-HU3.Api"
RUN dotnet build "./WebApi-HU3.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApi-HU3.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi-HU3.Api.dll"]
