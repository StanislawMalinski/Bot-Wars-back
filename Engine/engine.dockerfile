FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 7001

RUN apt-get update
RUN apt-get install -y g++

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Engine/Engine.csproj", "Engine/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Engine/Engine.csproj"
COPY . .
WORKDIR "/src/Engine"
RUN dotnet build "Engine.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Engine.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .



ENTRYPOINT ["dotnet", "Engine.dll"]
