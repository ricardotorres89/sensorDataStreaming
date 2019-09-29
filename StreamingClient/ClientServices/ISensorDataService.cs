using System.Collections.Generic;
using System.Threading;
using SensorDataService;

namespace StreamingClient.ClientServices
{
    public interface ISensorDataService
    {
        IAsyncEnumerable<PressureDataReply> GetSensorPresure(string sensorName);
    }
}