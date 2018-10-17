using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDDOct18AsyncStreams
{
    public class Sensor
    {
        private static Random CapturedRandom = new Random();

        public async Task<int> GetTemperatureAsync()
        {
            await Task.Delay(CapturedRandom.Next(1, 8));
            return CapturedRandom.Next(50, 100);
        }
    }
}
