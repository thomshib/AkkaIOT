

namespace TemperatureMonitor.Messages{


    public sealed class RequestTemperatureSensorIds{
        public long RequestId{get;}
        public RequestTemperatureSensorIds(long requestId)
        {
            this.RequestId = requestId;
        }
        
    }
}