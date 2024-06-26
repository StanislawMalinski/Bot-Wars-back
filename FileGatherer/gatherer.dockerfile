FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
RUN mkdir /app/Storage

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FileGatherer/FileGatherer.csproj", "FileGatherer/"]
RUN dotnet restore "./FileGatherer/./FileGatherer.csproj"
COPY . .


RUN dotnet tool install --global dotnet-ef --version 8.0.1
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef Migrations add Initial --project FileGatherer/FileGatherer.csproj

WORKDIR "/src/FileGatherer"
RUN dotnet build "./FileGatherer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FileGatherer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileGatherer.dll"]
