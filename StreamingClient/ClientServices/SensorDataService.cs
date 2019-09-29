using Grpc.Core;
using Grpc.Net.Client;
using SensorDataService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static SensorDataService.Sensors;

namespace StreamingClient.ClientServices
{
    public class SensorDataService : ISensorDataService
    {
        private readonly GrpcChannel _channel;
        private readonly SensorsClient _client;

        public SensorDataService()
        {
            _channel = GrpcChannel.ForAddress("https://localhost:5001");
            _client = new SensorsClient(_channel);
        }

        public IAsyncEnumerable<PressureDataReply> GetSensorPresure(string sensorName)
        {
            var request = _client.GetSensorPresure(new PressureDataRequest()
            {
                SensorName = sensorName
            });

            return request.ResponseStream.ReadAllAsync();
        }
    }
}
