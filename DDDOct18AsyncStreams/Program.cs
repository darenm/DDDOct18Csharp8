using System;
using System.Threading.Tasks;

namespace DDDOct18AsyncStreams
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service = new TelemetryService();

            foreach await (var item in service.GetTemperatureFromSensors())
            {
                Console.WriteLine(item);
            }
        }
    }
}
