using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Shared;
using GrpcDemo.Receiver.Controllers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GrpcDemo.Receiver.Services
{
    public sealed class GreetHostService : IHostedService
    {
        private const int Port = 5000;
        private readonly GreeterController _controller;
        private readonly ILogger<GreetHostService> _logger;
        private Grpc.Core.Server _server;

        public GreetHostService(GreeterController controller, ILogger<GreetHostService> logger)
        {
            _controller = controller;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _server = new Grpc.Core.Server
            {
                Services = { Greeter.BindService(_controller) },
                Ports = { new ServerPort("localhost", GreetHostService.Port, ServerCredentials.Insecure) }
            };
            _server.Start();

            _logger.LogInformation("Greeter server listening on port " + GreetHostService.Port);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _server.ShutdownAsync().ConfigureAwait(false);
        }
    }
}
