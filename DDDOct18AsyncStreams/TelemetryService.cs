using System;

using System.Collections.Generic;
using System.Text;

namespace DDDOct18AsyncStreams
{
    public class TelemetryService
    {
        private Sensor _sensor = new Sensor();

        public async IAsyncEnumerable<int> GetValuesFromServerAsync()
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

// Those interfaces will ship as part of .NET Core 3
namespace System.Collections.Generic
{
    public interface IAsyncEnumerable<out T>
    {
        IAsyncEnumerator<T> GetAsyncEnumerator();
    }

    public interface IAsyncEnumerator<out T> : System.IAsyncDisposable
    {
        System.Threading.Tasks.ValueTask<bool> MoveNextAsync();
        T Current { get; }
    }
}
namespace System
{
    public interface IAsyncDisposable
    {
        System.Threading.Tasks.ValueTask DisposeAsync();
    }
}
