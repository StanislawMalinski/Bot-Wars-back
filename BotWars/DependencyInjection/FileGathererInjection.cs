using Communication.ServiceInterfaces;
using Communication.Services.Achievement;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection
{
    public static class FileGathererInjection
    {
        public static IServiceCollection AddFileService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFileRepository, FileRepository>();
            return serviceCollection;
        }
    }
}
