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
            string name = args[1];
            string surname = args[2];


            var response = await client.SayHelloAsync(
                new HelloRequest
                {
                    Name = name,
                    Surname = surname
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
                    Console.WriteLine("Time:" + responseSwitch.Year + "//" + responseSwitch.Month + "//" + responseSwitch.Day);
                    break;
                case 2:
                    int count = 0;
                    var responseSwitchGame = await client.PlayGameAsync(
                    new GameRequest { });
                    string msg = responseSwitchGame.Number;
                    while (true)
                    {
                        if (count == 5)
                        {
                            Console.WriteLine("LOSE");
                            Console.WriteLine("Number was " + msg);
                            break;
                        }
                        Console.WriteLine("Enter a number");
                        string number = Console.ReadLine();
                        if (Int32.Parse(msg) > Int32.Parse(number) && count < 5)
                        {
                            count++;
                            Console.WriteLine("LTH");
                        }
                        else if (Int32.Parse(msg) < Int32.Parse(number) && count < 5)
                        {
                            count++;
                            Console.WriteLine("GTH");
                        }
                        else if (Int32.Parse(msg) == Int32.Parse(number) && count < 5)
                        {
                            Console.WriteLine("WIN");
                            break;
                        }

                        else
                        {
                            count++;
                            Console.WriteLine("ERR");
                        }
                    }
                    break;

            }
            Console.ReadKey();
        }
    }
}
