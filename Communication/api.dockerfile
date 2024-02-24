#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BotWars/BotWars.csproj", "BotWars/"]
COPY ["Communication/Communication.csproj", "Communication/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "./BotWars/./BotWars.csproj"
COPY . .

RUN dotnet tool install --global dotnet-ef --version 8.0.1

ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet ef migrations add Initial --project Shared/Shared.csproj --startup-project BotWars/BotWars.csproj --context Shared.DataAccess.Context.DataContext --output-dir Shared/Migrations
RUN dotnet ef migrations add TaskInitial --project Shared/Shared.csproj --startup-project BotWars/BotWars.csproj --context Shared.DataAccess.Context.TaskDataContext --output-dir Shared/TMigrations

WORKDIR "/src/BotWars"
RUN dotnet build "./BotWars.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BotWars.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BotWars.dll"]
