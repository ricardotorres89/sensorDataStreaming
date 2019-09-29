using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace SensorDataService
{
    public class SensorDataService : Sensors.SensorsBase
    {
        private static readonly Random _random = new Random();
        private readonly ILogger<SensorDataService> _logger;
        private const double _minValue = 0;
        private const double _maxValue = 100;

        public SensorDataService(ILogger<SensorDataService> logger)
        {
            _logger = logger;
        }

        public override async Task GetSensorPresure(PressureDataRequest request,
                                                 IServerStreamWriter<PressureDataReply> responseStream,
                                                 ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                try
                {
                    // Simulate delay for Sensor reading
                    await Task.Delay(TimeSpan.FromSeconds(_random.Next(1, 5)));

                    var sensorPressure = new PressureDataReply()
                    {
                        SensorName = request.SensorName,
                        Pressure = _random.NextDouble() * (_maxValue - _minValue) + _minValue
                    };
                    await responseStream.WriteAsync(sensorPressure);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "A problem has occured while getting sensor temperatures");
                }
            }
        }
    }
}
