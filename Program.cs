using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using FobumCinema.Data;
using Microsoft.Extensions.DependencyInjection;

namespace FobumCinema
{
    public class Program
    {

        private static readonly string LastRunFileName = "LastRunDate.txt";

        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var dbSeeder = (DatabaseSeeder)scope.ServiceProvider.GetService(typeof(DatabaseSeeder));
            await dbSeeder.SeedAsync();

            if (ShouldScrapeData())
            {
                await dbSeeder.ScrapeData();
                UpdateLastRunDate();
            }

            await host.RunAsync();
        }

        private static bool ShouldScrapeData()
        {
            var lastRunDateStr = File.Exists(LastRunFileName) ? File.ReadAllText(LastRunFileName) : null;
            if (DateTime.TryParse(lastRunDateStr, out var lastRunDate))
            {
                return lastRunDate.Date < DateTime.Now.Date;
            }
            return true; 
        }

        private static void UpdateLastRunDate()
        {
            File.WriteAllText(LastRunFileName, DateTime.Now.ToString("yyyy-MM-dd"));
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
 