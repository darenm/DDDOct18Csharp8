using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncStreamRepro
{
    public class TelemetryService
    {
        private Sensor _sensor = new Sensor();

        public async IAsyncEnumerable<int> GetTemperatureFromSensors()
        {
            while (true)
            {
                //IEnumerable<int> batch = await GetNextBatch();
                //if (batch == null) yield break;

                //foreach (int item in batch)
                //{
                //    yield return item;
                //}
                yield return await _sensor.GetTemperatureAsync();
            }
        }
    }
}
