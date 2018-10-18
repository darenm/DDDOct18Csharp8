using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDOct18.Library.Standard
{
    public class SensorService
    {
        private List<Sensor> _sensors;
        public SensorService()
        {
            _sensors = new List<Sensor>();
            for (int i = 0; i < 10; i++)
            {
                _sensors.Add(new Sensor());
            }
        }

        public async Task<IEnumerable<SensorReading>> GetTemperatureFromSensorsAsync()
        {
            var readings = new List<SensorReading>();
            foreach (var sensor in _sensors)
            {
                readings.Add(await sensor.GetTemperatureAsync());
            }

            return readings;
        }
    }
}
