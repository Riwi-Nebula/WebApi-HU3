FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Simulacro-HU3.sln","./"]
COPY ["Simulacro-HU3.Api/Simulacro-HU3.Api.csproj", "Simulacro-HU3.Api/"]
COPY ["Simulacro-HU3.Application/Simulacro-HU3.Application.csproj", "Simulacro-HU3.Application/"]
COPY ["Simulacro-HU3.Domain/Simulacro-HU3.Domain.csproj", "Simulacro-HU3.Domain/"]
COPY ["Simulacro-HU3.Infraestructure/Simulacro-HU3.Infraestructure.csproj", "Simulacro-HU3.Infraestructure/"]
RUN dotnet restore "Simulacro-HU3.Api/Simulacro-HU3.Api.csproj"
COPY . .
WORKDIR "/src/Simulacro-HU3.Api"
RUN dotnet build "./Simulacro-HU3.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simulacro-HU3.Api.dll"]
