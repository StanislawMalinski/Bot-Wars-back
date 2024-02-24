FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base


RUN groupadd -r exe && useradd -r -g exe -d /app -s /sbin/nologin -c "Docker image user" userexe
WORKDIR /app
EXPOSE 8080

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
RUN mkdir FileSystem
RUN mkdir FileSystem/Bots
RUN mkdir FileSystem/Games

ENTRYPOINT ["dotnet", "Engine.dll"]
