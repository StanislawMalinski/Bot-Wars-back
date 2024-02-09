using Communication.APIs.Controllers;
using Communication.ServiceInterfaces;
using Communication.Services.FIle;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection;

public static class FileSystemInjection
{
    public static IServiceCollection AddFileSystem(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFileProvider, FileProvider>();
        serviceCollection.AddScoped<IFileRepository, FileRepository>();
        serviceCollection.AddSingleton<FileCompressor,FileCompressor>();


        serviceCollection.AddScoped<BotController>();
        serviceCollection.AddScoped<BotRepository>();
        return serviceCollection;
    }
}