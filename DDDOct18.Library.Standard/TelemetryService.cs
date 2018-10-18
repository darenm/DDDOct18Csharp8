using System;
using System.Collections.Generic;
using System.Text;

namespace DDDOct18.Library.Standard
{
    public class TelemetryService
    {
        private Sensor _sensor = new Sensor();

        public async IAsyncEnumerable<SensorReading> GetTemperatureFromSensorsAsync()
        {
            var service = new SensorService();

            while (true)
            {
                IEnumerable<SensorReading> batch = await service.GetTemperatureFromSensorsAsync();
                if (batch == null) yield break;

                foreach (var item in batch)
                {
                    yield return item;
                }
            }
        }
    }
}
