using DDDOct18.Library.Standard;
using static System.Console;
class C
{
    public static async System.Threading.Tasks.Task Main()
    {
        var service = new TelemetryService();

        // Until 10/17/2018 the syntax was:  foreach await !!!
        await foreach (var reading in service.GetTemperatureFromSensorsAsync())
        {
            System.Console.WriteLine($"Sensor: {reading.Id} - Temp: {reading.Reading}");
        }

        System.Console.WriteLine("**************************");
        System.Console.WriteLine("Press enter to exit");
        System.Console.ReadLine();
    }
}