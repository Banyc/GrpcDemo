using System;
using Grpc.Shared;
using Grpc.Net.Client;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GrpcDemo.Client.Services
{
    public class GreetService
    {
        private readonly ILogger<GreetService> _logger;
        public GreetService(ILogger<GreetService> logger)
        {
            _logger = logger;
        }
        // follow the instruction here: <https://go.microsoft.com/fwlink/?linkid=2086909>
        // do not follow the instruction of google's
        public async Task SendAsync()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            _logger.LogInformation("Greeting: " + reply.Message);
        }
    }
}
