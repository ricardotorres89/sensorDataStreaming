using StreamingClient.ClientServices;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace StreamingClient
{
    class Program
    {
        private static readonly ISensorDataService _sensorDataService = new ClientServices.SensorDataService();

        static async Task Main(string[] args)
        {
            
            // Receive data from sensors
            var indoorDataStream = _sensorDataService.GetSensorPresure("IndoorSensor");
            var outdoorDataStream = _sensorDataService.GetSensorPresure("OutdoorSensor");

            // Combine Indoor and outdoor data and generate alerts if pressure dirrence is above 20
            var pressureDataAlerts = indoorDataStream
                                        .Zip(outdoorDataStream)
                                        .Select(combined => Math.Abs(combined.First.Pressure - combined.Second.Pressure))
                                        .Where(v => v > 20);


            await foreach(var deltaPressure in pressureDataAlerts)
            {
                Console.WriteLine($"{DateTime.Now.ToString("hh:mm:ss.fff")} - ALERT Pressure difference {deltaPressure}");
            }
        }
    }
}
