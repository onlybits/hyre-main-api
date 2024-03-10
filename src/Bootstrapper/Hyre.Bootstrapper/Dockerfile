FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Bootstrapper/Hyre.Bootstrapper/Hyre.Bootstrapper.csproj", "src/Bootstrapper/Hyre.Bootstrapper/"]
COPY ["src/Modules/Jobs/Hyre.Modules.Jobs.API/Hyre.Modules.Jobs.API.csproj", "src/Modules/Jobs/Hyre.Modules.Jobs.API/"]
COPY ["src/Modules/Jobs/Hyre.Modules.Jobs.Application/Hyre.Modules.Jobs.Application.csproj", "src/Modules/Jobs/Hyre.Modules.Jobs.Application/"]
COPY ["src/Modules/Jobs/Hyre.Modules.Jobs.Core/Hyre.Modules.Jobs.Core.csproj", "src/Modules/Jobs/Hyre.Modules.Jobs.Core/"]
COPY ["src/Shared/Hyre.Shared.Infrastructure/Hyre.Shared.Infrastructure.csproj", "src/Shared/Hyre.Shared.Infrastructure/"]
COPY ["src/Shared/Hyre.Shared.Abstractions/Hyre.Shared.Abstractions.csproj", "src/Shared/Hyre.Shared.Abstractions/"]
COPY ["src/Modules/Jobs/Hyre.Modules.Jobs.Core.Events/Hyre.Modules.Jobs.Core.Events.csproj", "src/Modules/Jobs/Hyre.Modules.Jobs.Core.Events/"]
COPY ["src/Modules/Jobs/Hyre.Modules.Jobs.Infrastructure/Hyre.Modules.Jobs.Infrastructure.csproj", "src/Modules/Jobs/Hyre.Modules.Jobs.Infrastructure/"]
COPY ["src/Modules/Notifications/Hyre.Modules.Notifications.API/Hyre.Modules.Notifications.API.csproj", "src/Modules/Notifications/Hyre.Modules.Notifications.API/"]
COPY ["src/Modules/Notifications/Hyre.Modules.Notifications.Application/Hyre.Modules.Notifications.Application.csproj", "src/Modules/Notifications/Hyre.Modules.Notifications.Application/"]
COPY ["src/Modules/Notifications/Hyre.Modules.Notifications.Core/Hyre.Modules.Notifications.Core.csproj", "src/Modules/Notifications/Hyre.Modules.Notifications.Core/"]
COPY ["src/Modules/Notifications/Hyre.Modules.Notifications.Infrastructure/Hyre.Modules.Notifications.Infrastructure.csproj", "src/Modules/Notifications/Hyre.Modules.Notifications.Infrastructure/"]
RUN dotnet restore "src/Bootstrapper/Hyre.Bootstrapper/Hyre.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/src/Bootstrapper/Hyre.Bootstrapper"
RUN dotnet build "Hyre.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Hyre.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hyre.Bootstrapper.dll"]
