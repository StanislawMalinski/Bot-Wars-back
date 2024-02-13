#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG GIT_COMMIT=0
ENV BUILDX_GIT_INFO=false
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM ubuntu:latest AS compilator

RUN apt-get update && \
    apt-get install -y --no-install-recommends \
        vim g++ git make
#COPY --from=ubuntu /usr/bin/g++ /usr/bin/g++

COPY --from=compilator /usr/bin/gcc /app/bin/gcc


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BotWars/BotWars.csproj", "BotWars/"]
COPY ["Communication/Communication.csproj", "Communication/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "./BotWars/./BotWars.csproj"
COPY . .

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

#RUN dotnet ef migrations add Initial --project Shared/Shared.csproj --startup-project BotWars/BotWars.csproj --context Shared.DataAccess.Context.DataContext --output-dir /src/Shared/Migrations
#RUN dotnet ef migrations add TaskInitial --project Shared/Shared.csproj --startup-project BotWars/BotWars.csproj --context Shared.DataAccess.Context.TaskDataContext --output-dir /src/Shared/TMigrations

WORKDIR "/src/BotWars"
RUN dotnet build "./BotWars.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BotWars.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir FileSystem
RUN mkdir FileSystem/Bots
RUN mkdir FileSystem/Games
ENTRYPOINT ["dotnet", "BotWars.dll"]

