using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace gRPC
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name + " " + request.Surname
            });
        }
        public override Task<TimeReply> GetTime(TimeRequest request, ServerCallContext context)
        {
            string year = (DateTime.UtcNow.Year).ToString();
            string day = (DateTime.UtcNow.Day).ToString();
            string month = (DateTime.UtcNow.Month).ToString();
            return Task.FromResult(new TimeReply
            {
                Day = "Day:" + day,
                Month = "Month:" + month,
                Year = "Year: " + year,
            });
        }
        public async override Task<GameReply> PlayGame(GameRequest request, ServerCallContext context)
        {
            Random r = new Random();
            int rInt = r.Next(0, 100);
            return await Task.FromResult(new GameReply
            {
                Number = rInt.ToString()
            });
        }

    }
}

