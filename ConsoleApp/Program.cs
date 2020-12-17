using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using gRPC;
using Grpc.Net.Client;

namespace ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new Greeter.GreeterClient(channel);
            

            var response = await client.SayHelloAsync(
                new HelloRequest
                {
                    Name = "Ömer Can",
                    Surname = "Sucu"
                });
            Console.WriteLine("From Server: " + response.Message);
            Console.WriteLine("1 - Get Time");
            Console.WriteLine("2 - Prediction Game");
            int caseSwitch = Convert.ToInt32(Console.ReadLine());
            switch (caseSwitch)
            {
                case 1:
                    var responseSwitch = await client.GetTimeAsync(
                        new TimeRequest { });
                    Console.WriteLine("Time:" + responseSwitch.Year+ "//" + responseSwitch.Month + "//" + responseSwitch.Day);
                    break;
                case 2:
                    while (true)
                    {
                        Console.WriteLine("Enter a number");
                        string number = Console.ReadLine();
                        var responseSwitch2 = await client.PlayGameAsync(
                            new GameRequest
                            {
                                Message = number
                            }
                        );
                        string msg = responseSwitch2.Message;
                        Console.WriteLine(msg);
                        if(msg == "WIN")
                        {
                            break;
                        }
                    }
                    break;
                    
            }
            Console.ReadKey();
        }
    }
}
