using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using GrpcDemo.Receiver.Models;
using GrpcDemo.Receiver.Services;
using GrpcDemo.Receiver.Controllers;
using System.Threading.Tasks;

namespace GrpcDemo.Receiver
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHostBuilder hostBuilder = CreateHostBuilder(args);
            await hostBuilder.RunConsoleAsync().ConfigureAwait(false);

            // // create service collection
            // var serviceCollection = new ServiceCollection();
            // ConfigureServices(serviceCollection);

            // // create service provider
            // var serviceProvider = serviceCollection.BuildServiceProvider();

            // // run app
            // serviceProvider.GetService<App>().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices(ConfigureServices);

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add logging
            serviceCollection.AddLogging(options =>
            {
                options.AddConsole();
                options.AddDebug();
            });

            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("Configuration"));
            ConfigureConsole(configuration);

            // add services
            serviceCollection.AddHostedService<GreetHostService>();
            serviceCollection.AddTransient<GreeterController>();
            serviceCollection.AddTransient<ITestService, TestService>();

            // add app
            serviceCollection.AddTransient<App>();
        }

        private static void ConfigureConsole(IConfigurationRoot configuration)
        {
            System.Console.Title = configuration.GetSection("Configuration:ConsoleTitle").Value;
        }
    }
}
