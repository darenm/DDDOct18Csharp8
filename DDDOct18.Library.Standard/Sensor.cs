using System;
using System.Threading.Tasks;

namespace DDDOct18.Library.Standard
{
    public class Sensor
    {
        private static Random CapturedRandom = new Random();

        public string Id = Guid.NewGuid().ToString();

        public async Task<SensorReading> GetTemperatureAsync()
        {
            await Task.Delay(CapturedRandom.Next(1, 8));
            var reading = CapturedRandom.Next(50, 100);
            return new SensorReading(Id, reading);
        }
    }

    public class SensorReading
    {
        public string Id { get; }
        public int Reading { get; }

        // Uses a Tuple that is then mapped by the compiler to the above member variables
        public SensorReading(string id, int temperature) => (Id, Reading) = (id, temperature);
    }
}
