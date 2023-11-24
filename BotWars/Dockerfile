FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BotWars.csproj", "."]
RUN dotnet restore "./BotWars.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BotWars.csproj" -c Development -o /app/build

FROM build AS migration
WORKDIR /src

FROM build AS publish
RUN dotnet publish "BotWars.csproj" -c Development -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BotWars.dll"]