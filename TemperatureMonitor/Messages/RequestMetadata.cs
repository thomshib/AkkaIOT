

namespace TemperatureMonitor.Messages{


    public sealed class RequestMetadata{
        public long RequestId{get;}
        public RequestMetadata(long requestId)
        {
            this.RequestId = requestId;
        }
        
    }
}