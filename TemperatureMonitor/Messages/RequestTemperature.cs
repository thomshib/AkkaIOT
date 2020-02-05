
namespace TemperatureMonitor.Messages{


    public sealed class RequestTemperature{
        public long RequestId{get;}
        public RequestTemperature(long requestId)
        {
            this.RequestId = requestId;
        }
        
    }
}