using Common;
using Common.Contracts;
using Common.Models;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureGameData(this IServiceCollection services, IConfiguration config)
        {
            var dataPath = config["DataPath"];
            Console.WriteLine(dataPath);

            services.AddSingleton<IGameData, GameData>(_ => Utilities.GetGameData(string.Empty, dataPath));
        }
    }
}