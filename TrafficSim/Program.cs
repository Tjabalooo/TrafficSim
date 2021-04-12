using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using TrafficSim.Conf;

namespace TrafficSim
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!(args.Length >= 1))
            {
                Console.WriteLine("TrafficSim needs a json config to work!");
                WaitForAnyKey();
                return;
            }

            try
            {
                var trafficSimJson = JsonSerializer.Deserialize<TrafficSimConf>(File.ReadAllText(args[0]));
                var trafficSim = new TrafficSim(
                    trafficSimJson,
                    message => Console.WriteLine(message),
                    new System.Net.Http.HttpClient());

                while (true)
                {
                    trafficSim.Update();
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                WaitForAnyKey();
                return;
            }
        }

        public static void WaitForAnyKey()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
