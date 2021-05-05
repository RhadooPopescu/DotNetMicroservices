using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OcelotApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //This method provides the creation of webhost builder with the provided configurations.
        //We will provide the logging configurations and application confugurations.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => 
                {
                    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                //indicating logging app setigs configuration
                .ConfigureLogging((hostingContext, loggingbuilder) => 
                {
                    loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    loggingbuilder.AddConsole();
                    loggingbuilder.AddDebug();
                });
    }
}
