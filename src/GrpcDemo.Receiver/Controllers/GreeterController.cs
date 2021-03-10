using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Shared;

namespace GrpcDemo.Receiver.Controllers
{
    public class GreeterController : Greeter.GreeterBase
    {
        // Server side handler of the SayHello RPC
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
