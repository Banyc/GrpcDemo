using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GrpcDemo.Client.Models;
using GrpcDemo.Client.Services;

namespace GrpcDemo.Client
{
    public class App
    {
        private readonly ITestService _testService;
        private readonly ILogger<App> _logger;
        private readonly AppSettings _config;
        private readonly GreetService _greet;

        public App(ITestService testService,
            IOptions<AppSettings> config,
            ILogger<App> logger,
            GreetService greet)
        {
            _testService = testService;
            _logger = logger;
            _config = config.Value;
            _greet = greet;
        }

        public void Run()
        {
            _logger.LogInformation($"This is a console application for {_config.ConsoleTitle}");
            _testService.Run();
            Console.WriteLine("Press return to send message");
            while (true)
            {
                Console.ReadLine();
                _greet.SendAsync().Wait();
            }
        }
    }
}
