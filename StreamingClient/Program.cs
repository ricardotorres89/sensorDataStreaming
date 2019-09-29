using StreamingClient.ClientServices;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StreamingClient
{
    class Program
    {
        private static readonly ISensorDataService _sensorDataService = new ClientServices.SensorDataService();

        static async Task Main(string[] args)
        {
            
            // Receive data from indoor sensor
            var indoorDataStream = _sensorDataService.GetSensorPresure("Sensor1");

            await foreach(var pressureDataPoint in indoorDataStream)
            {
                Console.WriteLine($"{DateTime.Now.ToString("hh:mm:ss.fff")} - Pressure for {pressureDataPoint.SensorName}: {pressureDataPoint.Pressure}");
            }
        }
    }
}
